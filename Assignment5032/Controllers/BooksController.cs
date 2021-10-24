using Assignment5032.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment5032.Controllers
{
    public class BooksController : Controller
    {
        // database object
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Books
        public ActionResult Index()
        {
            return HttpNotFound();
        }

        //
        [Authorize(Roles = "Customer")]
        public ActionResult ViewBookedEvents()
        {
            var userId = User.Identity.GetUserId();
            return View(db.Books.Include("Event").Include("Event.EventType").Include("Event.Restaurant").Where(a => a.UserId.ToString() == userId).ToList());
        }

        // 
        [Authorize(Roles = "Customer")]
        public ActionResult DeletBookEvent(int eventId)
        {
            var userId = User.Identity.GetUserId();
            var deleteEvent = db.Books.FirstOrDefault(o => o.EventId == eventId && o.UserId.ToString() == userId);

            if (deleteEvent != null)
            {
                db.Books.Remove(deleteEvent);
                // commit
                db.SaveChanges();
            }
            return RedirectToAction("ViewBookedEvents");
        }

        //
        [Authorize(Roles = "Customer")]
        public ActionResult BookEvent(int id)
        {
            try
            {
            // find user by id
            var userId = User.Identity.GetUserId();
            // int uId = int.Parse(userId);

            // add the booked event to the database
            var bookEvent = new Book { EventId = id, UserId = userId };
            db.Books.Add(bookEvent);
            // commit
            db.SaveChanges();

            var user = db.Users.FirstOrDefault(u => u.Id == userId.ToString());
            bookEvent.Event = db.Events.FirstOrDefault(e => e.EventId == id);
            bookEvent.Event.Restaurant = db.Restaurants.FirstOrDefault(r => r.RestID == bookEvent.Event.RestID);

            return RedirectToAction("ViewBookedEvents");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
               return RedirectToAction("ViewBookedEvents");
            }
        }
    }
}