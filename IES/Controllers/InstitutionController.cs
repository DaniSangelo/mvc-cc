using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Model.Registrations;
using System.Linq;
using IES.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using IES.Data.DAL.Registrations;

namespace IES.Controllers
{
    public class InstitutionController : Controller
    {
        private static IList<Institution> _institutions = new List<Institution>()
        {
            new Institution()
            {
                InstitutionId = 1,
                Name = "UniParaná",
                Address="Paraná"
            },
            new Institution()
            {
                InstitutionId = 2,
                Name = "UniSanta",
                Address="Santa Catarina"
            },
            new Institution()
            {
                InstitutionId = 3,
                Name = "UniSãoPaulo",
                Address="São Paulo"
            },
            new Institution()
            {
                InstitutionId = 4,
                Name = "UniSulgrandense",
                Address="Rio Grande do Sul"
            },
            new Institution()
            {
                InstitutionId = 5,
                Name = "UniCarioca",
                Address="Rio de Janeiro"
            },
            new Institution()
            {
                InstitutionId = 6,
                Name = "UniHorizonte",
                Address="Minas Gerais"
            }
        };

        private readonly IESContext _context;
        private readonly InstitutionDAL _institutionDAL;

        public InstitutionController(IESContext context)
        {
            _context = context;
            _institutionDAL = new InstitutionDAL(context);
        }

        public async Task<IActionResult> Index()
        {
            return View(await _institutionDAL.GetInstitutionsOrderedByName().ToListAsync());
        }

        #region create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("Name, Address")] Institution institution)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _institutionDAL.SaveInstitution(institution);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Wasn't possible to save data");
            }
            return View(institution);
        }
        #endregion

        #region edit
        public async Task<ActionResult> Edit(long id)
        {
            return View(await GetInstitutionById(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(long? id, [Bind("InstitutionId, Name, Address")] Institution institution)
        {
            if (id != institution.InstitutionId)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    await _institutionDAL.SaveInstitution(institution);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await InstitutionExists(institution.InstitutionId))
                        return NotFound();
                    else
                        throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        #endregion

        public async Task<ActionResult> Details(long id)
        {            
            return View(await GetInstitutionById(id));
        }

        #region delete
        public async Task<ActionResult> Delete(long id)
        {
            return View(await GetInstitutionById(id));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Institution institution)
        {
            var obj = await _institutionDAL.DeleteInstitution(institution.InstitutionId);
            TempData["Message"] = "Institution " + obj.Name.ToUpper() + " was deleted";
            return RedirectToAction(nameof(Index));
        }
        #endregion

        private async Task<IActionResult> GetInstitutionById(long? id)
        {
            if (id == null)
                return NotFound();

            var institution = await _institutionDAL.GetInstitutionById(id);

            if (institution == null)
                return NotFound();

            return View(institution);
        }
        
        private async Task<bool> InstitutionExists(long? id)
        {
            return await _institutionDAL.GetInstitutionById(id) != null;
        }
    }
}