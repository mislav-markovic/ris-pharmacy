using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Pharmacy.BusinessLayer.Repositories;
using Pharmacy.DataAccessLayer.Converters;
using Pharmacy.DataAccessLayer.Models;
using Stockpile = Pharmacy.BusinessLayer.Models.Stockpile;

namespace Pharmacy.DataAccessLayer.Repositories
{
  public class StockpileRepository : IStockpileRepository
  {
    private readonly PharmacyDbContext _db;

    public StockpileRepository(PharmacyDbContext db)
    {
      _db = db;
    }

    public Stockpile Create(Stockpile model)
    {
      throw new NotImplementedException();
    }

    public Stockpile Read(int id)
    {
      return _db.Stockpile.AsNoTracking().First(st=>st.StockpileId == id).ToBLL();
    }

    public IEnumerable<Stockpile> ReadAll()
    {
      throw new NotImplementedException();
    }

    public bool Update(Stockpile model)
    {
      var dal = model.ToDAL();
      try
      {
        _db.Stockpile.Update(dal);
        _db.SaveChanges();
        return true;
      }
      catch (Exception e)
      {
        return false;
      }
    }

    public bool Delete(Stockpile model)
    {
      throw new NotImplementedException();
    }

    public IEnumerable<Stockpile> ForPharmacy(int pharmacyId)
    {
      return _db.Stockpile.Where(st => st.PharmacyId == pharmacyId).Select(st => st.ToBLL());
    }
  }
}
