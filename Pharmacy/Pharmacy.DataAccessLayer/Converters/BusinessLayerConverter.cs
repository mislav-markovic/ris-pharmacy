using System.Collections.Generic;
using System.Linq;
using DALModels = Pharmacy.DataAccessLayer.Models;
using BLLModels = Pharmacy.BusinessLayer.Models;

namespace Pharmacy.DataAccessLayer.Converters
{
  public static class BusinessLayerConverter
  {
    public static DALModels.Location ToDAL(this BLLModels.Location model)
    {
      return model == null
        ? null
        : new DALModels.Location {Address = model.Address, City = model.City, LocationId = model.Id};
    }

    public static DALModels.Medicine ToDAL(this BLLModels.Medicine model)
    {
      return model == null
        ? null
        : new DALModels.Medicine {MedicineId = model.Id, Name = model.Name, Price = model.Price};
    }

    public static DALModels.Order ToDAL(this BLLModels.Order model)
    {
      return model == null
        ? null
        : new DALModels.Order
        {
          OrderId = model.Id, UserId = model.User.Id, OrderFulfilledTime = model.OrderFulfilledTime,
          OrderIssuedTime = model.OrderIssuedTime, WarehouseId = model.Warehouse.Id,
          OrderMedicine = model.OrderMedicine.Select(pair => new DALModels.OrderMedicine
            {Amount = pair.Value, MedicineId = pair.Key.Id, OrderId = model.Id}).ToHashSet()
        };
    }

    public static DALModels.Pharmacy ToDAL(this BLLModels.Pharmacy model)
    {
      return model == null
        ? null
        : new DALModels.Pharmacy {Name = model.Name, PharmacyId = model.Id, LocationId = model.Location?.Id};
    }

    public static DALModels.Prescription ToDAL(this BLLModels.Prescription model)
    {
      return model == null
        ? null
        : new DALModels.Prescription
        {
          PrescriptionId = model.Id, SaleTime = model.SaleTime, UserId = model.User.Id, Buyer = model.Buyer,
          PrescriptionMedicine = model.Medicine.Select(
            pair => new DALModels.PrescriptionMedicine
              {Amount = pair.Value, MedicineId = pair.Key.Id, PrescriptionId = model.Id}).ToHashSet()
        };
    }

    public static IEnumerable<DALModels.Stockpile> ToDAL(this BLLModels.Stockpile model)
    {
      return model?.Content.Select(val =>
      {
        var (medicine, value) = val;
        var (alert, amount) = value;
        return new DALModels.Stockpile
        {
          StockpileId = model.Id, Alerts = alert.IsActive ? 1 : 0, Amount = amount, AlertThreshold = alert.Threshold,
          MedicineId = medicine.Id, PharmacyId = model.Pharmacy.Id
        };
      }).AsEnumerable();
    }

    public static DALModels.User ToDAL(this BLLModels.User model)
    {
      return model == null
        ? null
        : new DALModels.User
        {
          PasswordHash = model.PasswordHash, PasswordSalt = model.PasswordSalt, UserId = model.Id,
          PharmacyId = model.Pharmacy.Id, UserRoleId = model.UserRole.Id, Username = model.Username
        };
    }

    public static DALModels.UserRole ToDAL(this BLLModels.UserRole model)
    {
      return model == null ? null : new DALModels.UserRole {RoleName = model.RoleName, UserRoleId = model.Id};
    }

    public static DALModels.Warehouse ToDAL(this BLLModels.Warehouse model)
    {
      var pharmaciesWarehouse = model?.PharmaciesSupplied
        .Select(val => new DALModels.PharmacyWarehouse
          {CanSupply = true, CurrentlySupplies = true, PharmacyId = val.Id, WarehouseId = model.Id}).ToHashSet();

      pharmaciesWarehouse?.UnionWith(model.PharmaciesCouldBeSupplied.Select(val => new DALModels.PharmacyWarehouse
        {CanSupply = true, CurrentlySupplies = false, PharmacyId = val.Id, WarehouseId = model.Id}));

      return model == null
        ? null
        : new DALModels.Warehouse
        {
          WarehouseId = model.Id, Name = model.Name, LocationId = model.Location.Id,
          PharmacyWarehouse = pharmaciesWarehouse
        };
    }
  }
}