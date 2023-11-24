using System.ComponentModel.DataAnnotations;

namespace Demo.Models
{
    public class Employee
    {
        [Key]
        public int EmpId { get;set; }   
        public string EmpName { get;set; }
        public string Mobno { get;set; }    
        public string Gender { get;set; } 
        public double Salary {  get;set; }

        public int DepId { get;set; }
        public int CountryId { get;set; }   
        public int StateId { get; set;   }
        public int CityId { get;set; }  
        public Department Department { get;set; }
        public Country Country { get;set; }
        public State State { get; set; }    
        public City City { get;set; }
    }
}
