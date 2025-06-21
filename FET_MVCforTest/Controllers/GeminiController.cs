using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FET_MVCforTest.Data;
using FET_MVCforTest.Models.GeminiViewModels;
using FET_MVCforTest.Services;
using FET_MVCforTest.Models;
using DinkToPdf;
using FET_MVCforTest.Entities;
using DinkToPdf.Contracts;
using AutoMapper;
using System.ComponentModel;
using OfficeOpenXml;
using LicenseContext = OfficeOpenXml.LicenseContext;
using Microsoft.AspNetCore.Authorization;

namespace FET_MVCforTest.Controllers
{
	[Authorize]
	public class GeminiController : Controller
	{
		private readonly AppDbContext _context;
		private readonly GeminiService _geminiService;
		private readonly IConverter _converter;
		private readonly IMapper _mapper;
		private static bool _isGenerating = false;
		private static int _currentSubjectId = 0;

		public GeminiController(AppDbContext context, IConverter converter, IMapper mapper,IConfiguration configuration)
		{
			_context = context;
			_converter = converter;
			_mapper = mapper;
			_geminiService = new GeminiService(configuration["Gemini:ApiKey"]);
		}

		// صفحة التحميل المؤقتة
		public IActionResult Loading()
		{
			return View();
		}

		// للتحقق من حالة التوليد
		public JsonResult CheckGenerationStatus()
		{
			return Json(new { isComplete = !_isGenerating });
		}

		// للتحقق من حالة توليد المقال
		public JsonResult CheckArticleGenerationStatus()
		{
			return Json(new { isComplete = !_isGenerating, subjectId = _currentSubjectId });
		}

		public async Task<IActionResult> Generate()
		{
			try
			{
				_isGenerating = true;

				var teachers = await _context.Teachers
					.Select(t => new TeacherData
					{
						Name = t.Name,
						MaxHoursPerDay = t.MaxHoursPerDay ?? 0,
						MaxHoursPerWeek = t.MaxHoursPerWeek ?? 0
					}).ToListAsync();

				var subjects = await _context.Subjects
					.Select(s => new SubjectData { Name = s.Name, FullScintificName = s.FullScientificName }).ToListAsync();

				var groups = await _context.StudentsGroups
					.Select(g => new GroupData
					{
						Name = g.Name,
						NumberOfStudents = g.NumberOfStudents ?? 0
					}).ToListAsync();

				var rooms = await _context.Rooms
					.Select(r => new RoomData
					{
						Name = r.Name,
						Capacity = r.Capacity ?? 0
					}).ToListAsync();

				var activities = await _context.Activities
					.Include(a => a.Teacher).Include(a => a.Subject).Include(a => a.StudentsGroup)
					.Select(a => new ActivityData
					{
						TeacherName = a.Teacher.Name,
						SubjectName = a.Subject.Name,
						GroupName = a.StudentsGroup.Name,
						Duration = a.Duration,
						NumOfSessionsPerWeek = a.NumOfSessionsPerWeek ?? 1
					}).ToListAsync();

				var Constraints = await _context.Constraints
					.Select(c => new ConstraintViewModel
					{
						ConstraintName = c.ConstraintName,
						ConstraintsDetails = c.ConstraintsDetails
					}).ToListAsync();

				var basics = await _context.Basics
					.Select(b => new BasicViewModel
					{
						Name = b.Name,
						startOfWeek = b.startOfWeek,
						endOfWeek = b.endOfWeek,
						TimeStart = b.TimeStart,
						TimeEnd = b.TimeEnd,
					}).SingleOrDefaultAsync();

				var faculties = await _context.Faculties.ToListAsync();
				var departments = await _context.Departments.ToListAsync();

				if (teachers.Count() == 0 || groups.Count() == 0 || subjects.Count() == 0 ||
					rooms.Count() == 0 || activities.Count() == 0 ||
					departments.Count() == 0 || faculties.Count() == 0 || basics is null)
				{
					TempData["Operation"] = @"Failed to Generate Table ,please check your cards 
                                            you must enter data in Basic ,Faculties,Departments,
                                            Subjects, StudentGroups, Teachers,Activities,Rooms  ";
					TempData["ToastColor"] = "text-light bg-danger";
					return RedirectToAction("Index", "Home");
				}

				var htmlTable = await _geminiService.GenerateScheduleTableAsync(basics, teachers, subjects, groups, activities, rooms, Constraints);

				ViewBag.ScheduleTable = htmlTable;
				return View("ScheduleTable");
			}
			finally
			{
				_isGenerating = false;
			}
		}

