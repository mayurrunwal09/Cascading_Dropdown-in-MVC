using System.ComponentModel.DataAnnotations;

namespace Demo.Models
{
    public class City
    {
        [Key]
        public int CityId { get; set; }
        public string CityName { get; set; }    

        public int StateId { get; set; }
        public State State {  get; set; }
       // public int CountryId { get; set; }  
        //public Country Country { get; set; }

        public IList<Employee> Employees { get; set; }
    }
}
