using System;
using System.Collections.Generic;
using System.Text;

namespace Pharmacy.BusinessLayer.Models
{
  public class PrescriptionMedicine
  {
    public int PrescriptionId { get; set; }
    public int MedicineId { get; set; }
    public int Amount { get; set; }
    public int PrescriptionMedicineId { get; set; }
    public  Medicine Medicine { get; set; }
  }
}
