using Pharmacy.BusinessLayer.Models;

namespace Pharmacy.BusinessLayer.Repositories
{
  public interface IUserRepository : ICRUDRepository<User>
  {
    int WorksAt(int userId);
  }
}