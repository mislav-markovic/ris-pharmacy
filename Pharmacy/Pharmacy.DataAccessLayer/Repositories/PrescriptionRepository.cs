using Pharmacy.BusinessLayer.Repositories;
using Pharmacy.DataAccessLayer.Converters;
using Pharmacy.DataAccessLayer.Models;
using Prescription = Pharmacy.BusinessLayer.Models.Prescription;

namespace Pharmacy.DataAccessLayer.Repositories
{
  public class PrescriptionRepository : IPrescriptionRepository
  {
    private readonly PharmacyDbContext _db;

    public PrescriptionRepository(PharmacyDbContext db)
    {
      _db = db;
    }

    public Prescription Create(Prescription model)
    {
      var dal = model.ToDAL();
      _db.Prescription.Add(dal);
      _db.SaveChanges();
      return dal.ToBLL();
    }

    public Prescription Read(int id)
    {
      var dal = _db.Prescription.Find(id);
      return dal?.ToBLL();
    }

    public bool Update(Prescription model)
    {
      var dal = model.ToDAL();
      try
      {
        _db.Prescription.Update(dal);
        _db.SaveChanges();
        return true;
      }
      catch
      {
        return false;
      }
    }

    public bool Delete(Prescription model)
    {
      var dal = model.ToDAL();

      try
      {
        _db.Prescription.Remove(dal);
        _db.SaveChanges();
        return true;
      }
      catch
      {
        return false;
      }
    }
  }
}