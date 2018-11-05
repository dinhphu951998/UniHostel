 using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Hostel1.Models;
using PagedList;
namespace Hostel1.Controllers
{
    public class RoomsController : Controller
    {
        private RoomDBContext db = new RoomDBContext();

        // GET: Rooms
        public ActionResult Index()
        {
            var books = from b in db.Rooms select b;
            return View(books.ToList());
        }


        public ActionResult RoomManager(String SortPrice,String search, int page = 1)
        {
            List<Rooms> room = db.Rooms.ToList();
            switch (SortPrice)
            {
                case "Sxtd":
                    room = room.OrderBy(s => s.Price).ToList();
                    break;
                case "Sxgd":
                    room = room.OrderByDescending(s => s.Price).ToList();//gd
                    break;
            }
            if (!String.IsNullOrEmpty(search)){
                room = room.Where(b => b.Name.Contains(search)).ToList();
            }
            ViewBag.searchName = search;
            return View(room.ToList().ToPagedList(page,3));
        }
        public ActionResult RoomDetails()
        {
            return View();
        }
        public ActionResult RoomUpdate(int? id)
        {
            //Rooms room = db.Rooms.Where(_ => _.ID == id).FirstOrDefault();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rooms room = db.Rooms.Find(id);
            if (room == null)
            {
                return HttpNotFound();
            }


            return View(room);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult RoomUpdate([Bind(Include = "ID,Name,Square,Price,Description,IsAvailable,Image")] Rooms room, HttpPostedFileBase banner)
        {
            string extentionName = System.IO.Path.GetExtension(banner.FileName);
            string finalFileName = DateTime.Now.Ticks.ToString() + extentionName;
            string path = System.IO.Path.Combine(Server.MapPath("~/asset/upload"), finalFileName);

            banner.SaveAs(path);
            string imgSrc = $"/asset/upload/{finalFileName}";

            room.Image = imgSrc;
            try
            {
                db.Entry(room).State = EntityState.Modified;
                db.SaveChanges();
                
            }
            catch(Exception e)
            {
                return View(room);
            }
            return RedirectToAction("RoomManager");
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rooms room = db.Rooms.Find(id);
            if (room == null)
            {
                return HttpNotFound();
            }

            return View(room);
        }


        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed([Bind(Include = "ID,Image")] int id)
        {
            Rooms room = db.Rooms.Find(id);
            db.Rooms.Remove(room);
            db.SaveChanges();
            return RedirectToAction("RoomManager");
        }


    }
}
