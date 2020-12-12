using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using IES.Models;
using System.Linq;
using IES.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

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

        public InstitutionController(IESContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Institutions.ToListAsync());
        }

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

        public async Task<ActionResult> Edit(long id)
        {
            return View(await _context.Institutions.Where(i => i.InstitutionId == id).FirstAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Institution institution)
        {
            _context.Update(institution);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Details(long id)
        {
            return View(await _context.Institutions.Where(i => i.InstitutionId == id).FirstAsync());
        }

        public async Task<ActionResult> Delete(long id)
        {
            var obj = await _context.Institutions.FindAsync(id);
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Institution institution)
        {
            var obj = await _context.Institutions.Where(i => i.InstitutionId == institution.InstitutionId).FirstAsync();
            _context.Institutions.Remove(obj);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}