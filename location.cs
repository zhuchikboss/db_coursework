// Commit message: Add default value for Location and log validation errors

using Microsoft.AspNetCore.Mvc;
using BookingSystem.Data;
using BookingSystem.Models;
using System.Threading.Tasks;
using System;

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
                Console.WriteLine("Ошибки валидации: модель не прошла проверку");
                return View(booking);
            }

            _context.Add(booking);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
