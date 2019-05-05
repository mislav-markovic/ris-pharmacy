using System.Collections.Generic;

namespace Pharmacy.BusinessLayer.Repositories
{
  public interface ICRUDRepository<T> where T : class
  {
    T Create(T model);
    T Read(int id);
    IEnumerable<T> ReadAll();
    bool Update(T model);
    bool Delete(T model);
  }
}