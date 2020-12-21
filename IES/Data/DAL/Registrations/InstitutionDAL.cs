using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Model.Registrations;

namespace IES.Data.DAL.Registrations
{
    public class InstitutionDAL
    {
        private readonly IESContext _context;

        public InstitutionDAL(IESContext context)
        {
            _context = context;
        }

        public IQueryable<Institution> GetInstitutionsOrderedByName()
        {
            return _context.Institutions.OrderBy(i => i.Name);
        }
        
        public async Task<Institution> GetInstitutionById(long? id)
        {
            return await _context.Institutions
                .Include(d => d.Departments)
                .SingleOrDefaultAsync(i => i.InstitutionId == id);
        }

        public async Task<Institution> SaveInstitution(Institution institution)
        {
            if (institution.InstitutionId == null)
                _context.Institutions.Add(institution);
            else
                _context.Update(institution);
            await _context.SaveChangesAsync();
            return institution;
        }
        
        public async Task<Institution> DeleteInstitution(long? id)
        {
            Institution institution = await GetInstitutionById(id);
            _context.Institutions.Remove(institution);
            await _context.SaveChangesAsync();
            return institution;
        }
    }
}
