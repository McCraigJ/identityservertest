using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CompanyChat.Models;
using Microsoft.AspNetCore.Authorization;
using CompanyChat.Interfaces;
using CompanyChat.Models.ViewModels;

namespace CompanyChat.Controllers
{
  public class HomeController : Controller
  {

    private readonly IGroups _groups;

    public HomeController(IGroups groups)
    {
      _groups = groups;
    }

    [Authorize]
    public IActionResult Index()
    {
      var vm = new HomeViewModel
      {
        Groups = _groups.Groups
      };
      return View(vm);
    }

    public IActionResult About()
    {
      ViewData["Message"] = "Your application description page.";

      return View();
    }

    public IActionResult Contact()
    {
      ViewData["Message"] = "Your contact page.";

      return View();
    }

    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }    
  }
}
