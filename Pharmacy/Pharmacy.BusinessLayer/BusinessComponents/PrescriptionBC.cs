using System;
using System.Linq;
using Pharmacy.BusinessLayer.Models;
using Pharmacy.BusinessLayer.Repositories;

namespace Pharmacy.BusinessLayer.BusinessComponents
{
  public class PrescriptionBC
  {
    private readonly IPharmacyRepository _pharmacyRepository;
    private readonly IPrescriptionRepository _prescriptionRepository;
    private readonly IStockpileRepository _stockpileRepository;
    private readonly IUserRepository _userRepository;

    public PrescriptionBC(IPrescriptionRepository prescriptionRepository, IPharmacyRepository pharmacyRepository,
      IStockpileRepository stockpileRepository, IUserRepository userRepository)
    {
      _prescriptionRepository = prescriptionRepository;
      _pharmacyRepository = pharmacyRepository;
      _stockpileRepository = stockpileRepository;
      _userRepository = userRepository;
    }

    public Prescription UpdatePrescription(int id, Prescription updatedPrescription)
    {
      ValidatePrescription(updatedPrescription, true);
      _prescriptionRepository.Update(updatedPrescription);
      return GetPrescription(id);
    }

    public Prescription GetPrescription(int prescriptionId)
    {
      return _prescriptionRepository.Read(prescriptionId);
    }

    public Prescription GetFirst()
    {
      return _prescriptionRepository.ReadAll().FirstOrDefault();
    }

    public int? GetIdOfNext(int current)
    {
      return _prescriptionRepository.ReadAll().OrderBy(pr => pr.Id).FirstOrDefault(pr => pr.Id > current)?.Id;
    }

    public int? GetIdOfPrevious(int current)
    {
      return _prescriptionRepository.ReadAll().OrderByDescending(pr => pr.Id).FirstOrDefault(pr => pr.Id < current)?.Id;
    }

    public int AddPrescription(Prescription prescription)
    {
      if (ValidatePrescription(prescription, false))
        return _prescriptionRepository.Create(prescription).Id;
      throw new ArgumentException();
    }

    public Prescription AddMedicineToPrescription(int prescriptionId, int medicineId, int amount,
      int? prescriptionMedicineId)
    {
      _prescriptionRepository.AddOrUpdateMedicineToPrescription(prescriptionId, medicineId, amount,
        prescriptionMedicineId);
      return _prescriptionRepository.Read(prescriptionId);
    }

    public bool RemoveMedicineFromPrescription(int prescriptionMedicineId)
    {
      return _prescriptionRepository.RemoveMedicineFromPrescription(prescriptionMedicineId);
    }

    public bool FulfillPrescription(Prescription prescription, int pharmacyId)
    {
      if (!ValidatePrescription(prescription, true)) return false;
      var pharmacy = _pharmacyRepository.Read(pharmacyId);
      var medicine = prescription.Medicine.Select(med => med.MedicineId).ToHashSet();
      var stockpile = pharmacy.Stockpile.Select(stock => stock.MedicineId).ToHashSet();
      // if stockpile doesn't contain requested medicine
      if (!medicine.All(med => stockpile.Contains(med))) return false;

      var stockpileDict = pharmacy.Stockpile.ToDictionary(k => k.MedicineId, v => v.Amount);
      foreach (var prescriptionMedicine in prescription.Medicine)
      {
        var newAmount = stockpileDict[prescriptionMedicine.MedicineId] - prescriptionMedicine.Amount;
        // check that requested amount can be fulfilled 
        if (newAmount < 0) return false;

        var stockpileModel = pharmacy.Stockpile.First(st => st.MedicineId == prescriptionMedicine.MedicineId);
        stockpileModel = _stockpileRepository.Read(stockpileModel.Id);
        // update the amount in stockpile
        stockpileModel.Amount = newAmount;
        _stockpileRepository.Update(stockpileModel);
      }

      // update prescription
      prescription.SaleTime = DateTime.Now;
      _prescriptionRepository.Update(prescription);
      return true;
    }

    public bool Delete(int prescriptionId)
    {
      return _prescriptionRepository.Delete(prescriptionId);
    }

    private bool ValidatePrescription(Prescription model, bool isUpdate)
    {
      var updateCondition = !isUpdate || model.Medicine?.Count > 0;
      var user = _userRepository.Read(model.User.Id);
      var roleCondition = new[] {UserRole.EmployeeRole, UserRole.AdminRole}.Contains(user.UserRole.RoleName);
      var result = !string.IsNullOrWhiteSpace(model.Buyer) &&
                   roleCondition &&
                   updateCondition;
      return result;
    }
  }
}