﻿using System.Collections.Generic;

namespace Pharmacy.BusinessLayer.Models
{
  public class Warehouse
  {
    public int Id { get; set; }
    public string Name { get; set; }

    public Location Location { get; set; }
    public ICollection<Pharmacy> PharmaciesSupplied { get; set; }
    public ICollection<Pharmacy> PharmaciesCouldBeSupplied { get; set; }
  }
}