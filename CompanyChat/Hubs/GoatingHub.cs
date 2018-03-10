using CompanyChat.Helpers;
using CompanyChat.Interfaces;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyChat.Hubs
{
  public class GoatingHub : Hub, IServerHub
  {
    private readonly IGroupManager _groupManager;

    public GoatingHub(IGroups groups)
    {      
      //_groupManager = groupManager;
      // get list of groups
      //foreach (var g in groups.Groups)
      //{
      //  Groups.AddAsync(Context.ConnectionId, g.Name);
      //}
    }

    public void Connected()
    {
      Clients.All.InvokeAsync("connected", ($"{GetUserName()} is now watching."));      
    }

    public void Disconnect()
    {
      Clients.All.InvokeAsync("disconnected", ($"{GetUserName()} has left."));
    }

    public void GoatingServer(string msg)
    {
      Clients.All.InvokeAsync("myClientListener", ($"Hello: {msg}"));
    }

    // this will return the name of the chat server just to the caller
    public void ChatName() { Clients.Client(Context.ConnectionId).InvokeAsync("chatName", ("zz_Goating")); }

    public void SendMessage(string msg, string groupName)
    {
      //Clients.AllExcept(new string[] { Context.ConnectionId }).InvokeAsync("receiveMessage", ($"{GetUserName()}: {msg}"));
      Clients.Group(groupName).InvokeAsync("receiveMessage", ($"{GetUserName()}: {msg}"), groupName);
    }

    public void JoinGroup(string groupName)
    {
      Groups.AddAsync(Context.ConnectionId, groupName);
    }

    public void MyName()
    {
      Clients.Client(Context.ConnectionId).InvokeAsync("myName", ($"{GetUserName()}"));
    }

    private string GetUserName()
    {
      var name = ClaimHelper.GetFirstName(Context.User?.Claims);
      if (name != null)
      {
        return name;
      }
      return Context.ConnectionId.Substring(0, 5);
    }
  }
}
