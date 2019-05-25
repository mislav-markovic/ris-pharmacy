using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Pharmacy.BusinessLayer.Repositories;
using Pharmacy.DataAccessLayer.Converters;
using Pharmacy.DataAccessLayer.Models;
using Medicine = Pharmacy.BusinessLayer.Models.Medicine;

namespace Pharmacy.DataAccessLayer.Repositories
{
  public class MedicineRepository : IMedicineRepository
  {
    private readonly PharmacyDbContext _db;

    public MedicineRepository(PharmacyDbContext db)
    {
      _db = db;
    }

    public Medicine Create(Medicine model)
    {
      var dal = model.ToDAL();
      _db.Medicine.Add(dal);
      return _db.Medicine.Find(dal.MedicineId).ToBLL();
    }

    public Medicine Read(int id)
    {
      return _db.Medicine.Find(id).ToBLL();
    }

    public IEnumerable<Medicine> ReadAll()
    {
      return _db.Medicine.Include(m => m.PrescriptionMedicine).Select(m => m.ToBLL());
    }

    public bool Update(Medicine model)
    {
      try
      {
        var temp = _db.Medicine.Find(model.Id);
        temp.Price = model.Price;
        temp.Name = model.Name ?? temp.Name;
        _db.Medicine.Update(temp);
        _db.SaveChanges();
      }
      catch (Exception e)
      {
        return false;
      }

      return true;
    }

    public bool Delete(Medicine model)
    {
      throw new NotImplementedException();
    }
  }
}