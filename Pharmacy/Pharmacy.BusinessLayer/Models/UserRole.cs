namespace Pharmacy.BusinessLayer.Models
{
  public class UserRole
  {
    public int Id { get; set; }
    public string RoleName { get; set; }

    internal const string EmployeeRole = "Employee";
    internal const string ManagerRole = "Manager";
    internal const string AdminRole = "Admin";
  }
}