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
      return null;
    }

    public static BLLModels.Pharmacy ToBLL(this DALModels.Pharmacy model)
    {
      return null;
    }

    public static BLLModels.Prescription ToBLL(this DALModels.Prescription model)
    {
      return null;
    }

    public static BLLModels.Stockpile ToBLL(this DALModels.Stockpile model)
    {
      return null;
    }

    public static BLLModels.User ToBLL(this DALModels.User model)
    {
      if (model == null) return null;
      var pharmacy = model.Pharmacy.ToBLL();
      var userRole = model.UserRole.ToBLL();
      var prescription = model.Prescription.Select(v => v.ToBLL()).ToHashSet();
      var order = model.Order.Select(v => v.ToBLL()).ToHashSet();
      return new BLLModels.User
      {
        Id = model.UserId, Pharmacy = pharmacy, UserRole = userRole, PasswordHash = model.PasswordHash,
        PasswordSalt = model.PasswordSalt, Username = model.Username, Prescription = prescription, Order = order
      };
    }

    public static BLLModels.UserRole ToBLL(this DALModels.UserRole model)
    {
      return model == null ? null : new BLLModels.UserRole {Id = model.UserRoleId, RoleName = model.RoleName};
    }

    public static BLLModels.Warehouse ToBLL(this DALModels.Warehouse model)
    {
      return null;
    }
  }
}