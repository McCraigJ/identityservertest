using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Test.IdentityServer.Models;

namespace Test.IdentityServer.Controllers
{
  public class HomeController : Controller
  {
    public IActionResult Index()
    {
      return Redirect("http://localhost:5002");
    }

    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
  }
}
