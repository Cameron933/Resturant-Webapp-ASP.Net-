using Assignment5032.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment5032.Controllers
{
    public class UserRolesController : Controller
    {
        // define the database 
        private ApplicationDbContext context = new ApplicationDbContext();
        // Manager the user
        private UserManager<ApplicationUser> userManager;

        // constructor for the userManager
        public UserRolesController()
        {
            var userStore = new UserStore<ApplicationUser>(context);
            userManager = new UserManager<ApplicationUser>(userStore);
        }

        // GET: UserRoles
        public ActionResult Index()
        {
            var roles = this.context.Roles.ToList();
            return View(roles);
        }

        // GET: CreateRole
        public ActionResult CreateRole()
        {
            // Verfy Userrole
            return View(); 
        }

        // POST
        [HttpPost]
        public ActionResult CreateRole(FormCollection fc)
        {
            try
            {
                var newRole = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole()
                    {
                        Name = fc["RoleName"]
                    };

                context.Roles.Add(newRole);
                // Commit
                context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch 
            {
                return View();
            }
        }

        // GET
        public ActionResult EditRole(String roleName) 
        {
            var role = context.Roles.Where(r => r.Name == roleName).FirstOrDefault();
            return View(role);
        }

        // Edit
        [HttpPost]
        public ActionResult EditRole(Microsoft.AspNet.Identity.EntityFramework.IdentityRole role)
        {
            try 
            {
                context.Entry(role).State = System.Data.Entity.EntityState.Modified;
                // commit
                context.SaveChanges();
                // retrun to the page
                return RedirectToAction("Index");
            }
            catch
            {
                return View(role);
            }
        }

        // Delete:
        public ActionResult DeleteRole(String roleName)
        {
            // find the role by name
            var thisRole = context.Roles.Where(o => o.Name == roleName).FirstOrDefault();
            context.Roles.Remove(thisRole);
            // commit
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: UserRoles and Add role to user
        public ActionResult AddRoleToUser()
        {
            // make the role names become a list
            var rolesList = context.Roles.ToList().Select(r => new SelectListItem { Value = r.Name, Text = r.Name }).ToList();
            // list for the user which can be identified by its unique email address
            var usersList = context.Users.ToList().Select(u => new SelectListItem { Value = u.Email, Text = u.Email }).ToList();


            // Pass parameters using viewbag
            ViewBag.Users = usersList;
            ViewBag.Roles = rolesList;
            return View();
        }

        // POST: UserRoles and Add role to user
        // Reference:https://docs.microsoft.com/en-us/answers/questions/470475/error-message-1.html
        // Reference:https://forums.asp.net/t/2141781.aspx?ViewBag+Message+not+displaying+in+view+in+mvc5
        [HttpPost]
        public ActionResult AddRoleToUser(string Email, string Role)
        {
            try
            {
                var user = userManager.FindByEmail(Email);
                userManager.AddToRole(user.Id, Role);
                // Pass parameters using viewbag
                // make the role names become content of the list
                var userRolesList = context.Roles.ToList().Select(o => new SelectListItem { Value = o.Name, Text = o.Name }).ToList();
                // list for the user which can be identified by its unique email address
                var usersList = context.Users.ToList().Select(u => new SelectListItem { Value = u.Email, Text = u.Email }).ToList();
                // Pass parameters using viewbag
                ViewBag.Users = usersList;
                ViewBag.Roles = userRolesList;
            }
            catch (Exception e)
            {
                ViewBag.Errormessage = e.Message;
            }
            

            return View();
        }

        // GET
        public ActionResult GetUserRoles()
        {
            // list for the user which can be identified by its unique email address
            ViewBag.Users = context.Users.ToList().Select(u => new SelectListItem { Value = u.Email, Text = u.Email }).ToList();
            return View();
        }

        // POST
        [HttpPost]
        public ActionResult GetUserRoles(string Email)
        {
            try
            {
                var user = userManager.FindByEmail(Email);
                ViewBag.Roles = userManager.GetRoles(user.Id);
                ViewBag.Users = context.Users.ToList().Select(u => new SelectListItem { Value = u.Email, Text = u.Email }).ToList();
                // another email parameter pass for delete 
                ViewBag.Email = Email;

            }
            catch (Exception e)
            {
                ViewBag.Errormessage = e.Message;

            }
            return View();
        }

        // 
        public ActionResult DeleteUserRole(string Email, string Role)
        {
            var user = userManager.FindByEmail(Email);
            if (this.userManager.IsInRole(user.Id, Role))
            {
                this.userManager.RemoveFromRole(user.Id, Role);
            }
            // use redirect to avoid return view error
            return RedirectToAction("Index");
        }
    }

}