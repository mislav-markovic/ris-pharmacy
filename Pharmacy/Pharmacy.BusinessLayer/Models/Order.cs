using System;
using System.Collections.Generic;

namespace Pharmacy.BusinessLayer.Models
{
  public class Order
  {
    public int Id { get; set; }
    public DateTime OrderIssuedTime { get; set; }
    public DateTime OrderFulfilledTime { get; set; }

    public User User { get; set; }

    public Warehouse Warehouse { get; set; }

    //medicine and amount it was ordered
    public IDictionary<Medicine, int> OrderMedicine { get; set; }
  }
}