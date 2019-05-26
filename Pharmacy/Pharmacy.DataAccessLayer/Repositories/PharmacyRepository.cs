using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Pharmacy.BusinessLayer.Repositories;
using Pharmacy.DataAccessLayer.Converters;
using Pharmacy.DataAccessLayer.Models;

namespace Pharmacy.DataAccessLayer.Repositories
{
  public class PharmacyRepository : IPharmacyRepository
  {
    private readonly PharmacyDbContext _db;

    public PharmacyRepository(PharmacyDbContext db)
    {
      _db = db;
    }

    public BusinessLayer.Models.Pharmacy Create(BusinessLayer.Models.Pharmacy model)
    {
      throw new NotImplementedException();
    }

    public BusinessLayer.Models.Pharmacy Read(int id)
    {
      return _db.Pharmacy.Include(ph => ph.Stockpile).AsNoTracking().First(e => e.PharmacyId == id).ToBLL();
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
