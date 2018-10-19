using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UniHostel.Models;

namespace UniHostel.Controllers
{
    public class UsersController : Controller
    {
        private UniHostelDB db = new UniHostelDB();

        public ActionResult Details()
        {
            if(Session["User"] is User userSession)
            {
                string resource = "RenterDetails";
                string id = userSession.ID;
                User user = db.Users.Find(id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                if(user.Host != null)
                {
                    resource = "HostDetails";
                }
                return View(resource, user);
            }
            return RedirectToAction("Login", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditHost([Bind(Include = "ID,FullName,Address,Phone,Mail")] Host host)
        {
            //var oldHost = db.Hosts.SingleOrDefault(h => host.ID == h.ID);
            //if (oldHost != null)
            //{
            //    db.Entry(oldHost).State = EntityState.Detached;
            //    db.Entry(host).State = EntityState.Added;
            //    db.SaveChanges();
            //    return RedirectToAction("Details", host.ID);
            //}
            db.Hosts.Attach(host);
            var entry = db.Entry(host);
            entry.Property(h => h.FullName).IsModified = true;
            entry.Property(h => h.Address).IsModified = true;
            entry.Property(h => h.Phone).IsModified = true;
            entry.Property(h => h.Mail).IsModified = true;
            db.SaveChanges();
            return RedirectToAction("Details");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditRenter([Bind(Include = "ID,FullName,BirthDate,Phone,Mail,HomeTown,RoomID")] Renter renter)
        {
            db.Renters.Attach(renter);
            var entry = db.Entry(renter);
            entry.Property(r => r.FullName).IsModified = true;
            entry.Property(r => r.BirthDate).IsModified = true;
            entry.Property(r => r.Phone).IsModified = true;
            entry.Property(r => r.Mail).IsModified = true;
            entry.Property(r => r.HomeTown).IsModified = true;
            entry.Property(r => r.HomeTown).IsModified = true;
            entry.Property(r => r.RoomID).IsModified = true;
            db.SaveChanges();
            return RedirectToAction("Details");
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
