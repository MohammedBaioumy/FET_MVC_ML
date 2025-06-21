using AutoMapper;
using FET_MVCforTest.Data;
using FET_MVCforTest.Entities;
using FET_MVCforTest.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FET_MVCforTest.Controllers
{
	[Authorize]
	public class FacultyController : Controller
	{
		private readonly AppDbContext _context;
		private readonly IMapper _mapper;

		public FacultyController(AppDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public IActionResult Index()
		{
			var faculties = _context.Faculties.ToList();
			var viewModel = _mapper.Map<List<FacultyViewModel>>(faculties);
			return View(viewModel);
		}

		public IActionResult Create()
		{
			return View(new FacultyViewModel());
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(FacultyViewModel model)
		{
			if (!ModelState.IsValid)
				return View(model);

			try
			{
				var faculty = _mapper.Map<Faculty>(model);
				_context.Faculties.Add(faculty);
				_context.SaveChanges();
				TempData["Operation"] = "Faculty Added successfully :) ";
				TempData["ToastColor"] = "text-dark-emphasis bg-info";
				return RedirectToAction(nameof(Index));
			}
			catch (Exception)
			{
				var message = "An unexpected error occurred. Please try again.";
				ModelState.AddModelError("", message);
				TempData["Operation"] = message;
				TempData["ToastColor"] = "text-light bg-danger";
				return View(model);
			}
		}

		public IActionResult Edit(int id)
		{
			var faculty = _context.Faculties.Find(id);
			if (faculty == null)
				return NotFound();

			var model = _mapper.Map<FacultyViewModel>(faculty);
			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(FacultyViewModel model)
		{
			if (!ModelState.IsValid)
				return View(model);

			try
			{
				var faculty = _context.Faculties.Find(model.Id);
				if (faculty == null)
					return NotFound();

				_mapper.Map(model, faculty); // Update existing entity
				_context.SaveChanges();
				TempData["Operation"] = "Faculty was updated :) ";
				TempData["ToastColor"] = "text-dark-emphasis bg-info";
				return RedirectToAction(nameof(Index));
			}
			catch (Exception)
			{
				var message = "An error occurred while updating. Please try again.";
				ModelState.AddModelError("", message);
				TempData["Operation"] = message;
				TempData["ToastColor"] = "text-light bg-danger";
				return View(model);
			}
		}

		public IActionResult Delete(int id)
		{
			try
			{
				var faculty = _context.Faculties.Find(id);
				if (faculty == null)
					return NotFound();

				_context.Faculties.Remove(faculty);
				_context.SaveChanges();
				TempData["Operation"] = "Faculty Deleted successfully";
				TempData["ToastColor"] = "text-dark-emphasis bg-info";
				return RedirectToAction(nameof(Index));
			}
			catch (Exception)
			{
				TempData["Error"] = "An error occurred while deleting.";
				TempData["Operation"] = "An error occurred while deleting :( ";
				TempData["ToastColor"] = "text-light bg-danger";
				return RedirectToAction(nameof(Index));
			}
		}
	}
}
