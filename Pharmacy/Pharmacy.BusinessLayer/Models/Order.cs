using System;
using System.Collections.Generic;

namespace Pharmacy.BusinessLayer.Models
{
  public class Order
  {
    public int Id { get; set; }
    public DateTime OrderIssuedTime { get; set; }
    public DateTime OrderFulfilledTime { get; set; }

    public virtual User User { get; set; }

    public virtual Warehouse Warehouse { get; set; }

    //medicine and amount it was ordered
    public virtual IDictionary<Medicine, int> OrderMedicine { get; set; }
  }
}