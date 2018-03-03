﻿using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Test.IdentityServer.Models;

namespace Test.IdentityServer.Services
{
  public class ProfileService : IProfileService
  {
    protected UserManager<ApplicationUser> _userManager;

    public ProfileService(UserManager<ApplicationUser> userManager)
    {
      _userManager = userManager;
    }

    public Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
      //>Processing
      var user = _userManager.GetUserAsync(context.Subject).Result;

      var claims = new List<Claim>
      {
          new Claim("name", user.UserName),
          new Claim("role", user.Role ?? ""),
      };

      context.IssuedClaims.AddRange(claims);

      //>Return
      return Task.FromResult(0);
    }

    public Task IsActiveAsync(IsActiveContext context)
    {
      //>Processing
      var user = _userManager.GetUserAsync(context.Subject).Result;

      context.IsActive = (user != null); // && user.IsActive;

      //>Return
      return Task.FromResult(0);
    }
  }
}
