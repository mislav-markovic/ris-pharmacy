using System.Collections.Generic;
using Pharmacy.BusinessLayer.Models;

namespace Pharmacy.BusinessLayer.Repositories
{
  public interface IStockpileRepository : ICRUDRepository<Stockpile>
  {
    IEnumerable<Stockpile> ForPharmacy(int pharmacyId);
  }
}