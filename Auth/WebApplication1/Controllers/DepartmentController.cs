using WebApplication1.Context;
using WebApplication1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.HelperFolder;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {
        private readonly MainDbContext _dbContext;
        public DepartmentController(MainDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            var dept = await _dbContext.Departments.ToListAsync();
            return View(dept);
        }

        [HttpGet]
        public async Task<IActionResult> AddOrEdit(int id = 0)
        {
            if (id == 0)
            {
                return View(new Department());
            }
            var emp = await _dbContext.Departments.FirstOrDefaultAsync(x => x.Dept_Id == id);
            if (emp == null)
            {
                return NotFound();
            }
            return View(emp);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int id, Department department)
        {


            if (id == 0)
            {
                _dbContext.Departments.Add(department);
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                try
                {
                     _dbContext.Departments.Update(department);
                    await _dbContext.SaveChangesAsync();

                }
                catch
                {
                    if (!DepartmentExists(department.Dept_Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", _dbContext.Departments.ToListAsync()) });

            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEdit", department) });

        }

        private bool DepartmentExists(int dept_Id)
        {
            throw new NotImplementedException();
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dep = await _dbContext.Departments.FindAsync(id);
            _dbContext.Departments.Remove(dep);
            await _dbContext.SaveChangesAsync();
            return Json(new { html = Helper.RenderRazorViewToString(this, "_ViewAll", _dbContext.Departments.ToListAsync()) });
        }
    }
}
