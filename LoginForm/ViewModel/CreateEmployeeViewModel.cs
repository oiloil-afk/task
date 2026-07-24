using LoginForm.Models;
using LoginForm.ViewModel;


namespace Task2.ViewModel
{
    public class CreateEmployeeViewModel
    {
        public EmployeeViewModel Employee { get; set; } = new EmployeeViewModel();
        public List<EducationDetailViewModel> EducationDetails { get; set; } = new List<EducationDetailViewModel>();
        public List<Department> Departments { get; set; } = new List<Department>();


        public Employee ToEmployeeEntity()
        {
            var employee = new Employee
            {
                Id = this.Employee.Id == Guid.Empty ? Guid.NewGuid() : this.Employee.Id,
                EmployeeCode = this.Employee.EmployeeCode,
                Name = this.Employee.Name,
                Email = this.Employee.Email,
                Password = this.Employee.Password,
                Phone = this.Employee.Phone,
                Age = this.Employee.Age,
                DateOfBirth = this.Employee.DateOfBirth,
                DateOfJoining = this.Employee.DateOfJoining,
                OfficeTime = this.Employee.OfficeTime,
                Gender = this.Employee.Gender,
                Department_Id = this.Employee.Department_Id,
                EducationDetails = new List<EducationDetail>()
            };

            // Map education details
            if (this.EducationDetails != null && this.EducationDetails.Count > 0)
            {
                foreach (var edu in this.EducationDetails)
                {
                    // Only add non-empty education details
                    if (!string.IsNullOrWhiteSpace(edu.Institution) && !string.IsNullOrWhiteSpace(edu.Degree))
                    {
                        employee.EducationDetails.Add(new EducationDetail
                        {
                            Id = Guid.NewGuid(),
                            Institution = edu.Institution,
                            Degree = edu.Degree,
                            Percentage = edu.Percentage,
                            YearOfPassing = edu.YearOfPassing,
                            From = edu.From,
                            To = edu.To,
                            EmployeeId = employee.Id
                        });
                    }
                }
            }

            return employee;
        }

        // Helper method to convert Entity to ViewModel
        public static CreateEmployeeViewModel FromEntity(Employee employee)
        {
            var viewModel = new CreateEmployeeViewModel
            {
                Employee = new EmployeeViewModel
                {
                    Id = employee.Id,
                    EmployeeCode = employee.EmployeeCode,
                    Name = employee.Name,
                    Email = employee.Email,
                    Password = employee.Password,
                    Phone = employee.Phone,
                    Age = employee.Age,
                    DateOfBirth = employee.DateOfBirth,
                    DateOfJoining = employee.DateOfJoining,
                    OfficeTime = employee.OfficeTime,
                    Gender = employee.Gender,
                    Department_Id = employee.Department_Id
                },
                EducationDetails = new List<EducationDetailViewModel>()
            };

            // Map education details
            if (employee.EducationDetails != null)
            {
                foreach (var edu in employee.EducationDetails)
                {
                    viewModel.EducationDetails.Add(new EducationDetailViewModel
                    {
                        Id = edu.Id,
                        Institution = edu.Institution,
                        Degree = edu.Degree,
                        Percentage = edu.Percentage,
                        YearOfPassing = edu.YearOfPassing,
                        From = edu.From,
                        To = edu.To
                    });
                }
            }

            return viewModel;
        }
    }
}
