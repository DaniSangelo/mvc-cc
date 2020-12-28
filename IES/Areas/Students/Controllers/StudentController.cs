using Microsoft.AspNetCore.Mvc;
using Model.Students;
using IES.Data.DAL.Students;
using System.Threading.Tasks;
using IES.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;

namespace IES.Areas.Students.Controllers
{
    [Area("Students")]
    [Authorize]
    public class StudentController : Controller
    {
        private readonly IESContext _context;
        private readonly StudentDAL _studentDAL;
        private readonly IHostingEnvironment _hostingEnvironment;

        public StudentController(IESContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _studentDAL = new StudentDAL(context);
            _hostingEnvironment = hostingEnvironment;
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
        public async Task<IActionResult> Edit(long? id, 
            [Bind("StudentId, StudentRegister, Name, BirthDate")] Student student,
            IFormFile photo)
        {
            if (id != student.StudentId)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var stream = new MemoryStream();
                    await photo.CopyToAsync(stream);
                    student.Photo = stream.ToArray();
                    student.PhotoMimeType = photo.ContentType;

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

        public async Task<FileContentResult> GetPhoto(long id)
        {
            Student student = await _studentDAL.GetStudentById(id);
            if (student != null)
                return File(student.Photo, student.PhotoMimeType);

            return null;
        }

        public async Task<FileResult> DownloadPhoto(long id)
        {
            Student student = await _studentDAL.GetStudentById(id);
            string fileName = "Photo " + student.StudentId.ToString().Trim() + ".jpg";
            using (FileStream fs = new FileStream(Path.Combine(_hostingEnvironment.WebRootPath, fileName), FileMode.Create, FileAccess.Write))
            {
                fs.Write(student.Photo, 0, student.Photo.Length);
            }
            IFileProvider provider = new PhysicalFileProvider(_hostingEnvironment.WebRootPath);
            IFileInfo fileInfo = provider.GetFileInfo(fileName);
            var rs = fileInfo.CreateReadStream();
            return File(rs, student.PhotoMimeType, fileName);
        }
    }
}
