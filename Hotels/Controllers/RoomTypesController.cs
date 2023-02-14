using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hotels.Data;
using Hotels.Models;
using Hotels.Services;

namespace Hotels.Controllers
{
    public class RoomTypesController : Controller
    {
        private readonly IGenericHotelService<RoomType> _roomTypeService;

        public RoomTypesController(IGenericHotelService<RoomType> roomTypeService)
        {
            _roomTypeService = roomTypeService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _roomTypeService.GetAllItemsAsync());
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roomType = await _roomTypeService.GetItemByIdAsync(id);

            if (roomType == null)
            {
                return NotFound();
            }
            return View(roomType);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,BasePrice,Description,ImageUrl")] RoomType roomType)
        {
            if (ModelState.IsValid)
            {
                roomType.ID = Guid.NewGuid();
                await _roomTypeService.CreateItemAsync(roomType);
                return RedirectToAction(nameof(Index));
            }
            return View(roomType);
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roomType = await _roomTypeService.GetItemByIdAsync(id);
            if (roomType == null)
            {
                return NotFound();
            }
            return View(roomType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ID,Name,BasePrice,Description,ImageUrl")] RoomType roomType)
        {
            if (id != roomType.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _roomTypeService.EditItemAsync(roomType);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_roomTypeService.GetItemByIdAsync(id) == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(roomType);
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roomType = await _roomTypeService.GetItemByIdAsync(id);
            if (roomType == null)
            {
                return NotFound();
            }

            return View(roomType);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var roomType = await _roomTypeService.GetItemByIdAsync(id);
            await _roomTypeService.DeleteItemAsync(roomType);
            return RedirectToAction(nameof(Index));
        }
    }
}
