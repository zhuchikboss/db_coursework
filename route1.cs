// Commit message: Add route attribute for custom routing in BookingController

using Microsoft.AspNetCore.Mvc;
using BookingSystem.Data;
using BookingSystem.Models;
using System.Threading.Tasks;
using System;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Controllers
{
    [Route("bookings")] // Добавляем атрибут маршрута к контроллеру
    public class BookingController : Controller
    {
        private readonly BookingContext _context;

        public BookingController(BookingContext context)
        {
            _context = context;
        }

        // Маршрут будет выглядеть как /bookings/create
        [HttpGet("create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(Booking booking)
        {
            if (string.IsNullOrEmpty(booking.Location))
            {
                booking.Location = "InCity";
                Console.WriteLine("Местоположение доставки не указано. Устанавливаем значение по умолчанию: InCity");
            }

            if (!ModelState.IsValid)
            {
                return View(booking);
            }

            _context.Add(booking);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Маршрут будет выглядеть как /bookings
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var bookings = await _context.Bookings.ToListAsync();
            return View(bookings);
        }
    }
}
