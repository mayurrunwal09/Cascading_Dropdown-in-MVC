using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Context;
using WebApplication1.HelperFolder;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class CityController : Controller
    {
        private readonly MainDbContext _dbContext;
        public CityController(MainDbContext context)
        {
            _dbContext = context;
        }

        public async Task<IActionResult> Index()
        {
            var emp = await _dbContext.Cities .Include(e => e.State).ThenInclude(e => e.Cities).ToListAsync();

            return View(emp);
        }

        public async Task<IActionResult> AddOrEdit(int id = 0)
        {
            if (id == 0)
            {
                ViewBag.States = _dbContext.Statements.ToList();
                ViewData["StateId"] = new SelectList(_dbContext.Statements, "StateId", "StateName");
              //  ViewData["CityId"] = new SelectList(_dbContext.Cities, "CityId", "CityName");

               // ViewData["Dept_Id"] = new SelectList(_dbContext.Departments, "Dept_Id", "Department_Name");

                return View(new City ());
            }
            else
            {
                var emp = await _dbContext.Cities .FindAsync(id);
                if (emp == null)
                {
                    return NotFound();
                }
                ViewBag.States = _dbContext.Statements.ToList();

              //  ViewData["CityId"] = new SelectList(_dbContext.Cities, "CityId", "CityName");

                ViewData["StateId"] = new SelectList(_dbContext.Statements, "StateId", "StateName");

               // ViewData["Dept_Id"] = new SelectList(_dbContext.Departments, "Dept_Id", "Department_Name");
                return View(emp);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int id, [Bind( "CityId","CityName","StateId")] City  employee)
        {


            if (id == 0)
            {
                ViewData["StateId"] = new SelectList(_dbContext.Statements, "StateId", "StateName", employee.StateId);
              //  ViewData["CityId"] = new SelectList(_dbContext.Cities, "CityId", "CityName", employee.CityId);

             //   ViewData["Dept_Id"] = new SelectList(_dbContext.Departments, "Dept_Id", "Department_Name", employee.Dept_Id);

                _dbContext.Cities .Add(employee);
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                try
                {

                    ViewData["StateId"] = new SelectList(_dbContext.Statements, "StateId", "StateName", employee.StateId);
                //    ViewData["CityId"] = new SelectList(_dbContext.Cities, "CityId", "CityName", employee.CityId);

                //    ViewData["Dept_Id"] = new SelectList(_dbContext.Departments, "Dept_Id", "Department_Name", employee.Dept_Id);

                    _dbContext.Update(employee);
                    await _dbContext.SaveChangesAsync();


                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExployeeExist(employee.CityId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }

                }

                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", _dbContext.Cities.ToListAsync()) });
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
            var emp = await _dbContext.Cities.Include(d => d.State).FirstOrDefaultAsync(x => x.CityId  == id);
            _dbContext.Cities.Remove(emp);
            await _dbContext.SaveChangesAsync();
            return Json(new { html = Helper.RenderRazorViewToString(this, "_ViewAll", _dbContext.Cities.ToListAsync()) });

        }
    }
}
