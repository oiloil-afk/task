using LoginForm.Models;
using Microsoft.AspNetCore.Mvc;
using Task2.ViewModel;

namespace LoginForm.Interfaces
{
    public interface IEmployee
    {
        List<Employee> GetEmployee();

        List<Department> GetDepartment();

        void AddEmployee(Employee employee);

        Employee? GetEdit(Guid id);

        bool UpdateEmployee(CreateEmployeeViewModel viewModel);

        Employee? GetDelete(Guid id);

        bool DeleteEmployee(Guid id);

        Employee? GetDetails(Guid id);


    }
}
