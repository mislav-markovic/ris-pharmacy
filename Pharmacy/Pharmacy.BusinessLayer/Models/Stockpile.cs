using System.Collections.Generic;

namespace Pharmacy.BusinessLayer.Models
{
  public class Stockpile
  {
    public IDictionary<Medicine, (Alert, int)> Content { get; set; }

    public class Alert
    {
      public bool IsActive { get; set; }
      public int? Threshold { get; set; }
    }
  }
}