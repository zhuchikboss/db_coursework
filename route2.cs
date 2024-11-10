// Commit message: Configure conventional routing in Startup.cs and add named route for Create action

// Изменения в BookingController.cs
using Microsoft.AspNetCore.Mvc;
using BookingSystem.Data;
using BookingSystem.Models;
using System.Threading.Tasks;
using System;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Controllers
{
    [Route("bookings")]
    public class BookingController : Controller
    {
        private readonly BookingContext _context;

        public BookingController(BookingContext context)
        {
            _context = context;
        }

        [HttpGet("create", Name = "CreateBooking")] // Добавляем имя маршрута
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
            }

            if (!ModelState.IsValid)
            {
                return View(booking);
            }

            _context.Add(booking);
            await _context.SaveChangesAsync();

            // Используем имя маршрута для перенаправления
            return RedirectToRoute("CreateBooking");
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var bookings = await _context.Bookings.ToListAsync();
            return View(bookings);
        }
    }
}

// Изменения в Startup.cs
public void Configure(IApplicationBuilder app, IHostingEnvironment env)
{
    if (env.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }

    app.UseRouting();

    app.UseEndpoints(endpoints =>
    {
        // Конвенционная маршрутизация
        endpoints.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
    });
}
