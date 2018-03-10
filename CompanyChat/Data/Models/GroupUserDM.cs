using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyChat.Data.Models
{
  [Table(name: "GroupUsers")]
  public class GroupUserDM
  {
    public int Id { get; set; }

    [ForeignKey("GroupId")]
    public GroupDM Group { get; set; }

    public string UserId { get; set; }
  }
}
