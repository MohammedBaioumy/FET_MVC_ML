using AutoMapper;
using FET_MVCforTest.Data;
using FET_MVCforTest.Entities;
using FET_MVCforTest.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FET_MVCforTest.Controllers
{
	[Authorize]
	public class SubjectController : Controller
	{
		private readonly AppDbContext _context;
		private readonly IMapper _mapper;

		public SubjectController(AppDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<IActionResult> Index()
		{
			var subjects = await _context.Subjects
				.Include(s => s.Department)
				.ToListAsync();

			var vm = _mapper.Map<List<SubjectViewModel>>(subjects);
			return View(vm);
		}

		public IActionResult Create()
		{
			ViewBag.Departments = _context.Departments.ToList();
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(SubjectViewModel model)
		{
			if (!ModelState.IsValid)
			{
				ViewBag.Departments = _context.Departments.ToList();
				return View(model);
			}

			var subject = _mapper.Map<Subject>(model);
			try
			{
				_context.Add(subject);
				await _context.SaveChangesAsync();
				TempData["Operation"] = "Subject created successfully.";
				TempData["ToastColor"] = "text-dark-emphasis bg-info";

				return RedirectToAction(nameof(Index));
			}
			catch
			{
				TempData["Error"] = "Failed to create subject.";
				TempData["ToastColor"] = "text-light bg-danger";
				ViewBag.Departments = _context.Departments.ToList();
				return View(model);
			}
		}

		public async Task<IActionResult> Edit(int id)
		{
			var subject = await _context.Subjects.FindAsync(id);
			if (subject == null) return NotFound();

			ViewBag.Departments = _context.Departments.ToList();
			var model = _mapper.Map<SubjectViewModel>(subject);
			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(SubjectViewModel model)
		{
			if (!ModelState.IsValid)
			{
				ViewBag.Departments = _context.Departments.ToList();
				return View(model);
			}

			var subject = await _context.Subjects.FindAsync(model.Id);
			if (subject == null) return NotFound();

			_mapper.Map(model, subject);

			try
			{
				await _context.SaveChangesAsync();
				TempData["Operation"] = "Subject updated successfully.";
				TempData["ToastColor"] = "text-dark-emphasis bg-info";

				return RedirectToAction(nameof(Index));
			}
			catch
			{
				TempData["Error"] = "Failed to update subject.";
				TempData["ToastColor"] = "text-light bg-danger";
				ViewBag.Departments = _context.Departments.ToList();
				return View(model);
			}
		}

		[HttpPost]
		public async Task<IActionResult> Delete(int id)
		{
			var subject = await _context.Subjects.FindAsync(id);
			if (subject == null)
			{
				TempData["Error"] = "Subject not found.";
				return RedirectToAction(nameof(Index));
			}

			try
			{
				_context.Subjects.Remove(subject);
				await _context.SaveChangesAsync();
				TempData["Operation"] = "Subject deleted successfully.";
				TempData["ToastColor"] = "text-dark-emphasis bg-info";

			}
			catch
			{
				TempData["Error"] = "Failed to delete subject.";
				TempData["ToastColor"] = "text-light bg-danger";
			}

			return RedirectToAction(nameof(Index));
		}
	}
}