// Commit message: Add weight validation and implement Index action for booking list

using Microsoft.AspNetCore.Mvc;
using BookingSystem.Data;
using BookingSystem.Models;
using System.Threading.Tasks;
using System;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Controllers
{
    public class BookingController : Controller
    {
        private readonly BookingContext _context;

        public BookingController(BookingContext context)
        {
            _context = context;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Booking booking)
        {
            if (string.IsNullOrEmpty(booking.Location))
            {
                booking.Location = "InCity";
                Console.WriteLine("Местоположение доставки не указано. Устанавливаем значение по умолчанию: InCity");
            }

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                foreach (var error in errors)
                {
                    Console.WriteLine($"Ошибка валидации: {error}");
                }

                return View(booking);
            }

            if (booking.Weight > 10)
            {
                ModelState.AddModelError("Weight", "Вес посылки не должен превышать 10 кг.");
                return View(booking);
            }

            _context.Add(booking);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Index()
        {
            var bookings = await _context.Bookings.ToListAsync();
            return View(bookings);
        }
    }
}
