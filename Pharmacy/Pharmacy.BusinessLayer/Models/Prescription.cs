using System;
using System.Collections.Generic;

namespace Pharmacy.BusinessLayer.Models
{
  public class Prescription
  {
    public int Id { get; set; }
    public string Buyer { get; set; }
    public DateTime SaleTime { get; set; }

    public User User { get; set; }

    //Medicine and amount of it
    public IList<PrescriptionMedicine> Medicine { get; set; }
  }
}