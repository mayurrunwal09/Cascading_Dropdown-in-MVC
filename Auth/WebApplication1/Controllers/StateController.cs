using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Context;
using WebApplication1.HelperFolder;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class StateController : Controller
    {
        private readonly MainDbContext _dbContext;
        public StateController(MainDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            var dept = await _dbContext.Statements .ToListAsync();
            return View(dept);
        }

        [HttpGet]
        public async Task<IActionResult> AddOrEdit(int id = 0)
        {
            if (id == 0)
            {
                return View(new State ());
            }
            var emp = await _dbContext.Statements .FirstOrDefaultAsync(x => x.StateId  == id);
            if (emp == null)
            {
                return NotFound();
            }
            return View(emp);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int id, State  department)
        {


            if (id == 0)
            {
                _dbContext.Statements .Add(department);
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                try
                {
                    _dbContext.Statements .Update(department);
                    await _dbContext.SaveChangesAsync();

                }
                catch
                {
                    if (!DepartmentExists(department.StateId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", _dbContext.Statements .ToListAsync()) });

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
            var dep = await _dbContext.Statements.FindAsync(id);
            _dbContext.Statements.Remove(dep);
            await _dbContext.SaveChangesAsync();
            return Json(new { html = Helper.RenderRazorViewToString(this, "_ViewAll", _dbContext.Statements.ToListAsync()) });
        }
    }
}
