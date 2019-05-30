using Pharmacy.BusinessLayer.Models;

namespace Pharmacy.PresentationLayer.Models
{
  public class MedicineDetailsViewModel
  {
    public MedicineDetailsViewModel(int id, string name, decimal price, int amount, int inStockpile)
    {
      Id = id;
      Name = name;
      Price = price;
      Amount = amount;
      InStockpile = inStockpile;
    }

    public MedicineDetailsViewModel(Medicine medicine, int amount, int inStockpile) : this(medicine.Id, medicine.Name, medicine.Price,
      amount, inStockpile)
    {
    }

    public MedicineDetailsViewModel() { }

    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Amount { get; set; }
    public int InStockpile { get; set; }
    public int PrescriptionMedicineId { get; set; }

    protected bool Equals(MedicineDetailsViewModel other)
    {
      return Id == other.Id;
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != GetType()) return false;
      return Equals((MedicineDetailsViewModel) obj);
    }

    public override int GetHashCode()
    {
      return Id;
    }
  }
}