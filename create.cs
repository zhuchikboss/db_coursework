// Commit message: Initialize BookingController with basic structure

using Microsoft.AspNetCore.Mvc;
using BookingSystem.Data;

namespace BookingSystem.Controllers
{
    public class BookingController : Controller
    {
        private readonly BookingContext _context;

        public BookingController(BookingContext context)
        {
            _context = context;
        }

        // Placeholder for Create action
        public IActionResult Create()
        {
            return View();
        }
    }
}
