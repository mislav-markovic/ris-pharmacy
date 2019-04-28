using System.Collections.Generic;

namespace Pharmacy.BusinessLayer.Models
{
  public class User
  {
    public int Id { get; set; }
    public string Username { get; set; }
    public string PasswordHash { get; set; }
    public string PasswordSalt { get; set; }

    //place of work
    public Pharmacy Pharmacy { get; set; }

    public UserRole UserRole { get; set; }
  }
}