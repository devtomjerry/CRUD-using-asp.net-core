using crud.Data;
using crud.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeCRUD.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            IEnumerable<Employee> employees = _context.Employees.ToList();
            return View("~/Views/Home/Index.cshtml", employees);
        }

        public IActionResult Create()
        {
            return View("~/Views/Home/Create.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employee empobj)
        {
            if (ModelState.IsValid)
            {
                _context.Employees.Add(empobj);
                _context.SaveChanges();
                TempData["ResultOk"] = "Record Added Successfully !";
                return RedirectToAction(nameof(Index));
            }

            return View("~/Views/Home/Create.cshtml", empobj);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Employee empobj = _context.Employees.Find(id);
            if (empobj == null)
            {
                return NotFound();
            }

            return View("~/Views/Home/Edit.cshtml", empobj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Employee empobj)
        {
            if (ModelState.IsValid)
            {
                _context.Employees.Update(empobj);
                _context.SaveChanges();
                TempData["ResultOk"] = "Data Updated Successfully !";
                return RedirectToAction(nameof(Index));
            }

            return View("~/Views/Home/Edit.cshtml", empobj);
        }
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = _context.Employees.FirstOrDefault(e => e.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View("~/Views/Home/Delete.cshtml", employee);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var employee = _context.Employees.FirstOrDefault(e => e.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(employee);
            _context.SaveChanges();
            TempData["ResultOk"] = "Employee deleted successfully!";
            return RedirectToAction(nameof(Index));
        }

    }
}
