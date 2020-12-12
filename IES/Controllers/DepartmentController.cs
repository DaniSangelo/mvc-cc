using IES.Data;
using IES.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IES.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IESContext _context;

        public DepartmentController(IESContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Departments.OrderBy(d => d.Name).ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("Name")] Department department)
        {
            try
            {
                if (ModelState.IsValid) {
                    _context.Add(department);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }catch(DbUpdateException)
            {
                ModelState.AddModelError("", "Wasn't possible to insert data");
            }
            return View(department);
        }

    }
}
