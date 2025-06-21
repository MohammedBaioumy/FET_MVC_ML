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
	public class DepartmentController : Controller
	{
		private readonly AppDbContext _context;
		private readonly IMapper _mapper;

		public DepartmentController(AppDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		// GET: Department
		public IActionResult Index()
		{
			//var departments = _context.Departments.Include(d => d.Faculty).ToList();

			//var model = _mapper.Map<List<DepartmentViewModel>>(departments);
		    var departments = _context.Departments.Include(d => d.Faculty).Select(d => new DepartmentDetailsViewModel()
			{
				FacultyId = d.FacultyId,
				FacultyName = d.Faculty.Name,
				Id = d.Id,
				Name = d.Name,
			});
			return View(departments);
		}

		// GET: Department/Create
		public IActionResult Create()
		{
			ViewBag.Faculties = _context.Faculties.ToList();
			return View();
		}

		// POST: Department/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(DepartmentViewModel model)
		{
			//model.FacultyName = _context.Faculties.FirstOrDefault(f => f.Id == model.FacultyId)?.Name;
			if (ModelState.IsValid)
			{
				try
				{
					var department = _mapper.Map<Department>(model);
					_context.Departments.Add(department);
					_context.SaveChanges();
					TempData["Operation"] = "Department Added successfully :) ";
					TempData["ToastColor"] = "text-dark-emphasis bg-info";
					return RedirectToAction(nameof(Index));
				}
				catch (Exception)
				{
					var message = "Something went wrong while creating the department.";
					ModelState.AddModelError(string.Empty, message);
					TempData["Operation"] = message;
					TempData["ToastColor"] = "text-light bg-danger";
				}
			}

			ViewBag.Faculties = _context.Faculties.ToList();
			return View(model);
		}

		// GET: Department/Edit/5
		public IActionResult Edit(int id)
		{
			var department = _context.Departments.Find(id);
			if (department == null)
				return NotFound();

			var model = _mapper.Map<DepartmentEditViewModel>(department);
			ViewBag.Faculties = _context.Faculties.ToList();
			return View(model);
		}

		// POST: Department/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit([FromRoute] int id, DepartmentEditViewModel model)
		{
		//	model.FacultyName = _context.Faculties.FirstOrDefault(f => f.Id == model.FacultyId)?.Name;
		     model.Id=id;
			if (ModelState.IsValid)
			{
				try
				{
					var department = _context.Departments.Find(model.Id);
					if (department == null)
						return NotFound();

					department.Name = model.Name;
					department.FacultyId = model.FacultyId;
					
					_context.Departments.Update(department);
				//	_mapper.Map(model, department);
					_context.SaveChanges();
					TempData["Operation"] = "Department was updated :) ";
					TempData["ToastColor"] = "text-dark-emphasis bg-info";
					return RedirectToAction(nameof(Index));
				}
				catch (Exception)
				{
					var message = "Something went wrong while updating the department.";
					ModelState.AddModelError(string.Empty, message);
					TempData["Operation"] =message;
					TempData["ToastColor"] = "text-dark-emphasis bg-info";
				}
			}

			ViewBag.Faculties = _context.Faculties.ToList();
			return View(model);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Delete(int id)
		{
			try
			{
				var department = _context.Departments.Find(id);
				if (department == null)
				{
					TempData["Error"] = "Department not found.";
					return RedirectToAction(nameof(Index));
				}

				_context.Departments.Remove(department);
				_context.SaveChanges();
				TempData["Operation"] = "Department deleted successfully.";
				TempData["ToastColor"] = "text-dark-emphasis bg-info";


			}
			catch (Exception)
			{
				ModelState.AddModelError(string.Empty, "An error occurred while deleting the department.");

				TempData["Error"] = "An error occurred while deleting the department.";
				TempData["Operation"] = "An error occurred while deleting the department.";
				TempData["ToastColor"] = "text-dark-emphasis bg-info";
			}

			return RedirectToAction(nameof(Index));
		}
	}
	}
