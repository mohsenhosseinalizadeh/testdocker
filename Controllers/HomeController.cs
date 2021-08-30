using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using testdocker1.Models;

namespace testdocker1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private MoviesContext _context;
        public HomeController(ILogger<HomeController> logger, MoviesContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Movie m)
        {
            if (ModelState.IsValid)
            {
                _context.Add(m);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            else
                return View();
        }

        public IActionResult Index()
        {
            var m = _context.Movies.ToList();
            return View(m);
        }

        public IActionResult Update(int id)
        {
            var pc = _context.Movies.Where(a => a.ID == id).FirstOrDefault();
            return View(pc);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Movie m)
        {
            if (ModelState.IsValid)
            {
                _context.Update(m);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            else
                return View(m);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var pc = _context.Movies.Where(a => a.ID == id).FirstOrDefault();
            _context.Remove(pc);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
