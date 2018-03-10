using CompanyChat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyChat.Interfaces
{
  public interface IGroups
  {
    List<GroupSM> Groups { get; }

    void AddGroup(GroupSM groupName);

    void RemoveGroup(string groupName, string currentUserId);
    
  }
}
