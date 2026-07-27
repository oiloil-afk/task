using BCrypt.Net;
using LoginForm.Data;
using LoginForm.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task2.ViewModel;

namespace LoginForm.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public readonly IPasswordHasher<Employee> _passwordHasher;
        public EmployeeController(ApplicationDbContext context,IPasswordHasher<Employee> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var employees = _context.Employees.Include(e => e.EducationDetails).ToList();
            return View(employees);
        }



        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Departments = _context.Departments.ToList();
            var viewModel = new CreateEmployeeViewModel();
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Create(CreateEmployeeViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(viewModel);
                }

                string pass = BCrypt.Net.BCrypt.HashPassword(viewModel.Employee.Password);

                viewModel.Employee.Password = pass;

                var employee = viewModel.ToEmployeeEntity();

                _context.Employees.Add(employee);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error: {ex.Message}");
                return View(viewModel);
            }
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var employee = _context.Employees.Include(e => e.EducationDetails).FirstOrDefault(e => e.Id == id);

            if (employee == null)
            {
                return NotFound();
            }

            var viewmodel = CreateEmployeeViewModel.FromEntity(employee);
            return View(viewmodel);
        }

        [HttpPost]
        public IActionResult Edit(CreateEmployeeViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "Validation failed. Please check the form." });
            }

            var employee = _context.Employees.Include(e => e.EducationDetails).FirstOrDefault(e => e.Id == viewModel.Employee.Id);

            employee.EmployeeCode = viewModel.Employee.EmployeeCode;
            employee.Name = viewModel.Employee.Name;
            employee.Email = viewModel.Employee.Email;
            employee.Password = _passwordHasher.HashPassword(employee,viewModel.Employee.Password);
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

            _context.SaveChanges();

            return Json(new { success = true, message = "Employee Updated Successfully" });
        }

        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            var employee = _context.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteCofirmed(Guid id)
        {
            var employee = _context.Employees.Find(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                _context.SaveChanges();
                return Json(new
                {
                    success = true,
                    message = "Employee Deleted Successfully"
                });
            }
            return Json(new
            {
                success = false,
                message = "Employee not found"
            });
        }

        [HttpGet]
        public IActionResult Details(Guid id)
        {
            var employee = _context.Employees.Include(e => e.EducationDetails).FirstOrDefault(e => e.Id == id);
            if (employee == null)
            {
                return NotFound();
            }
            return PartialView("Details", employee);

        }
    }
}
