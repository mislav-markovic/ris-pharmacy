using System;
using System.Collections.Generic;
using System.Linq;
using Pharmacy.BusinessLayer.Models;

namespace Pharmacy.PresentationLayer.Models
{
  public class PrescriptionCreateViewModel
  {
    public PrescriptionCreateViewModel(string buyer, DateTime saleTime, int chosenUser,
      IEnumerable<UserViewModel> availableUsers, IList<PrescriptionMedicine> medicine)
    {
      var medicineVm = medicine.Select(elem => new MedicineDetailsViewModel(elem.Medicine, elem.Amount){PrescriptionMedicineId = elem.PrescriptionMedicineId});
      Buyer = buyer;
      SaleTime = saleTime;
      ChosenUserId = chosenUser;
      AvailableUsers = availableUsers;
      Medicine = medicineVm.ToList();
    }

    public PrescriptionCreateViewModel(Prescription prescription, IEnumerable<UserViewModel> allUsers) : this(
      prescription.Buyer, prescription.SaleTime, prescription.User.Id, allUsers, prescription.Medicine)
    {
    }

    public PrescriptionCreateViewModel()
    {
    }

    public string Buyer { get; set; }
    public DateTime SaleTime { get; set; }

    public int ChosenUserId { get; set; }

    public IEnumerable<UserViewModel> AvailableUsers { get; set; }

    //Medicine and amount of it
    public List<MedicineDetailsViewModel> Medicine { get; set; } = new List<MedicineDetailsViewModel>();
    public IEnumerable<MedicineDetailsViewModel> AvailableMedicine { get; set; }
  }
}