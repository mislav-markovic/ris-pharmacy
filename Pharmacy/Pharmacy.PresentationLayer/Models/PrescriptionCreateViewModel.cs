using System;
using System.Collections.Generic;
using System.Linq;
using Pharmacy.BusinessLayer.Models;

namespace Pharmacy.PresentationLayer.Models
{
  public class PrescriptionCreateViewModel
  {
    public PrescriptionCreateViewModel(string buyer, DateTime saleTime, UserViewModel chosenUser,
      IEnumerable<UserViewModel> availableUsers, IDictionary<Medicine, int> medicine)
    {
      var medicineVm = medicine.Select(pair =>
      {
        var (k, v) = pair;
        return new MedicineDetailsViewModel(k.Name, k.Price, v);
      });
      Buyer = buyer;
      SaleTime = saleTime;
      ChosenUser = chosenUser;
      AvailableUsers = availableUsers;
      Medicine = medicineVm;
    }

    public PrescriptionCreateViewModel(Prescription prescription, IEnumerable<UserViewModel> allUsers) : this(
      prescription.Buyer, prescription.SaleTime, new UserViewModel(prescription.User), allUsers, prescription.Medicine)
    {
    }

    public string Buyer { get; }
    public DateTime SaleTime { get; }

    public UserViewModel ChosenUser { get; }

    public IEnumerable<UserViewModel> AvailableUsers { get; }

    //Medicine and amount of it
    public IEnumerable<MedicineDetailsViewModel> Medicine { get; }
  }
}