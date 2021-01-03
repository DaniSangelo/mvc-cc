using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Model.Registrations;

namespace IES.Data.DAL.Registrations
{
    public class DepartmentDAL
    {
        private IESContext _context;

        public DepartmentDAL(IESContext context)
        {
            _context = context;
        }       

        public IQueryable<Department> GetDepartmentsClassifiedByName()
        {
            return _context.Departments.Include(i => i.Institution).OrderBy(d => d.Name);
        }

        public async Task<Department> GetDepartmentById(long id)
        {
            var department = await _context.Departments.SingleOrDefaultAsync(d => d.DepartmentId == id);
            _context.Institutions.Where(i => department.InstitutionId == i.InstitutionId).Load();
            return department;
        }

        public async Task<Department> SaveDepartment(Department department)
        {
            if (department.DepartmentId == null)
            {
                _context.Departments.Add(department);
            }
            else
            {
                _context.Update(department);
            }
            await _context.SaveChangesAsync();
            return department;
        }

        public async Task<Department> DeleteDepartmentById(long id)
        {
            Department department = await GetDepartmentById(id);
            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();
            return department;
        }

        public IQueryable<Department> GetDepartmentByInstitution(long institutionId)
        {
            var departments = _context.Departments.Where(d => d.InstitutionId == institutionId).OrderBy(d => d.Name);
            return departments;
        }
    }
}
