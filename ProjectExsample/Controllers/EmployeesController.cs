using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectExsample.Models.Db;

namespace ProjectExsample.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly EmployeeDBContext _context;

        public EmployeesController(EmployeeDBContext context)
        {
            _context = context;
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            var employeeDBContext = _context.Employees.Include(e => e.Gender).Include(e => e.Position);
            return View(await employeeDBContext.ToListAsync());
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employees = await _context.Employees
                .Include(e => e.Gender)
                .Include(e => e.Position)
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (employees == null)
            {
                return NotFound();
            }

            return View(employees);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            ViewData["GenderId"] = new SelectList(_context.MasterGender, "GenderId", "GenderName");
            ViewData["PositionId"] = new SelectList(_context.MasterPosition, "PositionId", "PositionName");
            return View();
        }

        // GET: Employees/Create
        public IActionResult AddEmployees()
        {
            ViewData["GenderId"] = new SelectList(_context.MasterGender, "GenderId", "GenderName");
            ViewData["PositionId"] = new SelectList(_context.MasterPosition, "PositionId", "PositionName");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeId,Name,Surname,Birthday,GenderId,PositionId")] Employees employees)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employees);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GenderId"] = new SelectList(_context.MasterGender, "GenderId", "GenderName", employees.GenderId);
            ViewData["PositionId"] = new SelectList(_context.MasterPosition, "PositionId", "PositionName", employees.PositionId);
            return View(employees);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employees = await _context.Employees.FindAsync(id);
            if (employees == null)
            {
                return NotFound();
            }
            ViewData["GenderId"] = new SelectList(_context.MasterGender, "GenderId", "GenderName", employees.GenderId);
            ViewData["PositionId"] = new SelectList(_context.MasterPosition, "PositionId", "PositionName", employees.PositionId);


            return View(employees);
        }

        public async Task<IActionResult> Editpopup(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employees = await _context.Employees.FindAsync(id);
            if (employees == null)
            {
                return NotFound();
            }
            ViewData["GenderId"] = new SelectList(_context.MasterGender, "GenderId", "GenderName", employees.GenderId);
            ViewData["PositionId"] = new SelectList(_context.MasterPosition, "PositionId", "PositionName", employees.PositionId);


            return View(employees);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editpopup(int id, [Bind("EmployeeId,Name,Surname,Birthday,GenderId,PositionId")] Employees employees)
        {
            if (id != employees.EmployeeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employees);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeesExists(employees.EmployeeId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["GenderId"] = new SelectList(_context.MasterGender, "GenderId", "GenderId", employees.GenderId);
            ViewData["PositionId"] = new SelectList(_context.MasterPosition, "PositionId", "PositionId", employees.PositionId);
            return View(employees);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmployeeId,Name,Surname,Birthday,GenderId,PositionId")] Employees employees)
        {
            if (id != employees.EmployeeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employees);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeesExists(employees.EmployeeId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["GenderId"] = new SelectList(_context.MasterGender, "GenderId", "GenderId", employees.GenderId);
            ViewData["PositionId"] = new SelectList(_context.MasterPosition, "PositionId", "PositionId", employees.PositionId);
            return View(employees);
        }
        //-----------------------------------------------------------

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employees = await _context.Employees
                .Include(e => e.Gender)
                .Include(e => e.Position)
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (employees == null)
            {
                return NotFound();
            }

            return View(employees);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employees = await _context.Employees.FindAsync(id);
            _context.Employees.Remove(employees);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        //--------------------------------------

        public async Task<IActionResult> Deletepopup(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employees = await _context.Employees
                .Include(e => e.Gender)
                .Include(e => e.Position)
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (employees == null)
            {
                return NotFound();
            }

            return View(employees);
        }

        [HttpPost, ActionName("Deletepopup")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletepopupConfirmed(int id)
        {
            var employees = await _context.Employees.FindAsync(id);
            _context.Employees.Remove(employees);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeesExists(int id)
        {
            return _context.Employees.Any(e => e.EmployeeId == id);
        }
    }
}
