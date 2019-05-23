using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pharmacy.BusinessLayer.Models;

namespace Pharmacy.PresentationLayer.Models
{
  public class UserViewModel
  {
    public int Id { get; }
    public string Username { get; }

    public UserViewModel(int id, string username)
    {
      Id = id;
      Username = username;
    }

    public UserViewModel(User user)
    {
      Id = user.Id;
      Username = user.Username;
    }
  }
}
