using AutoMapper;
using FET_MVCforTest.Data;
using FET_MVCforTest.Entities;
using FET_MVCforTest.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FET_MVCforTest.Controllers
{
	[Authorize()]
	public class ActivityController : Controller
	{
		private readonly AppDbContext _context;
		private readonly IMapper _mapper;

		public ActivityController(AppDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<IActionResult> Index()
		{
			var activities = await _context.Activities
				.Include(a => a.Teacher)
				.Include(a => a.Subject)
				.Include(a => a.StudentsGroup)
				.ToListAsync();

			var viewModel = _mapper.Map<List<UpdateActivityViewModel>>(activities);
			return View(viewModel);
		}

		public IActionResult Create()
		{
			PopulateDropdowns();
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(ActivityViewModel viewModel)
		{
			//viewModel.SubjectName = _context.Subjects.FirstOrDefault(s => s.Id == viewModel.SubjectId).Name;
			//viewModel.StudentsGroupName = _context.StudentsGroups.FirstOrDefault(sg => sg.Id == viewModel.StudentsGroupId).Name;
			//viewModel.TeacherName = _context.Teachers.FirstOrDefault(t => t.Id == viewModel.TeacherId).Name;

			if (!ModelState.IsValid)
			{
				PopulateDropdowns();
				return View(viewModel);
			}

			try
			{
				var activity = _mapper.Map<Activity>(viewModel);
				_context.Add(activity);
				await _context.SaveChangesAsync();
				TempData["Operation"] = "Activity created successfully!";
				TempData["ToastColor"] = "text-dark-emphasis bg-info";

				return RedirectToAction(nameof(Index));
			}
			catch (Exception ex)
			{
				TempData["Error"] = $"Error while creating Activity: {ex.Message}";
				PopulateDropdowns();
				return View(viewModel);
			}
		}

		public async Task<IActionResult> Edit(int id)
		{
			var activity = await _context.Activities.FindAsync(id);
			if (activity == null)
				return NotFound();

			var viewModel = _mapper.Map<UpdateActivityViewModel>(activity);
			PopulateDropdowns();
			return View(viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, UpdateActivityViewModel viewModel)
		{
			viewModel.SubjectName = _context.Subjects.FirstOrDefault(s => s.Id == viewModel.SubjectId).Name;
			viewModel.StudentsGroupName = _context.StudentsGroups.FirstOrDefault(sg => sg.Id == viewModel.StudentsGroupId).Name;
			viewModel.TeacherName = _context.Teachers.FirstOrDefault(t => t.Id == viewModel.TeacherId).Name;
			if (id != viewModel.Id)
				return BadRequest();

			if (!ModelState.IsValid)
			{
				PopulateDropdowns();
				return View(viewModel);
			}

			try
			{
				var activity = _mapper.Map<Activity>(viewModel);
				activity.StudentsGroup = null;
				activity.Subject = null;
				activity.Teacher = null;
				
				_context.Update(activity);
				 await _context.SaveChangesAsync();
				TempData["Operation"] = "Activity updated successfully!";
				TempData["ToastColor"] = "text-dark-emphasis bg-info";

				return RedirectToAction(nameof(Index));
			}
			catch (Exception ex)
			{
				TempData["Error"] = $"Error while updating Activity: {ex.Message}";
				PopulateDropdowns();
				return View(viewModel);
			}
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Delete(int id)
		{
			var activity = await _context.Activities.FindAsync(id);
			if (activity == null)
				return NotFound();

			try
			{
				_context.Activities.Remove(activity);
				await _context.SaveChangesAsync();
				TempData["Operation"] = "Activity deleted successfully!";
				TempData["ToastColor"] = "text-dark-emphasis bg-info";

			}
			catch (Exception ex)
			{
				TempData["Error"] = $"Error while deleting Activity: {ex.Message}";

			}

			return RedirectToAction(nameof(Index));
		}

		private void PopulateDropdowns()
		{
			ViewBag.Teachers = new SelectList(_context.Teachers, "Id", "Name");
			ViewBag.Subjects = new SelectList(_context.Subjects, "Id", "Name");
			ViewBag.StudentsGroups = new SelectList(_context.StudentsGroups, "Id", "Name");
		}
	}
}
