﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Test.IdentityServer.Data.DataModels
{
  [Table(name: "AspNetUsers")]
  public class AspNetUserDM
  {
    [Key]
    public string Id { get; set; }
    public string Email { get; set; }
    public string UserName { get; set; }
    public string Role { get; set; }
  }
}
