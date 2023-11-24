using WebApplication1.Models;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class State
    {
        [Key]
        public int StateId { get; set; }

        [Required(ErrorMessage = "This Field is Required")]
        [StringLength(50)]
        public string StateName { get; set; }

        public ICollection<City> Cities { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
}
