using Microsoft.AspNetCore.Mvc.Rendering;
using ServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test.IdentityServer.Models.UsersViewModels
{
  public class UserRoleVM
  {
    public UserSM User { get; set; }

    public string Role { get; set; }

    public List<SelectListItem> RolesList { get; set; }
  }
}
