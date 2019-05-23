using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
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

    public IEnumerable<User> ReadAll()
    {
      return _db.User.Include(u => u.UserRole).Select(u => u.ToBLL());
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