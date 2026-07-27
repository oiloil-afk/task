using LoginForm.Models;
using Microsoft.AspNetCore.Mvc;

namespace LoginForm.Interfaces
{
    public   interface IDepartment
    {
        List<Department> GetAllDepartment();

        void AddDepartment(Department department);

        Department GetEdit(Guid Id);
    }
}
