using System;
using System.Collections.Generic;
using System.Linq;
using DALModels = Pharmacy.DataAccessLayer.Models;
using BLLModels = Pharmacy.BusinessLayer.Models;

namespace Pharmacy.DataAccessLayer.Converters
{
  public static class DataAccessLayerConverter
  {
    public static BLLModels.Location ToBLL(this DALModels.Location model)
    {
      return model == null
        ? null
        : new BLLModels.Location {Id = model.LocationId, Address = model.Address, City = model.City};
    }

    public static BLLModels.Medicine ToBLL(this DALModels.Medicine model)
    {
      return model == null
        ? null
        : new BLLModels.Medicine {Id = model.MedicineId, Name = model.Name, Price = model.Price};
    }

    public static BLLModels.Order ToBLL(this DALModels.Order model)
    {
      return model == null
        ? null
        : new BLLModels.Order
        {
          Id = model.OrderId, Warehouse = model.Warehouse.ToBLL(), User = model.User.ToBLL(),
          OrderFulfilledTime = model.OrderFulfilledTime, OrderIssuedTime = model.OrderIssuedTime, OrderMedicine = model
            .OrderMedicine.Select(
              val =>
              {
                var key = val.Medicine.ToBLL();
                var value = val.Amount;
                return KeyValuePair.Create(key, value);
              }).ToDictionary(pair => pair.Key, pair => pair.Value)
        };
    }

    public static BLLModels.Pharmacy ToBLL(this DALModels.Pharmacy model)
    {
      var currentSuppliers =
        model?.PharmacyWarehouse.Where(pw => pw.CurrentlySupplies).Select(pw => pw.Warehouse.ToBLL());
      return model == null
        ? null
        : new BLLModels.Pharmacy
        {
          Id = model.PharmacyId, Name = model.Name, Location = model.Location.ToBLL(),
          Stockpile = model.Stockpile?.Select(e => e.ToBLL()).ToList()
        };
    }

    public static BLLModels.Prescription ToBLL(this DALModels.Prescription model)
    {
      return model == null
        ? null
        : new BLLModels.Prescription
        {
          Buyer = model.Buyer, Id = model.PrescriptionId, SaleTime = model.SaleTime, User = model.User.ToBLL(),
          Medicine = model.PrescriptionMedicine?.Select(e => e.ToBLL()).ToList()
        };
    }

    public static BLLModels.Stockpile ToBLL(this DALModels.Stockpile model)
    {
      if (model == null) return null;

      var alert = new BLLModels.Stockpile.Alert
        {IsActive = Convert.ToBoolean(model.Alerts), Threshold = model.AlertThreshold};

      return new BLLModels.Stockpile
      {
        Id = model.StockpileId, PharmacyId = model.PharmacyId, MedicineId = model.MedicineId, Amount = model.Amount,
        MedicineAlert = alert
      };
    }

    public static BLLModels.User ToBLL(this DALModels.User model)
    {
      if (model == null) return null;
      var pharmacy = model.Pharmacy.ToBLL();
      var userRole = model.UserRole.ToBLL();
      return new BLLModels.User
      {
        Id = model.UserId, Pharmacy = pharmacy, UserRole = userRole, PasswordHash = model.PasswordHash,
        PasswordSalt = model.PasswordSalt, Username = model.Username
      };
    }

    public static BLLModels.UserRole ToBLL(this DALModels.UserRole model)
    {
      return model == null ? null : new BLLModels.UserRole {Id = model.UserRoleId, RoleName = model.RoleName};
    }

    public static BLLModels.Warehouse ToBLL(this DALModels.Warehouse model)
    {
      var pharmaciesSupplied = model?.PharmacyWarehouse.Where(pw => pw.CanSupply).Select(pw => pw.Pharmacy.ToBLL())
        .ToHashSet();
      return model == null
        ? null
        : new BLLModels.Warehouse
        {
          Id = model.WarehouseId, Name = model.Name, Location = model.Location.ToBLL(),
          PharmaciesCanBeSupplied = pharmaciesSupplied
        };
    }

    public static BLLModels.PrescriptionMedicine ToBLL(this DALModels.PrescriptionMedicine model)
    {
      return new BLLModels.PrescriptionMedicine
      {
        Amount = model.Amount, Medicine = model.Medicine.ToBLL(), MedicineId = model.MedicineId,
        PrescriptionMedicineId = model.PrescriptionMedicineId, PrescriptionId = model.PrescriptionId
      };
    }
  }
}