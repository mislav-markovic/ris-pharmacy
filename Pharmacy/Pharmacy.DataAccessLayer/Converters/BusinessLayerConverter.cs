using System;
using System.Collections.Generic;
using System.Text;
using DALModels = Pharmacy.DataAccessLayer.Models;
using BLLModels = Pharmacy.BusinessLayer.Models;

namespace Pharmacy.DataAccessLayer.Converters
{
  public static class BusinessLayerConverter
  {
    public static DALModels.Location ToDAL(this BLLModels.Location model)
    {
      return null;
    }

    public static DALModels.Medicine ToDAL(this BLLModels.Medicine model)
    {
      return null;
    }

    public static DALModels.Order ToDAL(this BLLModels.Order model)
    {
      return null;
    }

    public static DALModels.Pharmacy ToDAL(this BLLModels.Pharmacy model)
    {
      return null;
    }

    public static DALModels.Prescription ToDAL(this BLLModels.Prescription model)
    {
      return null;
    }

    public static DALModels.Stockpile ToDAL(this BLLModels.Stockpile model)
    {
      return null;
    }

    public static DALModels.User ToDAL(this BLLModels.User model)
    {
      return null;
    }

    public static DALModels.UserRole ToDAL(this BLLModels.UserRole model)
    {
      return null;
    }

    public static DALModels.Warehouse ToDAL(this BLLModels.Warehouse model)
    {
      return null;
    }
  }
}
