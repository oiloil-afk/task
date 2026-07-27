using LoginForm.Data;
using LoginForm.Interfaces;
using LoginForm.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LoginForm.Services
{
    public class DepartmentService : IDepartment
    {
        public readonly ApplicationDbContext applicationDbContext;

        public DepartmentService(ApplicationDbContext _applicationDbContext)
        {
            applicationDbContext = _applicationDbContext;
        }


        public List<Department> GetAllDepartment()
        {
            return applicationDbContext.Departments.ToList();
        }

       public void AddDepartment(Department department)
        {
            applicationDbContext.Departments.Add(department);
            applicationDbContext.SaveChanges();
        }

        public Department GetEdit(Guid id)
        {
            return applicationDbContext.Departments.FirstOrDefault(x => x.Id == id);
        }
    }
}
