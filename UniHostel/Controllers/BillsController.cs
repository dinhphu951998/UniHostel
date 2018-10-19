using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UniHostel.Models;
using UniHostel.ExtensionMethod;

namespace UniHostel.Controllers
{
    public class BillsController : Controller
    {
        private UniHostelDB db = new UniHostelDB();

        // GET: Bills
        public ActionResult Index()
        {
            if (Session["User"] is User user)
            {
                IEnumerable<Bill> bills = null;
                if (user.Host == null)
                {
                    bills = db.Bills.Where(bill => bill.Room.Renters.OrderByDescending(renter => renter.StartDate).FirstOrDefault().ID == user.ID);
                }
                else
                {
                    bills = db.Bills.Where(bill => bill.Room.HostID == user.ID);
                }
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
            if (Session["User"] is User user)
            {
                ViewBag.RenterID = new SelectList(db.Renters, "ID", "FullName");
                List<Room> rooms = db.Rooms.Include(r => r.Host)
                                           .Where(r => r.HostID == user.ID && r.Host.isActive == true
                                                /*&& !r.Bills.Any(b => DateTime.Now.Month == b.Time.Month && DateTime.Now.Year == b.Time.Year)*/)
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

        private bool CompareIgnoreDay(DateTime d1, DateTime d2)
        {
            if(d1.Month == d2.Month && d2.Year == d1.Year)
            {
                return true;
            }
            return false;
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
                return Json(detailsBill.CurNum, JsonRequestBehavior.AllowGet);

            }
            return Json(0, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( [Bind(Exclude = "ID,Time")] Bill bill,
            string ICollectionCompServicesJSON,
            string ICollectionAdvancedServicesJSON)
        {
            var compulsoryServiceDetailList 
= JsonConvert.DeserializeObject<ICollection<BillCompulsoryServiceDetail>>(ICollectionCompServicesJSON);

            var advancedServiceDetailList
= JsonConvert.DeserializeObject<ICollection<BillAdvancedServiceDetail>>(ICollectionAdvancedServicesJSON);

            if (ModelState.IsValid)
            {
                string billID = Utils.getRandomID();
                bill.ID = billID;
                bill.isPaid = false;
                bill.Time = DateTime.Now;
                foreach (var details in compulsoryServiceDetailList)
                {
                    details.BillID = billID;
                }
                foreach (var details in advancedServiceDetailList)
                {
                    details.BillID = billID;
                }
                bill.BillCompulsoryServiceDetails = compulsoryServiceDetailList;
                bill.BillAdvancedServiceDetails = advancedServiceDetailList;
                db.Bills.Add(bill);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            string messages = string.Join("; ", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));
            ModelState.AddModelError("Error", messages);
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
        public ActionResult Edit([Bind(Include = "ID,Time,OtherFee,Total,isPaid,Description,RoomID")] Bill newBill,
            string ICollectionCompServicesJSON,
            string ICollectionAdvancedServicesJSON)
        {
            var compulsoryServiceDetailList
= JsonConvert.DeserializeObject<ICollection<BillCompulsoryServiceDetail>>(ICollectionCompServicesJSON);

            var advancedServiceDetailList
= JsonConvert.DeserializeObject<ICollection<BillAdvancedServiceDetail>>(ICollectionAdvancedServicesJSON);
            var oldBill = db.Bills.Find(newBill.ID);

            if (ModelState.IsValid)
            {
                newBill.Time = DateTime.Now;
                for (int i = 0; i < compulsoryServiceDetailList.Count(); i++)
                {
                    var details = compulsoryServiceDetailList.ElementAt(i);
                    int detailsID = oldBill.BillCompulsoryServiceDetails.ElementAt(i).ID;
                    details.ID = detailsID;
                    details.BillID = newBill.ID;
                    db.BillCompulsoryServiceDetails.AddOrUpdate(detail => detail.ID, details);
                }
                for (int i = 0; i < advancedServiceDetailList.Count(); i++)
                {
                    var details = advancedServiceDetailList.ElementAt(i);
                    int detailsID = oldBill.BillAdvancedServiceDetails.ElementAt(i).ID;
                    details.ID = detailsID;
                    details.BillID = newBill.ID;
                    db.BillAdvancedServiceDetails.AddOrUpdate(detail => detail.ID, details);
                }
                newBill.BillCompulsoryServiceDetails = compulsoryServiceDetailList;
                newBill.BillAdvancedServiceDetails = advancedServiceDetailList;
                db.Bills.AddOrUpdate(bill => bill.ID, newBill);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(newBill);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateIsPaid(string ID, bool value)
        {
            var bill = db.Bills.Find(ID);
            bill.isPaid = value;
            db.Entry(bill).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Details", new {id = bill.ID });
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
            try
            {
                Bill bill = db.Bills.Find(id);
                db.BillCompulsoryServiceDetails.Remove<BillCompulsoryServiceDetail>(detail => detail.BillID == bill.ID);
                db.BillAdvancedServiceDetails.Remove<BillAdvancedServiceDetail>(detail => detail.BillID == bill.ID);
                db.Bills.Remove(bill);
                db.SaveChanges();
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex }, JsonRequestBehavior.AllowGet);
            }
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
