using System.ComponentModel.DataAnnotations;

namespace Demo.Models
{
    public class Department
    {
        [Key]
        public int DepId { get; set; }
        public string DepName { get; set; } 

        public IList<Employee> Employees { get; set; }
    }
}
