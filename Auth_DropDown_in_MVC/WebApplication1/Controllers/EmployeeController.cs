using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Context;
using WebApplication1.HelperFolder;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
   
        [Authorize]
        public class EmployeeController : Controller
        {
            private readonly MainDbContext _dbContext;
            public EmployeeController(MainDbContext context)
            {
                _dbContext = context;
            }

            public async Task<IActionResult> Index()
            {
                var emp = await _dbContext.Employees.Include(e => e.department).Include(e => e.State).ThenInclude(e => e.Cities).ToListAsync();

                return View(emp);
            }

            public async Task<IActionResult> AddOrEdit(int id = 0)
            {
                if (id == 0)
                {
                    ViewBag.States = _dbContext.Statements.ToList();
                    ViewData["StateId"] = new SelectList(_dbContext.Statements, "StateId", "StateName");
                    ViewData["CityId"] = new SelectList(_dbContext.Cities, "CityId", "CityName");
 
                    ViewData["Dept_Id"] = new SelectList(_dbContext.Departments, "Dept_Id", "Department_Name");

                    return View(new Employee());
                }
                else
                {
                    var emp = await _dbContext.Employees.FindAsync(id);
                    if (emp == null)
                    {
                        return NotFound();
                    }
                    ViewBag.States = _dbContext.Statements.ToList();

                     ViewData["CityId"] = new SelectList(_dbContext.Cities, "CityId", "CityName");

                     ViewData["StateId"] = new SelectList(_dbContext.Statements, "StateId", "StateName");

                    ViewData["Dept_Id"] = new SelectList(_dbContext.Departments, "Dept_Id", "Department_Name");
                    return View(emp);
                }
            }
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> AddOrEdit(int id, [Bind("Emp_Id", "Emp_Name", "Emp_Adress", "mobno", "Sallary", "age", "Dept_Id", "Department_Name", "StateId", "StateName", "CityId", "CityName")] Employee employee)
            {


                if (id == 0)
                {
                    ViewData["StateId"] = new SelectList(_dbContext.Statements, "StateId", "StateName", employee.StateId);
                ViewData["CityId"] = new SelectList(_dbContext.Cities, "CityId", "CityName",employee.CityId);

                ViewData["Dept_Id"] = new SelectList(_dbContext.Departments, "Dept_Id", "Department_Name", employee.Dept_Id);

                    _dbContext.Employees.Add(employee);
                    await _dbContext.SaveChangesAsync();
                }
                else
                {
                    try
                    {

                        ViewData["StateId"] = new SelectList(_dbContext.Statements, "StateId", "StateName", employee.StateId);
                    ViewData["CityId"] = new SelectList(_dbContext.Cities, "CityId", "CityName", employee.CityId);

                    ViewData["Dept_Id"] = new SelectList(_dbContext.Departments, "Dept_Id", "Department_Name", employee.Dept_Id);

                        _dbContext.Update(employee);
                        await _dbContext.SaveChangesAsync();


                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ExployeeExist(employee.Emp_Id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }

                    }

                    return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", _dbContext.Employees.ToListAsync()) });
                }

                return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEdit", employee) });
            }

            private bool ExployeeExist(int emp_Id)
            {
                throw new NotImplementedException();
            }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var emp = await _dbContext.Employees.Include(d => d.department).FirstOrDefaultAsync(x => x.Emp_Id == id);
            _dbContext.Employees.Remove(emp);
            await _dbContext.SaveChangesAsync();
            return Json(new { html = Helper.RenderRazorViewToString(this, "_ViewAll", emp) });
            /*       }
               #region State City 

               [HttpGet]
               public IActionResult GetStates()
               {
                   ViewBag.Statements = _dbContext.Statements.ToList().Select(c => new SelectListItem { Value = c.StateId.ToString(), Text = c.StateName }).ToList();
                   return View();
               }

               public IActionResult GetCities(int id)
               {

                   var statesList = _dbContext.Cities.Where(s => s.StateId == id).ToList().Select(c => new SelectListItem { Value = c.CityId.ToString(), Text = c.CityName }).ToList();
                   return Json(statesList);
               }

               [HttpPost]
               public IActionResult Cascading(int city, int State)
               {

                   ViewBag.SelectedStates =
                       _dbContext.Statements.Where(a => a.StateId == city).FirstOrDefault().StateName;
                   ViewBag.SelectedCities = _dbContext.Cities.Where(a => a.CityId == State).FirstOrDefault().CityName;
                   ViewBag.Statements = _dbContext.Statements.ToList().Select(c => new SelectListItem { Value = c.StateId.ToString(), Text = c.StateName }).ToList();
                   return View();
               }

               #endregion*/

        }
        [HttpGet]
        public JsonResult GetCitiesByState(int stateId)
        {
            var cities = _dbContext.Cities.Where(c => c.StateId == stateId).ToList();
            return Json(cities);
        }
    }
}
