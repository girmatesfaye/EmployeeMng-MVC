using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Employee_Management_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Employee_Management_System.Controllers;

public class EmployeeController : Controller
{
  private readonly EmployeeDbContext _context;

  public EmployeeController(EmployeeDbContext context)
  {
    _context = context;
  }

  public async Task<IActionResult> Index()
  {
    var employees = await _context.Employees.Include(employee => employee.Department).ToListAsync();
    return View(employees);
  }

  [HttpGet]
  public async Task<IActionResult> EmployeeTable()
  {
    var employees = await _context.Employees
        .Include(e => e.Department)
        .ToListAsync();

    return PartialView("_EmployeeTable", employees);
  }

  [HttpGet]
  public IActionResult CreateEmployee()
  {
    return PartialView("_CreateEmployee");

  }

  [HttpPost]
  public async Task<IActionResult> CreateEmployee(EmployeeCreateViewModel employeeCreateViewModel)
  {
    if (employeeCreateViewModel == null)
    {
      return BadRequest();
    }
    var employee = new Employee
    {
      Name = employeeCreateViewModel.Name,
      Salary = employeeCreateViewModel.Salary,
      DepartmentId = employeeCreateViewModel.DepartmentId,
      Status = EmployeeStatus.Active
    };
    _context.Employees.Add(employee);
    await _context.SaveChangesAsync();

    return Json(new { success = true, message = "Employee created successfully" });
  }

  [HttpGet]
  public async Task<IActionResult> EditEmployee(int id)
  {
    var employee = await _context.Employees.FindAsync(id);
    if (employee == null)
    {
      return NotFound();
    }
    return PartialView("_EditEmployee", employee);
  }

  [HttpPost]
  public async Task<IActionResult> EditEmployee(EmployeeCreateViewModel employeeCreateViewModel)
  {
    if (employeeCreateViewModel == null)
    {
      return BadRequest();
    }
    var employee = await _context.Employees.FindAsync(employeeCreateViewModel.Id);
    if (employee == null || employee.Id != employeeCreateViewModel.Id)
    {
      return NotFound();
    }
    employee.Name = employeeCreateViewModel.Name;
    employee.Salary = employeeCreateViewModel.Salary;
    employee.DepartmentId = employeeCreateViewModel.DepartmentId;

    _context.Employees.Update(employee);
    await _context.SaveChangesAsync();

    return Json(new { success = true, message = "Employee updated successfully" });
  }

  [HttpGet]
  public async Task<IActionResult> DeleteEmployee(int id)
  {
    var employee = await _context.Employees.FindAsync(id);
    if (employee == null)
    {
      return NotFound();
    }
    return PartialView("_DeleteEmployee", employee);
  }
  [HttpPost]
  [ActionName("DeleteEmployee")]
  public async Task<IActionResult> DeleteEmployeeConfirmed(int id)
  {
    var employee = await _context.Employees.FindAsync(id);
    if (employee == null)
    {
      return NotFound();
    }
    _context.Employees.Remove(employee);
    await _context.SaveChangesAsync();

    return Json(new { success = true, message = "Employee deleted successfully" });
  }

  [HttpGet]
  public async Task<IActionResult> Details(int id)
  {
    var employee = await _context.Employees.Include(e => e.Department).FirstOrDefaultAsync(e => e.Id == id);
    if (employee == null)
    {
      return NotFound();
    }
    return View(employee);
  }
}
