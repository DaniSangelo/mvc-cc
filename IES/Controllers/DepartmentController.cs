using IES.Data;
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

    }
}
