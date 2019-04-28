namespace Pharmacy.BusinessLayer.Models
{
  public class Stockpile
  {
    public int Id { get; set; }
    public int PharmacyId { get; set; }
    public int MedicineId { get; set; }
    public int Amount { get; set; }
    public Alert MedicineAlert { get; set; }


    public class Alert
    {
      public bool IsActive { get; set; }
      public int? Threshold { get; set; }
    }
  }
}