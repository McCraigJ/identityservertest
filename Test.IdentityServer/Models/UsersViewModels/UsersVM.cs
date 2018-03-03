using ServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test.IdentityServer.Models.UsersViewModels
{
  public class UsersVM
  {
    public List<UserSM> Users { get; set; }
  }
}
