using BCrypt.Net;
using LoginForm.Data;
using LoginForm.Interfaces;
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

        public readonly IEmployee _employee;
        public EmployeeController(ApplicationDbContext context,IPasswordHasher<Employee> passwordHasher , IEmployee employee)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _employee = employee;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var employees = _employee.GetEmployee();
            return View(employees);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Departments = _employee.GetDepartment();
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

                _employee.AddEmployee(employee);

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
            var employee = _employee.GetEdit(id);

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

            bool updated = _employee.UpdateEmployee(viewModel);

            if (!updated)
            {
                return Json(new { success = false, message = "Employee not found" });
            }

            return Json(new { success = true, message = "Employee Updated Successfully" });
        }

        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            var employee = _employee.GetDelete(id);
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
            bool deleted = _employee.DeleteEmployee(id);

            if (deleted)
            {
                return Json(new { success = true, message = "Employee Deleted Successfully" });
            }

            return Json(new { success = false, message = "Employee not found" });
        }

        [HttpGet]
        public IActionResult Details(Guid id)
        {
            var employee = _employee.GetDetails(id);
            if (employee == null)
            {
                return NotFound();
            }
            return PartialView("Details", employee);
        }


    }
}
