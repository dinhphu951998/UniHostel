using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniHostel.Models;

namespace UniHostel.Controllers
{
    public class HomeController : Controller
    {
        private UniHostelDB _db = new UniHostelDB();

        [HttpGet]
        public ActionResult Login()
        {
            User user = Session["User"] as User;
            if(user != null)
            {
                return RedirectToRoute("Login", new { username = user.Username, password = user.Password });
            }
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            try
            {
                User user = _db.Users.First<User>(u => u.Username == username 
                                                            && u.Password == password 
                                                            && u.isActive == true);
                if (user != null)
                {
                    Session["User"] = user;
                    Session.Timeout = 30;
                    switch (user.RoleID)
                    {
                        case 1: //Admin
                            return RedirectToAction("Index", "Admin");
                        case 2: // Host
                            return RedirectToAction("Index", "Hosts");
                        case 3:
                            return RedirectToAction("Index", "Bills");
                    }
                }
            }
            catch (InvalidOperationException)
            {
                ViewBag.Message = "Username or password is not correct";
            }
            return View();
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetValidUntilExpires(false);
            Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            return RedirectToAction("Login");
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}