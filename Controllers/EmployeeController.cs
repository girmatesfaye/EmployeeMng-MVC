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
        var employees = await _context.Employees.Include(e => e.Department).ToListAsync();
        return View(employees);
    }
}
