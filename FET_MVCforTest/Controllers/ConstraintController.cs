using AutoMapper;
using FET_MVCforTest.Data;
using FET_MVCforTest.Entities;
using FET_MVCforTest.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FET_MVCforTest.Controllers
{
	[Authorize]
	public class ConstraintsController : Controller
	{
		private readonly AppDbContext _context;
		private readonly IMapper _mapper;

		public ConstraintsController(AppDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		// GET: Constraints
		public IActionResult Index()
		{
			var constraints = _context.Constraints.ToList();
			var viewModel = _mapper.Map<List<ConstraintViewModel>>(constraints);
			return View(viewModel);
		}

		// GET: Constraints/Create
		public IActionResult Create()
		{			
			return View();
		}

		// POST: Constraints/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(ConstraintViewModel model)
		{
			if (!ModelState.IsValid)
			{				
				return View(model);
			}

			try
			{
				var constraint = _mapper.Map<Constraints>(model);
				_context.Constraints.Add(constraint);
				_context.SaveChanges();
				TempData["Operation"] = "Constraint added successfully.";
				TempData["ToastColor"] = "text-dark-emphasis bg-info";

				return RedirectToAction("Index");
			}
			catch (Exception ex)
			{
				ModelState.AddModelError("", "Something went wrong: " + ex.Message);				
				return View(model);
			}
		}

		// GET: Constraints/Edit/5
		public IActionResult Edit(int id)
		{
			var constraint = _context.Constraints.Find(id);
			if (constraint == null)
				return NotFound();

			var model = _mapper.Map<ConstraintViewModel>(constraint);			
			return View(model);
		}

		// POST: Constraints/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(int id, ConstraintViewModel model)
		{
			if (id != model.Id)
				return BadRequest();

			if (!ModelState.IsValid)
			{				
				return View(model);
			}

			try
			{
				var constraint = _mapper.Map<Constraints>(model);
				_context.Constraints.Update(constraint);
				_context.SaveChanges();
				TempData["Operation"] = "Constraint updated successfully.";
				TempData["ToastColor"] = "text-dark-emphasis bg-info";

				return RedirectToAction("Index");
			}
			catch (Exception ex)
			{
				ModelState.AddModelError("", "Error: " + ex.Message);				
				return View(model);
			}
		}

		// POST: Constraints/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Delete(int id)
		{
			try
			{
				var constraint = _context.Constraints.Find(id);
				if (constraint == null)
				{
					TempData["Error"] = "Constraint not found.";
					return RedirectToAction("Index");
				}

				_context.Constraints.Remove(constraint);
				_context.SaveChanges();
				TempData["Operation"] = "Constraint deleted successfully.";
				TempData["ToastColor"] = "text-dark-emphasis bg-info";

			}
			catch (Exception ex)
			{
				TempData["Error"] = "An error occurred while deleting the constraint: " + ex.Message;
			}

			return RedirectToAction("Index");
		}

		
	}
}