		[HttpPost]
		public async Task<IActionResult> SendFeedback(string feedback, string currentTableHtml)
		{
			var teachers = await _context.Teachers
				.Select(t => new TeacherData
				{
					Name = t.Name,
					MaxHoursPerDay = t.MaxHoursPerDay ?? 0,
					MaxHoursPerWeek = t.MaxHoursPerWeek ?? 0
				}).ToListAsync();

			var subjects = await _context.Subjects
				.Select(s => new SubjectData { Name = s.Name }).ToListAsync();

			var groups = await _context.StudentsGroups
				.Select(g => new GroupData
				{
					Name = g.Name,
					NumberOfStudents = g.NumberOfStudents ?? 0
				}).ToListAsync();

			var rooms = await _context.Rooms
				.Select(r => new RoomData
				{
					Name = r.Name,
					Capacity = r.Capacity ?? 0
				}).ToListAsync();

			var activities = await _context.Activities
				.Include(a => a.Teacher).Include(a => a.Subject).Include(a => a.StudentsGroup)
				.Select(a => new ActivityData
				{
					TeacherName = a.Teacher.Name,
					SubjectName = a.Subject.Name,
					GroupName = a.StudentsGroup.Name,
					Duration = a.Duration,
					NumOfSessionsPerWeek = a.NumOfSessionsPerWeek ?? 1
				}).ToListAsync();

			var Constraints = await _context.Constraints
				.Select(c => new ConstraintViewModel
				{
					ConstraintName = c.ConstraintName,
					ConstraintsDetails = c.ConstraintsDetails
				}).ToListAsync();

			var basics = await _context.Basics
				.Select(b => new BasicViewModel
				{
					Name = b.Name,
					startOfWeek = b.startOfWeek,
					endOfWeek = b.endOfWeek,
					TimeStart = b.TimeStart,
					TimeEnd = b.TimeEnd,
				}).SingleOrDefaultAsync();

			var htmlTable = await _geminiService.GenerateScheduleTableAsync(basics, teachers, subjects, groups, activities, rooms, Constraints, feedback, currentTableHtml);

			ViewBag.ScheduleTable = htmlTable;
			ViewBag.Feedback = feedback;
			return View("ScheduleTable");
		}

		public async Task<IActionResult> ResetDashboard()
		{
			try
			{
				_context.Activities.RemoveRange(_context.Activities);
				_context.Constraints.RemoveRange(_context.Constraints);
				_context.Rooms.RemoveRange(_context.Rooms);
				_context.Subjects.RemoveRange(_context.Subjects);
				_context.Teachers.RemoveRange(_context.Teachers);
				_context.StudentsGroups.RemoveRange(_context.StudentsGroups);
				_context.Departments.RemoveRange(_context.Departments);
				_context.Faculties.RemoveRange(_context.Faculties);
				_context.Basics.RemoveRange(_context.Basics);

				await _context.SaveChangesAsync();

				TempData["Operation"] = "Dashboard reset successfully.";
				TempData["ToastColor"] = "text-dark-emphasis bg-info";
			}
			catch (Exception)
			{
				var message = "An unexpected error occurred. Please try again.";
				ModelState.AddModelError("", message);
				TempData["Operation"] = message;
				TempData["ToastColor"] = "text-light bg-danger";
			}

			return RedirectToAction("Index", "Home");
		}

		[HttpGet]
		public async Task<IActionResult> SubjectArticles()
		{
			var subjects = await _context.Subjects.ToListAsync();
			var mappedSubjects = _mapper.Map<List<SubjectViewModel>>(subjects);
			return View(mappedSubjects);
		}

		[HttpGet]
		public async Task<IActionResult> ViewSubjectArticle(int subjectId)
		{
			var subject = await _context.Subjects.FindAsync(subjectId);
			if (subject == null)
			{
				return NotFound();
			}

			var existingArticle = await _context.SubjectArticles
				.FirstOrDefaultAsync(a => a.SubjectId == subjectId);

			var viewModel = new SubjectArticleViewModel
			{
				SubjectId = subject.Id,
				SubjectName = subject.Name,
				ScientificName = subject.FullScientificName,
				GeneratedDate = existingArticle?.GeneratedDate ?? DateTime.MinValue,
				ArticleContent = existingArticle?.ArticleContent ?? "No article generated yet."
			};

			return View("SubjectArticle", viewModel);
		}

