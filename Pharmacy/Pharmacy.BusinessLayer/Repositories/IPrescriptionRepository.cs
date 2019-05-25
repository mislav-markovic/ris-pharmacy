using System;
using System.Collections.Generic;
using System.Text;
using Pharmacy.BusinessLayer.Models;

namespace Pharmacy.BusinessLayer.Repositories
{
  public interface IPrescriptionRepository : ICRUDRepository<Prescription>
  {
    bool AddOrUpdateMedicineToPrescription(int prescriptionId, int medicineId, int amount, int? prescriptionMedicineId);
    bool RemoveMedicineFromPrescription(int prescriptionMedicineId);
    bool Delete(int id);
  }
}
