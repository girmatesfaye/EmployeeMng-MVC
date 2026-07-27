namespace Employee_Management_System.Models;

public enum EmployeeStatus
{
  Active,
  Inactive,
  OnLeave,
  Suspended,
  Resigned
}
public class Employee
{
  public int Id { get; set; }
  public string Name { get; set; }
  public string Email { get; set; }
  public string Phone { get; set; }
  public decimal Salary { get; set; }
  public int DepartmentId { get; set; }
  public EmployeeStatus Status { get; set; }
  public string Photo { get; set; }
  public DateTime CreatedAt { get; set; }
  public Department Department { get; set; }
}