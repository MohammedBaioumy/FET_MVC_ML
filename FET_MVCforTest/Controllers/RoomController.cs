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
	public class RoomController : Controller
	{
		private readonly AppDbContext _context;
		private readonly IMapper _mapper;

		public RoomController(AppDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<IActionResult> Index()
		{
			var rooms = await _context.Rooms.ToListAsync();
			var vm = _mapper.Map<List<RoomViewModel>>(rooms);
			return View(vm);
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(RoomViewModel model)
		{
			if (!ModelState.IsValid)
				return View(model);

			var room = _mapper.Map<Room>(model);
			try
			{
				_context.Add(room);
				await _context.SaveChangesAsync();
				TempData["Operation"] = "Room created successfully.";
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				TempData["Error"] = "Failed to create room.";
				return View(model);
			}
		}

		public async Task<IActionResult> Edit(int id)
		{
			var room = await _context.Rooms.FindAsync(id);
			if (room == null) return NotFound();

			var model = _mapper.Map<RoomViewModel>(room);
			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(RoomViewModel model)
		{
			if (!ModelState.IsValid)
				return View(model);

			var room = await _context.Rooms.FindAsync(model.Id);
			if (room == null) return NotFound();

			_mapper.Map(model, room);

			try
			{
				await _context.SaveChangesAsync();
				TempData["Operation"] = "Room updated successfully.";
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				TempData["Error"] = "Failed to update room.";
				return View(model);
			}
		}

		[HttpPost]
		public async Task<IActionResult> Delete(int id)
		{
			var room = await _context.Rooms.FindAsync(id);
			if (room == null)
			{
				TempData["Error"] = "Room not found.";
				return RedirectToAction(nameof(Index));
			}

			try
			{
				_context.Rooms.Remove(room);
				await _context.SaveChangesAsync();
				TempData["Operation"] = "Room deleted successfully.";
			}
			catch
			{
				TempData["Error"] = "Failed to delete room.";
			}

			return RedirectToAction(nameof(Index));
		}
	}
}
