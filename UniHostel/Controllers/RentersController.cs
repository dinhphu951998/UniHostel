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
    public class RentersController : Controller
    {
        private UniHostelDB db = new UniHostelDB();

        // GET: Renters
        //Get all bill of renter
        public ActionResult Index()
        {
            User user = Session["User"] as User;
            var bills = db.Bills.Join(db.Renters, r => r.RoomID, renter => renter.RoomID, (b, r) => r)
                                 .Where(renter => renter.ID == user.ID);
            return View(bills.ToList());
        }

        // GET: Renters/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Renter renter = db.Renters.Find(id);
            if (renter == null)
            {
                return HttpNotFound();
            }
            return View(renter);
        }

        // GET: Renters/Create
        public ActionResult Create()
        {
            ViewBag.RoomID = new SelectList(db.Rooms, "ID", "Name");
            ViewBag.ID = new SelectList(db.Users, "ID", "Username");
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FullName,StartDate,EndDate,BirthDate,Mail,HomeTown,Phone,Description,RoomID")] Renter renter)
        {
            if (ModelState.IsValid)
            {
                db.Renters.Add(renter);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RoomID = new SelectList(db.Rooms, "ID", "Name", renter.RoomID);
            ViewBag.ID = new SelectList(db.Users, "ID", "Username", renter.ID);
            return View(renter);
        }

        // GET: Renters/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Renter renter = db.Renters.Find(id);
            if (renter == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoomID = new SelectList(db.Rooms, "ID", "Name", renter.RoomID);
            ViewBag.ID = new SelectList(db.Users, "ID", "Username", renter.ID);
            return View(renter);
        }

        // POST: Renters/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FullName,StartDate,EndDate,BirthDate,Mail,HomeTown,Phone,Description,RoomID")] Renter renter)
        {
            if (ModelState.IsValid)
            {
                db.Entry(renter).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RoomID = new SelectList(db.Rooms, "ID", "Name", renter.RoomID);
            ViewBag.ID = new SelectList(db.Users, "ID", "Username", renter.ID);
            return View(renter);
        }

        // GET: Renters/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Renter renter = db.Renters.Find(id);
            if (renter == null)
            {
                return HttpNotFound();
            }
            return View(renter);
        }

        // POST: Renters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Renter renter = db.Renters.Find(id);
            db.Renters.Remove(renter);
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
