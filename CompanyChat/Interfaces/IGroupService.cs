using CompanyChat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyChat.Interfaces
{
  interface IGroupService
  {
    //AddGroup
    void AddGroup(GroupSM group);

    void RemoveUser(GroupSM group);

    void AddUserToGroup(GroupSM group, string userId);

    void RemoveUserFromGroup(GroupSM group, string userId);

    List<string> GetUsersForGroup(GroupSM group);

    List<GroupSM> GetGroupsForUser(string userId);

  }
}
