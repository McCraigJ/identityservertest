using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ServiceModels;
using Test.IdentityServer.Data;
using Test.IdentityServer.Models;
using Test.IdentityServer.Models.UsersViewModels;

namespace Test.IdentityServer.Controllers
{
  [Authorize]
  [Route("[controller]/[action]")]
  public class UsersController : Controller
  {
    private readonly ApplicationDbContext _context;

    public UsersController(ApplicationDbContext context)
    {
      _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
      var users = await _context.Users.Select(x => new UserSM
      {
        Id = x.Id,
        Email = x.Email,
        Role = x.Role,
        UserName = x.UserName,
        FirstName = x.FirstName,
        LastName = x.LastName
      }).ToListAsync();
      return View(new UsersVM { Users = users });
    }

    [HttpGet]
    public async Task<IActionResult> Update(string id)
    {
      var applicationUser = await _context.ApplicationUser.SingleOrDefaultAsync(m => m.Id == id);

      var user = new UserSM
      {
        Id = applicationUser.Id,
        Email = applicationUser.Email,
        Role = applicationUser.Role,
        UserName = applicationUser.UserName,
        FirstName = applicationUser.FirstName,
        LastName = applicationUser.LastName
      };

      UserRoleVM model = new UserRoleVM { User = user };
      PopulateRoles(model);
      return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Update(UserRoleVM model)
    {
      if (ModelState.IsValid)
      {

        var applicationUser = await _context.ApplicationUser.SingleOrDefaultAsync(m => m.Id == model.User.Id);

        if (User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value != model.User.Id)
        {
          applicationUser.Role = model.Role;
        } else
        {         
          applicationUser.FirstName = model.FirstName;
          applicationUser.LastName = model.LastName;
          await _context.SaveChangesAsync();
          return RedirectToAction("Index");
        }
        
      }
      PopulateRoles(model);
      return View(model);
    }

    private void PopulateRoles(UserRoleVM model)
    {
      model.RolesList = ApplicationRoles.GetRoles().Select(x => new SelectListItem { Value = x, Text = x }).ToList();
    }

    // GET: api/ApplicationUsers
    //[HttpGet]
    //public IEnumerable<ApplicationUser> GetApplicationUser()
    //{
    //  return _context.ApplicationUser;
    //}

    //// GET: api/ApplicationUsers/5
    //[HttpGet("{id}")]
    //public async Task<IActionResult> GetApplicationUser([FromRoute] string id)
    //{
    //  if (!ModelState.IsValid)
    //  {
    //    return BadRequest(ModelState);
    //  }

    //  var applicationUser = await _context.ApplicationUser.SingleOrDefaultAsync(m => m.Id == id);

    //  if (applicationUser == null)
    //  {
    //    return NotFound();
    //  }

    //  return Ok(applicationUser);
    //}

    //// PUT: api/ApplicationUsers/5
    //[HttpPut("{id}")]
    //public async Task<IActionResult> PutApplicationUser([FromRoute] string id, [FromBody] ApplicationUser applicationUser)
    //{
    //  if (!ModelState.IsValid)
    //  {
    //    return BadRequest(ModelState);
    //  }

    //  if (id != applicationUser.Id)
    //  {
    //    return BadRequest();
    //  }

    //  _context.Entry(applicationUser).State = EntityState.Modified;

    //  try
    //  {
    //    await _context.SaveChangesAsync();
    //  }
    //  catch (DbUpdateConcurrencyException)
    //  {
    //    if (!ApplicationUserExists(id))
    //    {
    //      return NotFound();
    //    }
    //    else
    //    {
    //      throw;
    //    }
    //  }

    //  return NoContent();
    //}

    //// POST: api/ApplicationUsers
    //[HttpPost]
    //public async Task<IActionResult> PostApplicationUser([FromBody] ApplicationUser applicationUser)
    //{
    //  if (!ModelState.IsValid)
    //  {
    //    return BadRequest(ModelState);
    //  }

    //  _context.ApplicationUser.Add(applicationUser);
    //  await _context.SaveChangesAsync();

    //  return CreatedAtAction("GetApplicationUser", new { id = applicationUser.Id }, applicationUser);
    //}

    //// DELETE: api/ApplicationUsers/5
    //[HttpDelete("{id}")]
    //public async Task<IActionResult> DeleteApplicationUser([FromRoute] string id)
    //{
    //  if (!ModelState.IsValid)
    //  {
    //    return BadRequest(ModelState);
    //  }

    //  var applicationUser = await _context.ApplicationUser.SingleOrDefaultAsync(m => m.Id == id);
    //  if (applicationUser == null)
    //  {
    //    return NotFound();
    //  }

    //  _context.ApplicationUser.Remove(applicationUser);
    //  await _context.SaveChangesAsync();

    //  return Ok(applicationUser);
    //}

    //private bool ApplicationUserExists(string id)
    //{
    //  return _context.ApplicationUser.Any(e => e.Id == id);
    //}
  }
}