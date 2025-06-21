using AutoMapper;
using FET_MVCforTest.Data;
using FET_MVCforTest.Entities;
using FET_MVCforTest.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FET_MVCforTest.Controllers
{
	[Authorize()]
	public class BasicController : Controller
	{
		private readonly AppDbContext _context;
		private readonly IMapper _mapper;

		public BasicController(AppDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public IActionResult Index()
		{
			var _Basic = _context.Basics.SingleOrDefault();
			return View(_mapper.Map<BasicViewModel>(_Basic));
		}

		public IActionResult Create()
		{
			// التحقق من وجود إعدادات أساسية بالفعل
			if (_context.Basics.Any())
			{
				TempData["Error"] = "Basic settings already exist. You can only edit the existing settings.";
				return RedirectToAction(nameof(Index));
			}
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(BasicViewModel model)
		{
			// التحقق من وجود إعدادات أساسية بالفعل
			if (_context.Basics.Any())
			{
				TempData["Error"] = "Basic settings already exist. You can only edit the existing settings.";
				return RedirectToAction(nameof(Index));
			}

			if (!ModelState.IsValid)
				return View(model);

			try
			{
				var basic = _mapper.Map<Basic>(model);
				_context.Basics.Add(basic);
				_context.SaveChanges();
				TempData["Operation"] = "Basic settings added successfully";
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
			var basic = _context.Basics.Find(id);
			if (basic == null)
				return NotFound();

			var model = _mapper.Map<BasicViewModel>(basic);
			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(BasicViewModel model)
		{
			if (!ModelState.IsValid)
				return View(model);

			try
			{
				var basic = _context.Basics.Find(model.Id);
				if (basic == null)
					return NotFound();

				_mapper.Map(model, basic);
				_context.SaveChanges();
				TempData["Operation"] = "Basic settings updated successfully";
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
				var basic = _context.Basics.Find(id);
				if (basic == null)
					return NotFound();

				_context.Basics.Remove(basic);
				_context.SaveChanges();
				TempData["Operation"] = "Basic settings deleted successfully";
				TempData["ToastColor"] = "text-dark-emphasis bg-info";
				return RedirectToAction(nameof(Index));
			}
			catch (Exception)
			{
				TempData["Error"] = "An error occurred while deleting.";
				TempData["Operation"] = "Failed to delete basic settings";
				TempData["ToastColor"] = "text-light bg-danger";
				return RedirectToAction(nameof(Index));
			}
		}
	}
}