		public async Task<IActionResult> GenerateSubjectArticle(int subjectId)
		{
			try
			{
				_isGenerating = true;
				_currentSubjectId = subjectId;

				var subject = await _context.Subjects.FindAsync(subjectId);
				if (subject == null)
				{
					return NotFound();
				}

				var articleContent = await _geminiService.GenerateSubjectArticleAsync(subject.Name, subject.FullScientificName);

				var existingArticle = await _context.SubjectArticles
					.FirstOrDefaultAsync(a => a.SubjectId == subjectId);

				if (existingArticle != null)
				{
					existingArticle.ArticleContent = articleContent;
					existingArticle.GeneratedDate = DateTime.Now;
				}
				else
				{
					var newArticle = new SubjectArticle
					{
						SubjectId = subjectId,
						ArticleContent = articleContent,
						GeneratedDate = DateTime.Now
					};
					_context.SubjectArticles.Add(newArticle);
				}

				await _context.SaveChangesAsync();

				return RedirectToAction("ViewSubjectArticle", new { subjectId });
			}
			finally
			{
				_isGenerating = false;
				_currentSubjectId = 0;
			}
		}

		public async Task<IActionResult> DownloadArticlePdf(int subjectId)
		{
			var subject = await _context.Subjects.FindAsync(subjectId);
			if (subject == null)
			{
				return NotFound();
			}

			var article = await _context.SubjectArticles
				.FirstOrDefaultAsync(a => a.SubjectId == subjectId);

			if (article == null)
			{
				return NotFound("Article not found");
			}

			var globalSettings = new GlobalSettings
			{
				ColorMode = ColorMode.Color,
				Orientation = Orientation.Portrait,
				PaperSize = PaperKind.A4,
				Margins = new MarginSettings { Top = 10, Bottom = 10, Left = 10, Right = 10 },
				DocumentTitle = $"{subject.Name} - Scientific Article"
			};

			var objectSettings = new ObjectSettings
			{
				PagesCount = true,
				HtmlContent = $@"
            <h1 style='text-align: center;'>{subject.FullScientificName} ({subject.Name})</h1>
            <h3 style='text-align: center;'>Scientific Article</h3>
            <p style='text-align: center;'>Generated on: {article.GeneratedDate.ToString("yyyy-MM-dd")}</p>
            <hr>
            <div style='text-align: justify;'>{article.ArticleContent}</div>
        ",
				WebSettings = { DefaultEncoding = "utf-8" },
				HeaderSettings = { FontSize = 9, Right = "Page [page] of [toPage]", Line = true },
				FooterSettings = { FontSize = 9, Center = "© FET Timetable System" }
			};

			var pdf = new HtmlToPdfDocument()
			{
				GlobalSettings = globalSettings,
				Objects = { objectSettings }
			};

			var file = _converter.Convert(pdf);
			return File(file, "application/pdf", $"{subject.Name}_Article.pdf");
		}

		[HttpGet]
		public async Task<IActionResult> GenerateFromExcel()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> GenerateFromExcel(IFormFile excelFile)
		{
			ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
			if (excelFile == null || excelFile.Length == 0)
			{
				return BadRequest("No file uploaded.");
			}

			string excelData = "";
			using (var stream = excelFile.OpenReadStream())
			using (var package = new ExcelPackage(stream))
			{
				var worksheet = package.Workbook.Worksheets[0];
				int rowCount = worksheet.Dimension.Rows;
				int colCount = worksheet.Dimension.Columns;
				for (int row = 1; row <= rowCount; row++)
				{
					List<string> rowValues = new List<string>();
					for (int col = 1; col <= colCount; col++)
					{
						rowValues.Add(worksheet.Cells[row, col].Value?.ToString() ?? "");
					}
					excelData += string.Join(", ", rowValues) + "\n";
				}
			}

			// Prompt to send to Gemini
			string prompt = $@"
This is raw schedule data extracted from an Excel file:

{excelData}

Please generate a weekly schedule table based solely on this data.

Guidelines:
- Use an HTML <table> with the following Bootstrap classes:
  table table-bordered table-striped table-hover text-center align-middle table-responsive
- Make the table header (thead) dark using the 'table-dark' class
- Ensure the table is clean, consistent, professional, and well-formatted
- Return only the HTML code of the table — no explanation or extra text
";

			var htmlTable = await _geminiService.GenerateScheduleTableFromPrompt(prompt);

			ViewBag.ScheduleTable = htmlTable;
			return View("ScheduleTable");
		}

	}
}