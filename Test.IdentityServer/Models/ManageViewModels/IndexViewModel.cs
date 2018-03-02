using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Test.IdentityServer.Models.ManageViewModels
{
  public class IndexViewModel
  {
    public string Username { get; set; }

    public bool IsEmailConfirmed { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Phone]
    [Display(Name = "Phone number")]
    public string PhoneNumber { get; set; }

    public string StatusMessage { get; set; }

    [StringLength(15, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 0)]
    [Display(Name = "Memorable Word")]
    public string MemorableWord { get; set; }
  }
}
