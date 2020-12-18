using Microsoft.AspNetCore.Mvc.Rendering;
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
            return View(await _context.Departments
                        .Include(i => i.Institution)
                        .OrderBy(d => d.Name).ToListAsync());
        }
        #region create
        public IActionResult Create()
        {
            var institutions = _context.Institutions.OrderBy(i => i.Name).ToList();
            institutions.Insert(0, new Institution() { InstitutionId = 0, Name = "Select the institution" });
            ViewBag.Institutions = institutions;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("Name, InstitutionId")] Department department)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(department);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Wasn't possible to insert data");
            }
            return View(department);
        }
        #endregion

        #region edit
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
                return NotFound();

            var department = await _context.Departments.SingleOrDefaultAsync(d => d.DepartmentId == id);

            if (department == null)
                return NotFound();

            ViewBag.Institutions = new SelectList(_context.Institutions.OrderBy(
                i => i.Name), "InstitutionId", "Name", department.InstitutionId);

            return View(department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long? id, [Bind("DepartmentId, Name, InstitutionId")] Department department)
        {
            if (id != department.DepartmentId)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(department);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartmentExists(department.DepartmentId))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        public bool DepartmentExists(long? id)
        {
            return _context.Departments.Any(d => d.DepartmentId == id);
        }

        #endregion

        #region delete
        public async Task<IActionResult> Delete(long? id)
        {
            var department = await _context.Departments.SingleOrDefaultAsync(d => d.DepartmentId == id);

            if (department == null)
                return NotFound();

            return View(await _context.Departments.Where(d => d.DepartmentId == id).FirstAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(long? id, [Bind("DepartmentId, Name")] Department department)
        {
            if (department == null)
                return NotFound();
            
            if (ModelState.IsValid)
            {
                var obj = await _context.Departments.SingleOrDefaultAsync(d => d.DepartmentId == id);
                _context.Departments.Remove(obj);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(department);

        }

        #endregion
        public ActionResult Details(long? id)
        {
            var department = _context.Departments.Include(d => d.Institution).SingleOrDefault(d => d.DepartmentId == id);
            return View(department);
        }
    }
}
