using AutoMapper;
using FET_MVCforTest.Data;
using FET_MVCforTest.Entities;
using FET_MVCforTest.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FET_MVCforTest.Controllers
{
	[Authorize()]
	public class TeacherController : Controller
	{
		private readonly AppDbContext _context;
		private readonly IMapper _mapper;

		public TeacherController(AppDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<IActionResult> Index()
		{
			var teachers = await _context.Teachers.ToListAsync();
			var viewModel = _mapper.Map<List<TeacherViewModel>>(teachers);
			return View(viewModel);
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(TeacherViewModel model)
		{
			if (!ModelState.IsValid)
				return View(model);

			var teacher = _mapper.Map<Teacher>(model);
			_context.Teachers.Add(teacher);
			await _context.SaveChangesAsync();

			TempData["Operation"] = "Created successfully";
			return RedirectToAction(nameof(Index));
		}

		public async Task<IActionResult> Edit(int id)
		{
			var teacher = await _context.Teachers.FindAsync(id);
			if (teacher == null)
				return NotFound();

			var viewModel = _mapper.Map<TeacherViewModel>(teacher);
			return View(viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(TeacherViewModel model)
		{
			if (!ModelState.IsValid)
				return View(model);

			var teacher = await _context.Teachers.FindAsync(model.Id);
			if (teacher == null)
				return NotFound();

			_mapper.Map(model, teacher);
			await _context.SaveChangesAsync();

			TempData["Operation"] = "Updated successfully";
			return RedirectToAction(nameof(Index));
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Delete(int id)
		{
			var teacher = await _context.Teachers.FindAsync(id);
			if (teacher == null)
				return NotFound();

			_context.Teachers.Remove(teacher);
			await _context.SaveChangesAsync();

			TempData["Operation"] = "Deleted successfully";
			return RedirectToAction(nameof(Index));
		}
	}
}
