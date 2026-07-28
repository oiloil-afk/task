using LoginForm.Data;
using LoginForm.Interfaces;
using LoginForm.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task2.ViewModel;

namespace LoginForm.Services
{
    public class EmployeeService : IEmployee

        
    {
        public readonly ApplicationDbContext _applicationDbContext;
        public readonly IPasswordHasher<Employee> _passwordHasher;
        public EmployeeService (ApplicationDbContext applicationDbContext, IPasswordHasher<Employee> passwordHasher)
        {
            _applicationDbContext = applicationDbContext;
            _passwordHasher = passwordHasher;
        }
        public List<Employee> GetEmployee()
        {
            return _applicationDbContext.Employees.Include(e => e.EducationDetails).ToList();
        }

       public List<Department> GetDepartment()

        {
            return _applicationDbContext.Departments.ToList();
        }

        public void AddEmployee(Employee employee)
        {
            _applicationDbContext.Employees.Add(employee);
            _applicationDbContext.SaveChanges();
        }

        public Employee? GetEdit(Guid id)
        {
            return _applicationDbContext.Employees
                .Include(e => e.EducationDetails)
                .FirstOrDefault(e => e.Id == id);
        }


        public bool UpdateEmployee(CreateEmployeeViewModel viewModel)
        {
            var employee = _applicationDbContext.Employees
                .Include(e => e.EducationDetails)
                .FirstOrDefault(e => e.Id == viewModel.Employee.Id);

            if (employee == null)
            {
                return false;
            }

            employee.EmployeeCode = viewModel.Employee.EmployeeCode;
            employee.Name = viewModel.Employee.Name;
            employee.Email = viewModel.Employee.Email;
            employee.Password = _passwordHasher.HashPassword(employee, viewModel.Employee.Password);
            employee.Phone = viewModel.Employee.Phone;
            employee.Age = viewModel.Employee.Age;
            employee.DateOfBirth = viewModel.Employee.DateOfBirth;
            employee.DateOfJoining = viewModel.Employee.DateOfJoining;
            employee.OfficeTime = viewModel.Employee.OfficeTime;
            employee.Gender = viewModel.Employee.Gender;

            if (viewModel.EducationDetails != null)
            {
                foreach (var edu in viewModel.EducationDetails)
                {
                    if (edu.Id != Guid.Empty)
                    {
                        var existing = employee.EducationDetails.FirstOrDefault(x => x.Id == edu.Id);

                        if (existing != null)
                        {
                            existing.Institution = edu.Institution;
                            existing.Degree = edu.Degree;
                            existing.Percentage = edu.Percentage;
                            existing.YearOfPassing = edu.YearOfPassing;
                            existing.From = edu.From;
                            existing.To = edu.To;
                        }
                    }
                }
            }

            _applicationDbContext.SaveChanges();
            return true;
        }

        public Employee? GetDelete(Guid id)
        {
             return _applicationDbContext.Employees.Find(id);

        }

        public bool DeleteEmployee(Guid id)
        {
            var employee = _applicationDbContext.Employees.Find(id);
            if (employee == null)
            {
                return false;
            }

            _applicationDbContext.Employees.Remove(employee);
            _applicationDbContext.SaveChanges();
            return true;
        }

        public Employee? GetDetails(Guid id)
        {
            return _applicationDbContext.Employees
                .Include(e => e.EducationDetails)
                .FirstOrDefault(e => e.Id == id);
        }
    }
}
