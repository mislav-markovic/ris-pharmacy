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

    public PrescriptionBC(IPrescriptionRepository prescriptionRepository, IPharmacyRepository pharmacyRepository,
      IStockpileRepository stockpileRepository)
    {
      _prescriptionRepository = prescriptionRepository;
      _pharmacyRepository = pharmacyRepository;
      _stockpileRepository = stockpileRepository;
    }

    public Prescription GetPrescription(int prescriptionId)
    {
      return _prescriptionRepository.Read(prescriptionId);
    }

    public Prescription GetFirst()
    {
      return _prescriptionRepository.ReadAll().First();
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
      if (ValidatePrescription(prescription))
        return _prescriptionRepository.Create(prescription).Id;
      throw new ArgumentException();
    }

    public Prescription AddMedicineToPrescription(Prescription prescription, Medicine medicine, int amount)
    {
      return AddMedicineToPrescription(prescription.Id, medicine, amount);
    }

    public Prescription AddMedicineToPrescription(int prescriptionId, Medicine medicine, int amount)
    {
      var prescription = _prescriptionRepository.Read(prescriptionId);
      prescription.Medicine[medicine] = amount;
      _prescriptionRepository.Update(prescription);
      return _prescriptionRepository.Read(prescriptionId);
    }

    public bool FulfillPrescription(Prescription prescription, int pharmacyId)
    {
      if (!ValidatePrescription(prescription)) return false;
      var pharmacy = _pharmacyRepository.Read(pharmacyId);
      var medicine = prescription.Medicine.Select(med => med.Key.Id).ToHashSet();
      var stockpile = pharmacy.Stockpile.Select(stock => stock.MedicineId).ToHashSet();
      // if stockpile doesn't contain requested medicine
      if (!medicine.All(med => stockpile.Contains(med))) return false;

      var stockpileDict = pharmacy.Stockpile.ToDictionary(k => k.MedicineId, v => v.Amount);
      foreach (var (med, amount) in prescription.Medicine)
      {
        var newAmount = stockpileDict[med.Id] - amount;
        // check that requested amount can be fulfilled 
        if (newAmount < 0) return false;

        var stockpileModel = pharmacy.Stockpile.First(st => st.MedicineId == med.Id);
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

    private static bool ValidatePrescription(Prescription model)
    {
      var result = !string.IsNullOrWhiteSpace(model.Buyer) &&
                   model.User.UserRole.RoleName.Equals(UserRole.EmployeeRole) &&
                   model.Medicine?.Count > 0;
      return result;
    }
  }
}