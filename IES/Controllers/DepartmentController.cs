using Microsoft.AspNetCore.Mvc.Rendering;
using IES.Data;
using Model.Registrations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using IES.Models.Exceptions;
using IES.Data.DAL.Registrations;
using System;

namespace IES.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IESContext _context;
        private readonly DepartmentDAL _departmentDAL;
        private readonly InstitutionDAL _institutionDAL;

        public DepartmentController(IESContext context)
        {
            _context = context;
            _departmentDAL = new DepartmentDAL(context);
            _institutionDAL = new InstitutionDAL(context);
        }

        public async Task<IActionResult> Index()
        {
            return View(await _departmentDAL.GetDepartmentsClassifiedByName().ToListAsync());
        }

        #region create
        public IActionResult Create()
        {
            var institutions = _institutionDAL.GetInstitutionsOrderedByName().ToList();
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
                if (!await HasInstitution(department.InstitutionId))
                    throw new NoInstitutionException("Institution must be informed.");

                if (ModelState.IsValid)
                {
                    await _departmentDAL.SaveDepartment(department);
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
            ViewResult departmentView = (ViewResult)await GetDepartmentViewById(id);
            Department department = (Department)departmentView.Model;
            ViewBag.Institutions = new SelectList(_institutionDAL.GetInstitutionsOrderedByName(), "InstitutionId", "Name", department.InstitutionId);
            return departmentView;
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
                    await _departmentDAL.SaveDepartment(department);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await DepartmentExists(department.DepartmentId))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Institutions = new SelectList(_institutionDAL.GetInstitutionsOrderedByName(), "InstitutionId", "Name", department.InstitutionId);
            return View(department);
        }


        #endregion

        #region delete
        public async Task<IActionResult> Delete(long? id)
        {
            return await GetDepartmentViewById(id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(long? id, [Bind("DepartmentId, Name")] Department department)
        {
            if (department == null)
                return NotFound();

            if (ModelState.IsValid)
            {
                var obj = await _departmentDAL.DeleteDepartmentById((long)id);
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        #endregion

        public async Task<IActionResult> Details(long? id)
        {
            return await GetDepartmentViewById(id);
        }

        private async Task<bool> HasInstitution(long? id)
        {
            if (id == null || id == 0)
                return false;

            return true;
        }

        public async Task<bool> DepartmentExists(long? id)
        {
            return await _departmentDAL.GetDepartmentById((long) id) != null;
        }

        private async Task<IActionResult> GetDepartmentViewById(long? id)
        {
            if (id == null)
                return NotFound();
            
            var department = await _departmentDAL.GetDepartmentById((long)id);

            if (id == null)
                return NotFound();

            return View(department);
        }
    }
}
