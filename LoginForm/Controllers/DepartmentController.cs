using LoginForm.Data;
using LoginForm.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LoginForm.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {
        public readonly ApplicationDbContext _dataContext;

        public DepartmentController(ApplicationDbContext dataContext)
        {
            _dataContext = dataContext;
        }
        [HttpGet]
        public IActionResult Index(Department department)
        {
            var dep = _dataContext.Departments.ToList();
            return View(dep);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Department department)
        {
            if (ModelState.IsValid && department != null)
            {
                department.Createdby = User.Identity.Name;
                department.CreatedDate = DateOnly.FromDateTime(DateTime.Now);
                _dataContext.Departments.Add(department);
                _dataContext.SaveChanges();
                return Redirect("Index");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Edit(Guid Id)
        {
            var dep = _dataContext.Departments.FirstOrDefault(x => x.Id == Id);
            return View(dep);
        }

        [HttpPost]
        public IActionResult Edit(Department department)
        {
            if (ModelState.IsValid && department != null)
            {
                try
                {
             
                    var exist = _dataContext.Departments.FirstOrDefault(x => x.Id == department.Id);

                    if (exist != null)
                    {
                       
                        exist.Dep_id = department.Dep_id;
                        exist.code = department.code;
                        exist.Name = department.Name;
               
                        exist.Editedby = User.Identity?.Name;
                        exist.EditedDate = DateOnly.FromDateTime(DateTime.Now);
                        exist.isActive = department.isActive;

                        _dataContext.Departments.Update(exist);
                        _dataContext.SaveChanges();

                        return RedirectToAction("Index");
                    }

                    else
                    {
                        ModelState.AddModelError("", "Department not found!");
                    }
                }
                catch (DbUpdateException ex)
                {
                    ModelState.AddModelError("", "An error occurred while updating the department: " + ex.Message);
                }
            }

            return View(department);
        }

        [HttpGet]
        public IActionResult Delete(Guid Id)
        {
            var dep = _dataContext.Departments.FirstOrDefault(x => x.Id == Id);
            if (dep == null)
            {
                return NotFound();
            }
            return View(dep);
        }

        [HttpPost]
        public IActionResult Delete(Guid Id, Department department)
        {
            try
            {
                var exist = _dataContext.Departments.FirstOrDefault(x => x.Id == Id);

                if (exist != null)
                {
                    _dataContext.Departments.Remove(exist);
                    _dataContext.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Department not found!");
                }
            }
            catch (DbUpdateException ex)
            {
                ModelState.AddModelError("", "An error occurred while deleting the department: " + ex.Message);
            }
            return View(department);
        }
    }
}
