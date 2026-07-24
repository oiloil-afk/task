using System.ComponentModel.DataAnnotations;

namespace LoginForm.ViewModel
{
    public class EmployeeViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Code")]
        public int EmployeeCode { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Phone]
        [Display(Name = "Phone")]
        public string Phone { get; set; }

        [Required]
        [Display(Name = "Age")]
        public int Age { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date Of Birth")]
        public DateOnly DateOfBirth { get; set; }

        [Display(Name = "Date Of Joining")]
        [DataType(DataType.Date)]
        public DateOnly DateOfJoining { get; set; }

        [DataType(DataType.Time)]
        [Display(Name = "Office Time")]
        public TimeOnly OfficeTime { get; set; }

        [Display(Name = "Gender")]
        public string Gender { get; set; }

        public Guid? Department_Id { get; set; }
    }
}
