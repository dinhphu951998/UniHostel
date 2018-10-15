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
    public class BillsController : Controller
    {
        private UniHostelDB db = new UniHostelDB();

        // GET: Bills
        public ActionResult Index()
        {
            User user = Session["User"] as User;
            if(user != null)
            {
                var bills = db.Bills.Where(bill => bill.Room.HostID == user.ID);
                return View(bills.ToList());
            }
            return RedirectToAction("Login", "Home");

        }

        // GET: Bills/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bill bill = db.Bills.Find(id);
            if (bill == null)
            {
                return HttpNotFound();
            }
            return View(bill);
        }

        // GET: Bills/Create
        public ActionResult Create()
        {
            User user = Session["User"] as User;
            if (user != null)
            {
                ViewBag.RenterID = new SelectList(db.Renters, "ID", "FullName");
                List<Room> rooms = db.Rooms.Include(r => r.Host)
                                           .Where(r => r.HostID == user.ID && r.Host.isActive == true)
                                           .ToList();
                List<CompulsoryService> compulsoryServices = db.CompulsoryServices.Where(c => c.isActive == true)
                                                                                  .ToList();
                List<AdvancedService> advancedServices = db.AdvancedServices.Where(a => a.isActive == true)
                                                                                  .ToList();
                ViewData["CompulsoryService"] = compulsoryServices;
                ViewData["AdvancedService"] = advancedServices;
                TempData["RoomID"] = rooms.Select(r => new SelectListItem()
                {
                    Text = r.Name + " - " + r.Renters.Where(renter => renter.RoomID == r.ID
                                                           && renter.EndDate == null)
                                                   .Select(renter => renter.FullName)
                                                   .First(),
                    Value = r.ID
                });
                var Bill = new Bill();
                return View(Bill);
            }
            return RedirectToAction("Login", "Home");
        }

        public JsonResult GetPreviousNumber(string CompServiceId, string roomID)
        {
            DateTime now = DateTime.Now;
            var ChosenBill = db.Bills.Where(bill => bill.RoomID == roomID)
                                     .OrderByDescending(bill => bill.Time)
                                     .DefaultIfEmpty(null)
                                     .First();
            if(ChosenBill != null)
            {
                var detailsBill = ChosenBill.BillCompulsoryServiceDetails.Where(details => details.CompulsoryServiceID == CompServiceId)
                                                       .First();
                return Json(detailsBill.PreNum, JsonRequestBehavior.AllowGet);

            }
            return Json(0, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( [Bind(Exclude = "ID,Time")] Bill bill,
            [Bind(Exclude = "ID,Time,BillID")] ICollection<BillAdvancedServiceDetail> advancedServiceDetail,
            [Bind(Exclude = "ID,Time,BillID")] ICollection<BillCompulsoryServiceDetail> compulsoryServiceDetail)
        {
            if (ModelState.IsValid)
            {
                string billID = Utils.getRandomID();
                bill.ID = billID;
                bill.isPaid = false;
                bill.Time = DateTime.Now;
                //bill.BillAdvancedServiceDetails = advancedServiceDetail;
                //bill.BillCompulsoryServiceDetails = compulsoryServiceDetailList;
                //foreach (var details in compulsoryServiceDetailList)
                //{
                //    details.Bill = bill;
                //}
                //foreach (var details in advancedServiceDetailList)
                //{
                //    details.Bill = bill;
                //}
                db.Bills.Add(bill);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            TempData.Keep();
            return View(bill);
        }

        // GET: Bills/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bill bill = db.Bills.Find(id);
            if (bill == null)
            {
                return HttpNotFound();
            }
            return View(bill);
        }

        // POST: Bills/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Time,OtherFee,Total,isPaid,Description,RenterID")] Bill bill)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bill).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bill);
        }

        // GET: Bills/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bill bill = db.Bills.Find(id);
            if (bill == null)
            {
                return HttpNotFound();
            }
            return View(bill);
        }

        // POST: Bills/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Bill bill = db.Bills.Find(id);
            db.Bills.Remove(bill);
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
