using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebApplication1.Models
{
    public class Department
    {
        [Key]
        public int Dept_Id { get; set; }
        public string Department_Name { get; set; }

        public ICollection<Employee> Employees { get; set; }
    }
}
