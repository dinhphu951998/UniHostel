using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UniHostel.Models;
using UniHostel.Views;

namespace UniHostel.Controllers
{
    public class CompulsoryServicesController : Controller
    {
        private UniHostelDB db = new UniHostelDB();

        // GET: CompulsoryServices
        public ActionResult Index()
        {
            if (CheckSession())
            {
                var compulsoryServices = db.CompulsoryServices.Include(c => c.Host);
                return View(compulsoryServices.ToList());
            }
            return RedirectToAction("Login", "Home");
        }

        // GET: CompulsoryServices/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompulsoryService compulsoryService = db.CompulsoryServices.Find(id);
            if (compulsoryService == null)
            {
                return HttpNotFound();
            }
            return View(compulsoryService);
        }

        // GET: CompulsoryServices/Create
        public ActionResult Create()
        {
            if (CheckSession())
            {
                ViewBag.HostID = new SelectList(db.Hosts, "ID", "FullName");
                return View();
            }
            return RedirectToAction("Login", "Home");
        }

        // POST: CompulsoryServices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Price,Unit,Description,HostID")] CompulsoryService compulsoryService)
        {
            string ID = Utils.getRandomID();
            compulsoryService.ID = ID;
            db.CompulsoryServices.Add(compulsoryService);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: CompulsoryServices/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompulsoryService compulsoryService = db.CompulsoryServices.Find(id);
            if (compulsoryService == null)
            {
                return HttpNotFound();
            }
            ViewBag.HostID = new SelectList(db.Hosts, "ID", "FullName", compulsoryService.HostID);
            return View(compulsoryService);
        }

        // POST: CompulsoryServices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Price,Unit,Description,HostID")] CompulsoryService compulsoryService)
        {
            if (ModelState.IsValid)
            {
                db.Entry(compulsoryService).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.HostID = new SelectList(db.Hosts, "ID", "FullName", compulsoryService.HostID);
            return View(compulsoryService);
        }

        // GET: CompulsoryServices/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompulsoryService compulsoryService = db.CompulsoryServices.Find(id);
            if (compulsoryService == null)
            {
                return HttpNotFound();
            }
            return View(compulsoryService);
        }

        // POST: CompulsoryServices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            CompulsoryService compulsoryService = db.CompulsoryServices.Find(id);
            db.CompulsoryServices.Remove(compulsoryService);
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

        private bool CheckSession()
        {
            User user = Session["User"] as User;
            if (user != null)
            {
                return true;
            }
            return false;
        }
    }
}
