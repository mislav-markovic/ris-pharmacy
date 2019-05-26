using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
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
      _db.SaveChanges();
      return dal.ToBLL();
    }

    public Prescription Read(int id)
    {
      var dal = _db.Prescription.Include(pr => pr.User).Include(pr => pr.PrescriptionMedicine)
        .ThenInclude(pm => pm.Medicine).FirstOrDefault(elem => elem.PrescriptionId == id);
      return dal?.ToBLL();
    }

    public IEnumerable<Prescription> ReadAll()
    {
      var result = _db.Prescription.Include(pr => pr.User).Include(pr => pr.PrescriptionMedicine)
        .ThenInclude(pm => pm.Medicine).Select(elem => elem.ToBLL()).ToList();
      return result;
    }

    public bool Update(Prescription model)
    {
      var dal = model.ToDAL();

      var prescriptionMedicine = _db.PrescriptionMedicine.Where(pm => pm.PrescriptionId == model.Id).ToList();
      try
      {
        var pmIds = dal.PrescriptionMedicine.Select(pm => pm.PrescriptionMedicineId).ToList();
        var persistenceIds = new List<int>();
        {
          foreach (var pm in prescriptionMedicine)
            if (pmIds.Contains(pm.PrescriptionMedicineId))
            {
              var dalPm = dal.PrescriptionMedicine.First(e => e.PrescriptionMedicineId == pm.PrescriptionMedicineId);
              pm.MedicineId = dalPm.MedicineId;
              pm.Amount = dalPm.Amount;
              //_db.PrescriptionMedicine.Update(pm);
              persistenceIds.Add(pm.PrescriptionMedicineId);
            }
            else
            {
              _db.PrescriptionMedicine.Remove(pm);
            }

          _db.SaveChanges();
        }

        foreach (var medicine in dal.PrescriptionMedicine)
        {
          if (!persistenceIds.Contains(medicine.PrescriptionMedicineId))
          {
            var newPm = new PrescriptionMedicine
              {Amount = medicine.Amount, MedicineId = medicine.MedicineId, PrescriptionId = medicine.PrescriptionId};
            _db.Add(newPm);
            _db.SaveChanges();
          }
        }
      }
      catch (Exception e)
      {
        return false;
      }

      try
      {
        var updated = _db.Prescription
          .First(e => e.PrescriptionId == dal.PrescriptionId);
        updated.Buyer = dal.Buyer;
        updated.SaleTime = dal.SaleTime;
        updated.UserId = dal.UserId;
        _db.Prescription.Update(updated);
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
      return Delete(model.Id);
    }

    public bool AddOrUpdateMedicineToPrescription(int prescriptionId, int medicineId, int amount,
      int? prescriptionMedicineId)
    {
      try
      {
        var pm = new PrescriptionMedicine {Amount = amount, MedicineId = medicineId, PrescriptionId = prescriptionId};
        if (prescriptionMedicineId.HasValue)
        {
          pm.PrescriptionMedicineId = prescriptionMedicineId.Value;
          _db.PrescriptionMedicine.Update(pm);
        }
        else
        {
          var id = _db.PrescriptionMedicine.Max(e => e.PrescriptionMedicineId);
          pm.PrescriptionMedicineId = id + 1;
          _db.PrescriptionMedicine.Add(pm);
        }

        _db.SaveChanges();
      }
      catch (Exception e)
      {
        return false;
      }

      return true;
    }

    public bool RemoveMedicineFromPrescription(int prescriptionMedicineId)
    {
      try
      {
        var pm = _db.PrescriptionMedicine.Find(prescriptionMedicineId);
        _db.PrescriptionMedicine.Remove(pm);
        _db.SaveChanges();
      }
      catch (Exception e)
      {
        return false;
      }

      return true;
    }

    public bool Delete(int id)
    {
      try
      {
        foreach (var pmId in _db.PrescriptionMedicine.Where(pm => pm.PrescriptionId == id)
          .Select(e => e.PrescriptionMedicineId))
        {
          var temp = _db.PrescriptionMedicine.First(pm => pm.PrescriptionMedicineId == pmId);
          _db.PrescriptionMedicine.Remove(temp);
          _db.SaveChanges();
        }

        var dal = _db.Prescription.Find(id);
        _db.Prescription.Remove(dal);
        _db.SaveChanges();
        return true;
      }
      catch (Exception e)
      {
        return false;
      }
    }
  }
}