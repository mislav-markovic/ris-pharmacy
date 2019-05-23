using System;
using System.Collections.Generic;
using System.Text;
using Pharmacy.BusinessLayer.Models;
using Pharmacy.BusinessLayer.Repositories;

namespace Pharmacy.BusinessLayer.BusinessComponents
{
  public class UserBC
  {
    private readonly IUserRepository _userRepository;

    public IEnumerable<User> ReadAll()
    {
      return _userRepository.ReadAll();
    }

    public UserBC(IUserRepository userRepository)
    {
      _userRepository = userRepository;
    }
  }
}
