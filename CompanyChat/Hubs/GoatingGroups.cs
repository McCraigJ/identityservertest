using CompanyChat.Interfaces;
using CompanyChat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyChat.Hubs
{
  public class GoatingGroups : IGroups
  {
    public List<GroupSM> Groups { get { return _groups; } }

    private List<GroupSM> _groups { get; set; }

    public GoatingGroups()
    {
      AddGroup(new Models.GroupSM
      {
        Name = "Test1"
      });
      AddGroup(new Models.GroupSM
      {
        Name = "Test2"
      });
    }

    public void AddGroup(GroupSM group)
    {
      if (_groups == null)
      {
        _groups = new List<GroupSM>();
      }

      _groups.Add(group);
    }
    
    public void RemoveGroup(string groupName, string currentUserId)
    {
      if (_groups == null)
      {
        throw new Exception("no groups found");
      }
      var groupToRemove = _groups.Where(x => x.Name == groupName).SingleOrDefault();
      if (groupToRemove == null)
      {
        throw new Exception("no groups found");
      }
      if (groupToRemove.CreatedBy != currentUserId)
      {
        throw new Exception("not allowed");
      }
      _groups.Remove(groupToRemove);

    }
  }
}
