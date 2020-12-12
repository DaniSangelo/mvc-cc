using IES.Models;
using System.Linq;

namespace IES.Data
{
    public class IESDbInitializer
    {
        public static void Initialize(IESContext context)
        {
            context.Database.EnsureCreated();
            if (context.Departments.Any())
                return;

            var departments = new Department[]
            {
                new Department{Name="Ciência da Computação"},
                new Department{Name="Engenharia de Alimentos"}
            };

            foreach(var d in departments)
            {
                context.Departments.Add(d);
            }
            context.SaveChanges();
        }
    }
}
