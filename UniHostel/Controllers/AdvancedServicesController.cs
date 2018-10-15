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
    public class AdvancedServicesController : Controller
    {
        private UniHostelDB db = new UniHostelDB();

        // GET: AdvancedServices
        public ActionResult Index()
        {
            User user = Session["User"] as User;
            if (CheckSession())
            {
                var advancedServices = db.AdvancedServices
                                            .Include(a => a.Host)
                                            .Where(s => s.isActive == true && s.HostID == user.ID);
                return View(advancedServices.ToList());
            }
            return RedirectToAction("Login", "Home");
        }

        // GET: AdvancedServices/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdvancedService advancedService = db.AdvancedServices.Find(id);
            if (advancedService == null)
            {
                return HttpNotFound();
            }
            return View(advancedService);
        }

        // GET: AdvancedServices/Create
        public ActionResult Create()
        {
            if (CheckSession())
            {
                return View();
            }
            return RedirectToAction("Login", "Home");
        }

        // POST: AdvancedServices/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Price,Unit,Description,HostID")] AdvancedService advancedService)
        {
            if (CheckSession())
            {
                if (ModelState.IsValid)
                {
                    advancedService.ID = Utils.getRandomID();
                    advancedService.isActive = true;
                    db.AdvancedServices.Add(advancedService);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError(String.Empty, "All information is required");
                return View(advancedService);
            }
            return RedirectToAction("Login", "Home");
        }

        // GET: AdvancedServices/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdvancedService advancedService = db.AdvancedServices.Find(id);
            if (advancedService == null)
            {
                return HttpNotFound();
            }
            return View(advancedService);
        }

        // POST: AdvancedServices/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Price,Unit,Description,HostID")] AdvancedService advancedService)
        {
            if (ModelState.IsValid)
            {
                var dbService = db.AdvancedServices.Find(advancedService.ID);
                dbService.Name = advancedService.Name;
                dbService.Price = advancedService.Price;
                dbService.Unit = advancedService.Unit;
                dbService.Description = advancedService.Description;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.HostID = new SelectList(db.Hosts, "ID", "FullName", advancedService.HostID);
            return View(advancedService);
        }

        // GET: AdvancedServices/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdvancedService advancedService = db.AdvancedServices.Find(id);
            if (advancedService == null)
            {
                return HttpNotFound();
            }
            return View(advancedService);
        }

        // POST: AdvancedServices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            AdvancedService advancedService = db.AdvancedServices.Find(id);
            advancedService.isActive = false;
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
