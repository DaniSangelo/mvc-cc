using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using IES.Models;

namespace IES.Controllers
{
    public class InstitutionController1 : Controller
    {
        private static IList<Institution> _institutions = new List<Institution>()
        {
            new Institution()
            {
                InstitutionId = 1,
                Name = "UniParaná",
                Endereco="Paraná"
            },
            new Institution()
            {
                InstitutionId = 2,
                Name = "UniSanta",
                Endereco="Santa Catarina"
            },
            new Institution()
            {
                InstitutionId = 3,
                Name = "UniSãoPaulo",
                Endereco="São Paulo"
            },
            new Institution()
            {
                InstitutionId = 4,
                Name = "UniSulgrandense",
                Endereco="Rio Grande do Sul"
            },
            new Institution()
            {
                InstitutionId = 5,
                Name = "UniCarioca",
                Endereco="Rio de Janeiro"
            },
            new Institution()
            {
                InstitutionId = 6,
                Name = "UniHorizonte",
                Endereco="Minas Gerais"
            }
        };

        public IActionResult Index()
        {
            return View(_institutions);
        }
    }
}