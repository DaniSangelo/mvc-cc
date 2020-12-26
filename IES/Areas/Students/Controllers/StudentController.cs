using Microsoft.AspNetCore.Mvc;
using Model.Students;
using IES.Data.DAL.Students;
using System.Threading.Tasks;
using IES.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace IES.Areas.Students.Controllers
{
    [Area("Students")]
    [Authorize]
    public class StudentController : Controller
    {
        private readonly IESContext _context;
        private readonly StudentDAL _studentDAL;

        public StudentController(IESContext context)
        {
            _context = context;
            _studentDAL = new StudentDAL(context);
        }

        public async Task<IActionResult> Index()
        {
            return View(await _studentDAL.GetStudentsClassifiedByName().ToListAsync());
        }

        public async Task<IActionResult> Details(long id)
        {
            return await GetStudentViewById(id);
        }

        #region create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name, StudentRegister, BirthDate")] Student student)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _studentDAL.SaveStudent(student);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                ModelState.AddModelError("", "Wasn't possible to save data");
            }
            return View(student);
        }
        #endregion

        #region edit

        public async Task<IActionResult> Edit(long? id)
        {
            return await GetStudentViewById(id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long? id, [Bind("StudentId, StudentRegister, Name, BirthDate")] Student student)
        {
            if (id != student.StudentId)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    await _studentDAL.SaveStudent(student);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if(!await HasStudent(student.StudentId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }
        #endregion

        #region delete
        public async Task<IActionResult> Delete(long? id)
        {
            return await GetStudentViewById(id);
        }

        public async Task<IActionResult> ConfirmedDelete(long? id)
        {
            _ = await _studentDAL.DeleteStudent((long)id);
            return RedirectToAction(nameof(Index));
        }

        #endregion

        private async Task<IActionResult> GetStudentViewById(long? id)
        {
            if (id == null)
                return NotFound();

            var student = await _studentDAL.GetStudentById((long) id);
            
            if (student.StudentId == null)
                return NotFound();

            return View(student);
        }

        public async Task<bool> HasStudent(long? id)
        {
            return await _studentDAL.GetStudentById((long)id) != null;
        }
    }
}
