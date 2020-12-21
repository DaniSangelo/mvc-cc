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
        public async Task<ActionResult> Create(Institution institution)
        {
            _context.Add(institution);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion

        #region edit
        public async Task<ActionResult> Edit(long id)
        {
            return View(GetInstitutionById(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Institution institution)
        {
            _context.Update(institution);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion

        public async Task<ActionResult> Details(long id)
        {            
            return View(GetInstitutionById(id));
        }

        #region delete
        public async Task<ActionResult> Delete(long id)
        {
            return View(GetInstitutionById(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Institution institution)
        {
            var obj = await _institutionDAL.GetInstitutionsById(institution.InstitutionId);
            _context.Institutions.Remove(obj);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion

        private async Task<IActionResult> GetInstitutionById(long? id)
        {
            if (id == null)
                return NotFound();

            var institution = await _institutionDAL.GetInstitutionsById(id);

            if (institution == null)
                return NotFound();

            return View(institution);
        }
    }
}