using CompanyChat.Data;
using CompanyChat.Data.Models;
using CompanyChat.Interfaces;
using CompanyChat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyChat.Services
{
  public class GroupService : IGroupService
  {
    private readonly ChatDbContext _ctx;

    public GroupService(ChatDbContext ctx)
    {
      _ctx = ctx;
    }
    public void AddGroup(GroupSM group)
    {
      _ctx.Groups.Add(AutoMapper.Mapper.Map<GroupDM>(group));
      _ctx.SaveChanges();
    }

    public void AddUserToGroup(GroupSM group, string userId)
    {
      throw new NotImplementedException();
    }

    public List<GroupSM> GetGroupsForUser(string userId)
    {
      throw new NotImplementedException();
    }

    public List<string> GetUsersForGroup(GroupSM group)
    {
      throw new NotImplementedException();
    }

    public void RemoveUser(GroupSM group)
    {
      throw new NotImplementedException();
    }

    public void RemoveUserFromGroup(GroupSM group, string userId)
    {
      throw new NotImplementedException();
    }
  }
}
