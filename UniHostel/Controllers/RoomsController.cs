 using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using UniHostel.Models;

namespace Hostel1.Controllers
{
    public class RoomsController : Controller
    {
        private UniHostelDB db = new UniHostelDB();

        // GET: Rooms
        public ActionResult Index()
        {
            var user = Session["User"] as User;
            if(user != null)
            {
                var books = from b in db.Rooms where b.HostID == user.ID select b;
                return View(books.ToList());
            }
            return RedirectToAction("Login", "Home");
        }


        public ActionResult RoomManager(String SortPrice,String search, int page = 1)
        {
            var user = Session["User"] as User;
            if (user != null)
            {
                List<Room> room = db.Rooms.Where(r => r.HostID == user.ID).ToList();
                switch (SortPrice)
                {
                    case "Sxtd":
                        room = room.OrderBy(s => s.Price).ToList();
                        break;
                    case "Sxgd":
                        room = room.OrderByDescending(s => s.Price).ToList();//gd
                        break;
                }
                if (!String.IsNullOrEmpty(search))
                {
                    room = room.Where(b => b.Name.Contains(search)).ToList();
                }
                ViewBag.searchName = search;
                return View(room.ToList().ToPagedList(page, 3));
            }
            return RedirectToAction("Login", "Home");
        }


        public ActionResult RoomDetails()
        {
            return View();
        }


        public ActionResult RoomUpdate(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room room = db.Rooms.Find(id);
            if (room == null)
            {
                return HttpNotFound();
            }
            return View(room);
        }


        [HttpPost, ValidateInput(false)]
        public ActionResult RoomUpdate(
            [Bind(Include = "ID,Name,Square,Price,Description,IsAvailable,Image")] Room room, 
            HttpPostedFileBase banner)
        {
            try
            {
                if (banner != null)
                {
                    string extentionName = System.IO.Path.GetExtension(banner.FileName);
                    string finalFileName = DateTime.Now.Ticks.ToString() + extentionName;
                    string path = System.IO.Path.Combine(Server.MapPath("~/asset/RoomImage"), finalFileName);
                    DirectoryInfo fileInfo = new DirectoryInfo(Server.MapPath("~/asset/RoomImage"));
                    if (!fileInfo.Exists)
                    {
                        Directory.CreateDirectory(Server.MapPath("~/asset/RoomImage"));
                    }
                    banner.SaveAs(path);
                    string imgSrc = $"/asset/RoomImage/{finalFileName}";
                    room.Image = imgSrc;
                }
                var oldRoom = db.Rooms.Find(room.ID);
                oldRoom.Image = room.Image;
                oldRoom.Name = room.Name;
                oldRoom.Square = room.Square;
                oldRoom.Price = room.Price;
                db.SaveChanges();
            }
            catch(Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(room);
            }
            return RedirectToAction("RoomManager");
        }



        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room room = db.Rooms.Find(id);
            if (room == null)
            {
                return HttpNotFound();
            }

            return View(room);
        }


        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {
            Room room = db.Rooms.Find(id);
            room.isActive = false;
            db.SaveChanges();
            return RedirectToAction("RoomManager");
        }


    }
}
