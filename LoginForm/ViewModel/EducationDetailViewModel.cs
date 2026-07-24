using System.ComponentModel.DataAnnotations;

namespace LoginForm.ViewModel
{
    public class EducationDetailViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Institution")]
        public string Institution { get; set; }

        [Display(Name = "Degree")]
        public string Degree { get; set; }

        [Display(Name = "Percentage")]
        public decimal Percentage { get; set; }

        [Display(Name = "Year Of Passing")]
        public int YearOfPassing { get; set; }

        [Display(Name = "From")]
        [DataType(DataType.Date)]
        public DateOnly? From { get; set; }

        [Display(Name = "To")]
        [DataType(DataType.Date)]
        public DateOnly? To { get; set; }
    }
}
