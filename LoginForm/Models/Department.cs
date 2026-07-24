using System.ComponentModel.DataAnnotations;

namespace LoginForm.Models
{
    public class Department
    {
        public Guid Id { get; set; }
        [Display(Name = "Departmant Id")]
        public int Dep_id { get; set; }
        [Display(Name = "Code")]
        public int code { get; set; }
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "Created By")]

        public string Createdby { get; set; }
        [Display(Name = "Created Date")]

        public DateOnly CreatedDate { get; set; }
        [Display(Name = "Edited By")]
        public string Editedby { get; set; }
        [Display(Name = "Edited Date")]
        public DateOnly EditedDate { get; set; }

        [Display(Name = "Is Active")]
        public Boolean isActive { get; set; }

        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}
