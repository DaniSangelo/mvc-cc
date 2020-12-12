using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using IES.Models;
using System.Linq;

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

        public IActionResult Index()
        {
            return View(_institutions);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Institution institution)
        {
            _institutions.Add(institution);
            institution.InstitutionId = _institutions.Select(i => i.InstitutionId).Max() + 1;
            return RedirectToAction("Index");
        }

        public ActionResult Edit(long id)
        {
            return View(_institutions.Where(i => i.InstitutionId == id).First());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Institution institution)
        {
            var id = institution.InstitutionId;
            _institutions[_institutions.IndexOf(_institutions.Where(i => i.InstitutionId == id).First())] = institution;
            return RedirectToAction("Index");
        }

        public ActionResult Details(long id)
        {
            return View(_institutions.Where(i => i.InstitutionId == id).First());
        }

        public ActionResult Delete(long id)
        {
            return View(_institutions.Where(i => i.InstitutionId == id).First());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Institution institution)
        {
            _institutions.Remove(_institutions.Where(i => i.InstitutionId == institution.InstitutionId).First());
            return RedirectToAction(nameof(Index));
        }
    }
}