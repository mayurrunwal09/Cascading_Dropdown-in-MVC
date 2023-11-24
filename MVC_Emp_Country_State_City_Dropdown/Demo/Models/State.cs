using System.ComponentModel.DataAnnotations;

namespace Demo.Models
{
    public class State
    {
        [Key]
        public int StateId { get; set; }
        public string StateName { get; set; }

        public int CountryId { get; set; }
        public Country Country { get; set; }

        public IList<City> City { get; set; }
        public IList<Employee> Employees { get; set; }
    }
}
