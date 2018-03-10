using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyChat.Data.Models
{
  [Table(name: "Groups")]
  public class GroupDM
  {
    [Key]
    public int Id { get; set; }
    public string GroupName { get; set; }
    public string GroupDescription { get; set; }
    public string CreatedByUserId { get; set; }
    public virtual ICollection<GroupUserDM> GroupUsers { get; set; }

  }
}
