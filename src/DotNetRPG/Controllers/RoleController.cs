using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using DotNetRPG.Models;
using DotNetRPG.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DotNetRPG.Controllers
{
    public class RoleController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        //ALWAYS use a constructor in Controllers

        public RoleController(ApplicationDbContext db, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _roleManager = roleManager;
            _userManager = userManager;
        }
        //Need a constructor to redefine RoleManager because it's automatically null & doesn't update values when program is run

        public IActionResult Index()
        {
            var roles = _db.Roles.ToList();
            return View(roles);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RoleCreateViewModel model)
        {
            var role = new IdentityRole();
            role.Name = model.Name;
            IdentityResult result = await _roleManager.CreateAsync(role);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
        public IActionResult AddUser()
        {
            var list = _db.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name}).ToList();
            ViewBag.Roles = list;
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> AddUser(string UserName, string RoleName)
        {
            ApplicationUser user = _db.Users.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            IdentityResult result = await _userManager.AddToRoleAsync(user, RoleName);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                var list = _db.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
                ViewBag.Roles = list;
                return View();
            }
        }
    }
}
