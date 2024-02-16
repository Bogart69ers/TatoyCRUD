using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TatoyCRUD.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            var list = new List<User>();
            using (var db = new CRUDEntities())
            {
                list = db.User.ToList();
            }
                return View(list);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(User u)
        {
            using (var db = new CRUDEntities())
            {
                var newUser = new User();
                newUser.username = u.username;
                newUser.password = u.password;

                db.User.Add(newUser);
                db.SaveChanges();

                TempData["msg"] = $"Added {newUser.username} Successfuly!";

            }

            return RedirectToAction("Index");
        }
        public ActionResult Update(int id)
        {
            var u = new User();
            using (var db = new CRUDEntities())
            {
                u = db.User.Find(id);
            }
                return View(u);
        }
        [HttpPost]
        public ActionResult Update(User u)
        {
            using (var db = new CRUDEntities())
            {
                var newUser = db.User.Find(u.id);
                newUser.username = u.username;
                newUser.password = u.password;
               
                db.SaveChanges();

                TempData["msg"] = $"Updated {newUser.username} Successfuly!";

            }

            return RedirectToAction("Index");
        }
        public ActionResult Remove(int id) 
        {
            var u = new User();
            using (var db = new CRUDEntities())
            {
                u = db.User.Find(id);
                db.User.Remove(u);
                db.SaveChanges();

                TempData["msg"] = $"Deleted {u.username} Succesfuly!";
            }
            return RedirectToAction("Index");
        }

    }
}