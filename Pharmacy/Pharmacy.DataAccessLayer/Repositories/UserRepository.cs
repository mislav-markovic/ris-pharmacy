using System;
using Pharmacy.BusinessLayer.Repositories;
using Pharmacy.DataAccessLayer.Converters;
using Pharmacy.DataAccessLayer.Models;
using User = Pharmacy.BusinessLayer.Models.User;

namespace Pharmacy.DataAccessLayer.Repositories
{
  public class UserRepository : IUserRepository
  {
    private readonly PharmacyDbContext _db;

    public UserRepository(PharmacyDbContext db)
    {
      _db = db;
    }

    public User Create(User model)
    {
      var dal = model.ToDAL();
      _db.User.Add(dal);
      _db.SaveChanges();
      return _db.User.Find(dal.UserId).ToBLL();
    }

    public User Read(int id)
    {
      throw new NotImplementedException();
    }

    public bool Update(User model)
    {
      throw new NotImplementedException();
    }

    public bool Delete(User model)
    {
      throw new NotImplementedException();
    }
  }
}