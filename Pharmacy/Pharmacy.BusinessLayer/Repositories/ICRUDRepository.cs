using System;
using System.Collections.Generic;
using System.Text;

namespace Pharmacy.BusinessLayer.Repositories
{
  public interface ICRUDRepository<T> where T : class
  {
    T Create(T model);
    T Read(int id);
    bool Update(T model);
    bool Delete(T model);
  }
}
