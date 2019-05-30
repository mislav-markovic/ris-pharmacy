using Pharmacy.BusinessLayer.Models;

namespace Pharmacy.PresentationLayer.Models
{
  public class UserViewModel
  {
    public UserViewModel(int id, string username, string pharmacyName)
    {
      Id = id;
      Username = username;
      PharmacyName = pharmacyName;
    }

    public UserViewModel(User user) : this(user.Id, user.Username, user.Pharmacy?.Name)
    {
    }

    public int Id { get; }
    public string Username { get; }
    public string PharmacyName { get; }
  }
}