using System;
using System.Collections.Generic;
using System.Text;
using Pharmacy.BusinessLayer.Repositories;

namespace Pharmacy.DataAccessLayer.Repositories
{
  public class PharmacyRepository : IPharmacyRepository
  {
    public BusinessLayer.Models.Pharmacy Create(BusinessLayer.Models.Pharmacy model)
    {
      throw new NotImplementedException();
    }

    public BusinessLayer.Models.Pharmacy Read(int id)
    {
      throw new NotImplementedException();
    }

    public IEnumerable<BusinessLayer.Models.Pharmacy> ReadAll()
    {
      throw new NotImplementedException();
    }

    public bool Update(BusinessLayer.Models.Pharmacy model)
    {
      throw new NotImplementedException();
    }

    public bool Delete(BusinessLayer.Models.Pharmacy model)
    {
      throw new NotImplementedException();
    }
  }
}
