using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TrackingApp.Models;

namespace TrackingApp.Controllers
{
    public class System_UserController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: System_User
        [Authorize(Roles ="Admin")]
        public ActionResult Index()
        {
            return View(db.System_User.ToList());
        }

        // GET: System_User/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            System_User system_User = db.System_User.Find(id);
            if (system_User == null)
            {
                return HttpNotFound();
            }
            return View(system_User);
        }

        // GET: System_User/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: System_User/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "User_id,User_name,User_password,User_department,User_email,User_badge")] System_User system_User)
        {
            if (ModelState.IsValid)
            {
                db.System_User.Add(system_User);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(system_User);
        }

        // GET: System_User/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            System_User system_User = db.System_User.Find(id);
            if (system_User == null)
            {
                return HttpNotFound();
            }
            return View(system_User);
        }

        // POST: System_User/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "User_id,User_name,User_password,User_department,User_email,User_badge")] System_User system_User)
        {
            if (ModelState.IsValid)
            {
                db.Entry(system_User).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(system_User);
        }

        // GET: System_User/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            System_User system_User = db.System_User.Find(id);
            if (system_User == null)
            {
                return HttpNotFound();
            }
            return View(system_User);
        }

        // POST: System_User/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            System_User system_User = db.System_User.Find(id);
            db.System_User.Remove(system_User);
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
