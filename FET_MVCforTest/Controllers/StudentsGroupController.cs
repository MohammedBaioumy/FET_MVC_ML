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
	public class StudentsGroupController : Controller
	{
		private readonly AppDbContext _context;
		private readonly IMapper _mapper;

		public StudentsGroupController(AppDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		// GET: StudentsGroup
		public async Task<IActionResult> Index()
		{
			var groups = await _context.StudentsGroups.ToListAsync();
			var viewModel = _mapper.Map<List<StudentsGroupViewModel>>(groups);
			return View(viewModel);
		}

		// GET: StudentsGroup/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: StudentsGroup/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(StudentsGroupViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			try
			{
				var group = _mapper.Map<StudentsGroup>(model);
				_context.StudentsGroups.Add(group);
				await _context.SaveChangesAsync();
				TempData["Operation"] = "Students group created successfully!";
				TempData["ToastColor"] = "text-dark-emphasis bg-info";

				return RedirectToAction(nameof(Index));
			}
			catch (Exception ex)
			{
				TempData["Error"] = "An error occurred while creating the students group.";
				return View(model);
			}
		}

		// GET: StudentsGroup/Edit/5
		public async Task<IActionResult> Edit(int id)
		{
			var group = await _context.StudentsGroups.FindAsync(id);
			if (group == null)
			{
				TempData["Error"] = "Students group not found.";

				return RedirectToAction(nameof(Index));
			}

			var viewModel = _mapper.Map<StudentsGroupViewModel>(group);
			return View(viewModel);
		}

		// POST: StudentsGroup/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(StudentsGroupViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			try
			{
				var group = await _context.StudentsGroups.FindAsync(model.Id);
				if (group == null)
				{
					TempData["Error"] = "Students group not found.";
					return RedirectToAction(nameof(Index));
				}

				_mapper.Map(model, group);

				_context.StudentsGroups.Update(group);
				await _context.SaveChangesAsync();
				TempData["Operation"] = "Students group updated successfully!";
				TempData["ToastColor"] = "text-dark-emphasis bg-info";

				return RedirectToAction(nameof(Index));
			}
			catch (Exception ex)
			{
				TempData["Error"] = "An error occurred while updating the students group.";
				return View(model);
			}
		}

		// POST: StudentsGroup/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Delete(int id)
		{
			try
			{
				var group = await _context.StudentsGroups.FindAsync(id);
				if (group == null)
				{
					TempData["Error"] = "Students group not found.";
					return RedirectToAction(nameof(Index));
				}

				_context.StudentsGroups.Remove(group);
				await _context.SaveChangesAsync();
				TempData["Operation"] = "Students group deleted successfully!";
				TempData["ToastColor"] = "text-dark-emphasis bg-info";

			}
			catch (Exception ex)
			{
				TempData["Error"] = "An error occurred while deleting the students group.";
			}

			return RedirectToAction(nameof(Index));
		}
	}
}
