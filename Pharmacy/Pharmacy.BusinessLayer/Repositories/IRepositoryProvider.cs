namespace Pharmacy.BusinessLayer.Repositories
{
  public interface IRepositoryProvider
  {
    IMedicineRepository GetMedicineRepository();
    IOrderRepository GetOrderRepository();
    IPharmacyRepository GetPharmacyRepository();
    IUserRepository GetUserRepository();
  }
}