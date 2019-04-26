using System.Collections.Generic;

namespace Pharmacy.BusinessLayer.Models
{
  public class Pharmacy
  {
    public int Id { get; set; }
    public string Name { get; set; }

    public Location Location { get; set; }
    public ICollection<Warehouse> CurrentSuppliers { get; set; }
    public ICollection<Warehouse> PossibleSuppliers { get; set; }
    public Stockpile Stockpile { get; set; }
    public ICollection<User> Employees { get; set; }
  }
}