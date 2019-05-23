namespace Pharmacy.PresentationLayer.Models
{
  public class MedicineDetailsViewModel
  {
    public MedicineDetailsViewModel(string name, decimal price, int amount)
    {
      Name = name;
      Price = price;
      Amount = amount;
    }

    public string Name { get; }
    public decimal Price { get; }
    public int Amount { get; }
  }
}