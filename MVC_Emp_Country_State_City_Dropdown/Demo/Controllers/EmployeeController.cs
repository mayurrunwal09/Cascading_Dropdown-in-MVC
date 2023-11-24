using Demo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace Demo.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {



        private readonly MainDBContext _dbContext;
        public EmployeeController(MainDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IActionResult> Index()
        {
            var emp = _dbContext.Employees.Include(d => d.Department).Include(d => d.Country).Include(d => d.State).Include(c => c.City);
            return View(await emp.ToListAsync());
        }

        public async Task<IActionResult> AddOrEdit(int id = 0)
        {
            if (id == 0)
            {
                ViewBag.Countries = _dbContext.Countries.ToList();
                ViewBag.Statements = _dbContext.Statements.ToList();

                ViewData["CountryId"] = new SelectList(_dbContext.Countries, "CountryId", "CountryName");
                ViewData["StateId"] = new SelectList(_dbContext.Statements, "StateId", "StateName");
                ViewData["CityId"] = new SelectList(_dbContext.Cities, "CityId", "CityName");
                ViewData["DepId"] = new SelectList(_dbContext.Departments, "DepId", "DepName");
                return View(new Employee());
            }
            else
            {
                var emp = await _dbContext.Employees.FirstOrDefaultAsync(d => d.EmpId == id);
                if (emp == null)
                {
                    return NotFound();
                }

                ViewBag.Countries = _dbContext.Countries.ToList();
                ViewBag.Statements = _dbContext.Statements.ToList();

                ViewData["CountryId"] = new SelectList(_dbContext.Countries, "CountryId", "CountryName");
                ViewData["StateId"] = new SelectList(_dbContext.Statements, "StateId", "StateName");
                ViewData["CityId"] = new SelectList(_dbContext.Cities, "CityId", "CityName");
                ViewData["DepId"] = new SelectList(_dbContext.Departments, "DepId", "DepName");
                return View(emp);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int id, [Bind("EmpId", "EmpName", "Mobno", "Gender", "Salary", "DepId", "CountryId", "StateId", "CityId")] Employee employee)
        {
            if (id == 0)
            {
                ViewData["CountryId"] = new SelectList(_dbContext.Countries, "CountryId", "CountryName", employee.CountryId);
                ViewData["StateId"] = new SelectList(_dbContext.Statements, "StateId", "StateName", employee.StateId);
                ViewData["CityId"] = new SelectList(_dbContext.Cities, "CityId", "CityName", employee.CityId);
                ViewData["DepId"] = new SelectList(_dbContext.Departments, "DepId", "DepName", employee.DepId);
                _dbContext.Employees.Add(employee);
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                var existingEmployee = await _dbContext.Employees.FindAsync(id);
                if (existingEmployee != null)
                {
                    existingEmployee.EmpName = employee.EmpName;
                    existingEmployee.Mobno = employee.Mobno;
                    existingEmployee.Gender = employee.Gender;
                    existingEmployee.Salary = employee.Salary;
                    existingEmployee.DepId = employee.DepId;
                    existingEmployee.CountryId = employee.CountryId;
                    existingEmployee.StateId = employee.StateId;
                    existingEmployee.CityId = employee.CityId;

                   
                    _dbContext.Update(existingEmployee);
                    await _dbContext.SaveChangesAsync();
                }
                else
                {
                    return NotFound();
                }
            }

            var updatedEmployees = await _dbContext.Employees
                .Include(d => d.Department)
                .Include(d => d.Country)
                .Include(d => d.State)
                .Include(c => c.City)
                .ToListAsync();

            var updatedHtml = Helper.RenderRazorViewToString(this, "_ViewAll", updatedEmployees);

            return Json(new { isValid = true, html = updatedHtml });
        }
       

        


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var emp = await _dbContext.Employees.Include(d => d.Department).Include(d => d.Country).Include(c => c.State).Include(c => c.City).FirstOrDefaultAsync(x => x.EmpId == id);
            _dbContext.Employees.Remove(emp);
            await _dbContext.SaveChangesAsync();
            return Json(new { html = Helper.RenderRazorViewToString(this, "_ViewAll", emp) });


        }

        [HttpGet]
        public JsonResult GetCitiesByState(int stateId)
        {
            var cities = _dbContext.Cities.Where(c => c.StateId == stateId).ToList();
            return Json(cities);
        }

        [HttpGet]
        public JsonResult GetStatesByCountry(int countryId)
        {
            var states = _dbContext.Statements.Where(s => s.CountryId == countryId).ToList();
            return Json(states);
        }

        [HttpGet]
        public async Task<IActionResult> EmployeesByCountry(string CountryName)
        {
            ViewBag.SearchString = CountryName; // Store the search string for displaying it in the input field.

            var country = await _dbContext.Countries.FirstOrDefaultAsync(d => d.CountryName == CountryName);
            if (country == null)
            {
                return NotFound();
            }

            var empincountry = await _dbContext.Employees
                .Include(d => d.Country).Include(d=>d.Department).Include(d=>d.Country).Include(d=>d.State).Include(d=>d.City)
                .Where(d => d.CountryId == country.CountryId)
                .ToListAsync();

            return View("EmployeesByCountry", empincountry);
        }


    }
}