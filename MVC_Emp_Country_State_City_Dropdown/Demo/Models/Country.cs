using System.ComponentModel.DataAnnotations;
using System.Drawing.Printing;

namespace Demo.Models
{
    public class Country
    {
        [Key]
        public int CountryId { get; set; }
        public string CountryName { get; set; } 

        public IList<State> states { get; set; }
      //  public IList<City> citys { get; set; }
        public IList<Employee> employees { get; set; }
    }
}
