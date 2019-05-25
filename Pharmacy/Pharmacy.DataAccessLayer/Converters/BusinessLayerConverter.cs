using System;
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
          PrescriptionMedicine = model.Medicine?.Select(e => e.ToDAL()).ToHashSet()
        };
    }

    public static DALModels.Stockpile ToDAL(this BLLModels.Stockpile model)
    {
      return model == null
        ? null
        : new DALModels.Stockpile
        {
          StockpileId = model.Id, MedicineId = model.MedicineId, PharmacyId = model.PharmacyId, Amount = model.Amount,
          AlertThreshold = model.MedicineAlert.Threshold, Alerts = Convert.ToInt32(model.MedicineAlert.IsActive)
        };
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
      return model == null
        ? null
        : new DALModels.Warehouse
        {
          WarehouseId = model.Id, Name = model.Name, LocationId = model.Location.Id
        };
    }

    public static DALModels.PrescriptionMedicine ToDAL(this BLLModels.PrescriptionMedicine model)
    {
      return model == null
        ? null
        : new DALModels.PrescriptionMedicine
        {
          Amount = model.Amount, PrescriptionId = model.PrescriptionId, MedicineId = model.MedicineId,
          PrescriptionMedicineId = model.PrescriptionMedicineId, Medicine = model.Medicine?.ToDAL()
        };
    }
  }
}