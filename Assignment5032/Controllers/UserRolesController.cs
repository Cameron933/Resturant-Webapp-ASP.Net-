using Assignment5032.Models;
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


        // GET: UserRoles
        public ActionResult Index()
        {
            var roles = this.context.Roles.ToList();
            return View(roles);
        }

        // GET: CreateRole
        public ActionResult CreateRole()
        {
            return View();
        }
        // POST
        [HttpPost]
        public ActionResult CreateRole(FormCollection formCollection)
        {
            try
            {
                var newRole = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole()
                    {
                        Name = formCollection["RoleName"]
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
        public ActionResult ManageRole(String roleName) 
        {
            var role = context.Roles.Where(r => r.Name == roleName).FirstOrDefault();
            return View(role);
        }

        // Edit
        [HttpPost]
        public ActionResult ManageRole(Microsoft.AspNet.Identity.EntityFramework.IdentityRole role)
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
    }

}