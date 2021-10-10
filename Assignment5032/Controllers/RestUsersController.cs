using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Assignment5032.Models;

namespace Assignment5032.Controllers
{
    public class RestUsersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: RestUsers
        public ActionResult Index()
        {
            // return View(db.RestUsers.ToList());
            var restUsers = db.RestUsers.ToList();
            return View(restUsers);
        }

        // GET: RestUsers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RestUser restUser = db.RestUsers.Find(id);
            if (restUser == null)
            {
                return HttpNotFound();
            }
            return View(restUser);
        }

        // GET: RestUsers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RestUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,FirstName,LastName,Dob")] RestUser restUser)
        {
            if (ModelState.IsValid)
            {
                db.RestUsers.Add(restUser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(restUser);
        }

        // GET: RestUsers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RestUser restUser = db.RestUsers.Find(id);
            if (restUser == null)
            {
                return HttpNotFound();
            }
            return View(restUser);
        }

        // POST: RestUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,FirstName,LastName,Dob")] RestUser restUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(restUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(restUser);
        }

        // GET: RestUsers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RestUser restUser = db.RestUsers.Find(id);
            if (restUser == null)
            {
                return HttpNotFound();
            }
            return View(restUser);
        }

        // POST: RestUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RestUser restUser = db.RestUsers.Find(id);
            db.RestUsers.Remove(restUser);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
