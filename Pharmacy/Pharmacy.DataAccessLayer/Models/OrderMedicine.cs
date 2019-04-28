using System.Collections.Generic;

namespace Pharmacy.DataAccessLayer.Models
{
  public class OrderMedicine
  {
    public int MedicineId { get; set; }
    public int OrderId { get; set; }
    public int Amount { get; set; }

    public virtual Medicine Medicine { get; set; }
    public virtual Order Order { get; set; }

    public static IEqualityComparer<OrderMedicine> OrderMedicineComparer { get; } =
      new OrderMedicineEqualityComparer();

    private sealed class OrderMedicineEqualityComparer : IEqualityComparer<OrderMedicine>
    {
      public bool Equals(OrderMedicine x, OrderMedicine y)
      {
        if (ReferenceEquals(x, y)) return true;
        if (ReferenceEquals(x, null)) return false;
        if (ReferenceEquals(y, null)) return false;
        if (x.GetType() != y.GetType()) return false;
        return x.MedicineId == y.MedicineId && x.OrderId == y.OrderId;
      }

      public int GetHashCode(OrderMedicine obj)
      {
        unchecked
        {
          return (obj.MedicineId * 397) ^ obj.OrderId;
        }
      }
    }
  }
}