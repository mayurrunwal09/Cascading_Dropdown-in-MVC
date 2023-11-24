using Demo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Demo.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {
        private readonly MainDBContext _dbContext;
        public DepartmentController(MainDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            var dep = await _dbContext.Departments.ToListAsync();
            return View(dep);
        }

        public async Task<IActionResult>AddOrEdit(int id = 0)
        {
            if(id == 0)
            {
                return View(new Department());
            }
            else
            {
                var dep = await _dbContext.Departments.FindAsync(id);
                return View(dep);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int id, Department department)
        {
           if(id==0)
            {
                _dbContext.Departments.Add(department);
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                var existdep = await _dbContext.Departments.FindAsync(id);
                if (existdep != null)
                {
                    existdep.DepId = department.DepId;
                    existdep.DepName = department.DepName;

                    _dbContext.Departments.Update(department);
                    await _dbContext.SaveChangesAsync();
                }
                else
                {
                    return NotFound();
                }
            }
            var updatedEmployees = await _dbContext.Departments.ToListAsync();

            var updatedHtml = Helper.RenderRazorViewToString(this, "_ViewAll", updatedEmployees);

            return Json(new { isValid = true, html = updatedHtml });
        }

       

        

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var emp = await _dbContext.Departments.FindAsync(id);
            _dbContext.Departments .Remove(emp) ;
            await _dbContext.SaveChangesAsync();
            return Json(new { html = Helper.RenderRazorViewToString(this, "_ViewAll", emp) });


        }
    }
}
