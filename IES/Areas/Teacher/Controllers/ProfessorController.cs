using IES.Data;
using IES.Data.DAL.Registrations;
using IES.Data.DAL.Teacher;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Model.Teacher;

namespace IES.Areas.Teacher.Controllers
{
    [Area("Teacher")]
    public class ProfessorController : Controller
    {
        private readonly IESContext _context;
        private readonly InstitutionDAL _institutionDAL;
        private readonly DepartmentDAL _departmentDAL;
        private readonly CourseDAL _courseDAL;
        private readonly ProfessorDAL _professorDAL;

        public ProfessorController(IESContext context)
        {
            _context = context;
            _institutionDAL = new InstitutionDAL(_context);
            _departmentDAL = new DepartmentDAL(_context);
            _courseDAL = new CourseDAL(_context);
            _professorDAL = new ProfessorDAL(_context);
        }

        public async Task<IActionResult> Index()
        {
            return View(await _professorDAL.GetProfessorOrderedByName().ToListAsync());
        }

        public async Task<IActionResult> Details(long? id)
        {
            return await GetViewProfessorById(id);
        }

        #region create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProfessorId, Name")] Professor professor)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _professorDAL.SaveProfessor(professor);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                ModelState.AddModelError("", "Wasn't possible to save data"); ;
            }
            return View(professor);
        }
        #endregion

        #region edit
        public async Task<IActionResult> Edit(long? id)
        {
            return await GetViewProfessorById(id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long? id, [Bind("ProfessorId, Name")] Professor professor)
        {
            if (id != professor.ProfessorId)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    await _professorDAL.SaveProfessor(professor);
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    ModelState.AddModelError("", "Wasn't possible to save data");
                }
            }
            return View(professor);
        }


        #endregion

        #region delete
        public async Task<IActionResult> Delete (long? id)
        {
            return await GetViewProfessorById(id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(long? id, [Bind("ProfessorId, Name")] Professor professor)
        {
            await _professorDAL.DeleteProfessor(professor);
            return RedirectToAction(nameof(Index));
        }

        #endregion

        private async Task<IActionResult> GetViewProfessorById(long? id)
        {
            if (id == null)
                return NotFound();

            var professor = await _professorDAL.GetProfessorById(id);

            if (professor.ProfessorId == null)
                return NotFound();

            return View(professor);
        }

    }
}
