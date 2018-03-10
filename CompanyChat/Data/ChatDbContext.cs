using CompanyChat.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyChat.Data
{
  public class ChatDbContext : DbContext
  {
    public ChatDbContext(DbContextOptions<ChatDbContext> options) : base (options)
    {      
    }
    public DbSet<GroupDM> Groups { get; set; }
    public DbSet<GroupUserDM> GroupUsers { get; set; }
  }
}
