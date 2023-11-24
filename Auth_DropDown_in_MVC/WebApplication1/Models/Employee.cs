using WebApplication1.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebApplication1.Models
{
    public class Employee
    {
        [Key]
        public int Emp_Id { get; set; }

        [Required(ErrorMessage = "This Field is Required")]
        [StringLength(50)]
        public string Emp_Name { get; set; }

        [Required(ErrorMessage = "This Field is Required")]
        [StringLength(50)]
        public string Emp_Adress { get; set; }

        [Required(ErrorMessage = "This Field is Required")]
        [StringLength(50)]
        public string mobno { get; set; }
        [Required(ErrorMessage = "This Field is Required")]

        public int Sallary { get; set; }
        [Required(ErrorMessage = "This Field is Required")]
        public int age { get; set; }
        public int Dept_Id { get; set; }
        public int StateId { get; set; }

        public int CityId { get; set; }


        public Department department { get; set; }
        public City City { get; set; }
        public State State { get; set; }
      

    }
}

