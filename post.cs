// Commit message: Add Create POST action method for booking

using Microsoft.AspNetCore.Mvc;
using BookingSystem.Data;
using BookingSystem.Models;
using System.Threading.Tasks;

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
            // Placeholder for saving booking data
            _context.Add(booking);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
