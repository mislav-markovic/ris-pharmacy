using System;
using System.Collections.Generic;
using System.Linq;
using Pharmacy.BusinessLayer.Models;

namespace Pharmacy.PresentationLayer.Models
{
  public class PrescriptionDetailsVIewModel
  {
    public PrescriptionDetailsVIewModel(int id, string buyer, DateTime saleTime, UserViewModel user,
      IList<PrescriptionMedicine> medicine, int? nextPrescriptionId, int? previousPrescriptionId)
    {
      var medicineVm = medicine.Select(elem => new MedicineDetailsViewModel(elem.Medicine, elem.Amount){PrescriptionMedicineId = elem.PrescriptionMedicineId});
      Id = id;
      Buyer = buyer;
      SaleTime = saleTime;
      User = user;
      Medicine = medicineVm;
      NextPrescriptionId = nextPrescriptionId;
      PreviousPrescriptionId = previousPrescriptionId;
    }

    public PrescriptionDetailsVIewModel(Prescription prescription, int? next, int? prev) : this(prescription.Id,
      prescription.Buyer, prescription.SaleTime, new UserViewModel(prescription.User), prescription.Medicine, next,
      prev)
    {
    }

    public int Id { get; }
    public string Buyer { get; }
    public DateTime SaleTime { get; }

    public UserViewModel User { get; }

    //Medicine and amount of it
    public IEnumerable<MedicineDetailsViewModel> Medicine { get; }
    public int? NextPrescriptionId { get; }
    public int? PreviousPrescriptionId { get; }
  }
}