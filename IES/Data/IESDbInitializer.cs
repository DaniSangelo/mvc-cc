using IES.Models;
using System.Linq;

namespace IES.Data
{
    public class IESDbInitializer
    {
        public static void Initialize(IESContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            if (context.Departments.Any())
                return;
            var institutions = new Institution[]
            {
                new Institution{Name="UFMG", Address="Minas Gerais"},
                new Institution{Name="UEMG", Address="Minas Gerais"}
            };

            foreach (Institution i in institutions)
                context.Institutions.Add(i);

            context.SaveChanges();

            var departments = new Department[]
            {
                new Department{Name="Ciência da Computação", InstitutionId = 1},
                new Department{Name="Engenharia de Alimentos", InstitutionId = 2}
            };

            foreach (var d in departments)
            {
                context.Departments.Add(d);
            }
            context.SaveChanges();
        }
    }
}
