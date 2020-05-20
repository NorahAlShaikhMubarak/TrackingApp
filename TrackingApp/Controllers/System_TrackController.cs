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
    public class System_TrackController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: System_Track
        public ActionResult Index()
        {
            if (User.IsInRole("User"))
            {
                int user_id = Convert.ToInt32(System.Web.HttpContext.Current.User.Identity.GetUser_id());
                var system_Track = db.System_Track.Include(f => f.System_Form).Where(t => t.System_Form.System_User.User_id == user_id);
                return View(system_Track.ToList());
            }
            else
            {
                int user_id = Convert.ToInt32(System.Web.HttpContext.Current.User.Identity.GetUser_id());

                var system_Track = db.System_Track.Include(f => f.System_Form);
                return View(system_Track.ToList());
            }
        }

        // GET: System_Track/Details/5
        [Authorize(Roles = "User")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            System_Track system_Track = db.System_Track.Find(id);
            if (system_Track == null)
            {
                return HttpNotFound();
            }
            return View(system_Track);
        }

        // GET: System_Track/Create
        [Authorize(Roles = "User")]
        public ActionResult Create()
        {
            int user_id = Convert.ToInt32(System.Web.HttpContext.Current.User.Identity.GetUser_id());

            ViewBag.Form_id = new SelectList(db.System_Form.Where(t => t.User_id == user_id), "Form_id", "Form_title");
            ViewBag.Status_track_id = new SelectList(db.System_Status_Track, "Status_track_id", "Status_track_name");
            return View();
        }

        // POST: System_Track/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "User")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Track_id,Form_id,Status_track_id,Track_remark,Track_title,Track_date")] System_Track system_Track)
        {
            if (ModelState.IsValid)
            {
                db.System_Track.Add(system_Track);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Form_id = new SelectList(db.System_Form, "Form_id", "Form_title", system_Track.Form_id);
            ViewBag.Status_track_id = new SelectList(db.System_Status_Track, "Status_track_id", "Status_track_name", system_Track.Status_track_id);
            return View(system_Track);
        }

        // GET: System_Track/Edit/5
        [Authorize(Roles = "User")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            System_Track system_Track = db.System_Track.Find(id);
            if (system_Track == null)
            {
                return HttpNotFound();
            }
            ViewBag.Form_id = new SelectList(db.System_Form, "Form_id", "Form_title", system_Track.Form_id);
            ViewBag.Status_track_id = new SelectList(db.System_Status_Track, "Status_track_id", "Status_track_name", system_Track.Status_track_id);
            return View(system_Track);
        }

        // POST: System_Track/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "User")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Track_id,Form_id,Status_track_id,Track_remark,Track_title,Track_date")] System_Track system_Track)
        {
            if (ModelState.IsValid)
            {
                db.Entry(system_Track).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Form_id = new SelectList(db.System_Form, "Form_id", "Form_title", system_Track.Form_id);
            ViewBag.Status_track_id = new SelectList(db.System_Status_Track, "Status_track_id", "Status_track_name", system_Track.Status_track_id);
            return View(system_Track);
        }

        // GET: System_Track/Delete/5
        [Authorize(Roles = "User")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            System_Track system_Track = db.System_Track.Find(id);
            if (system_Track == null)
            {
                return HttpNotFound();
            }
            return View(system_Track);
        }

        // POST: System_Track/Delete/5
        [Authorize(Roles = "User")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            System_Track system_Track = db.System_Track.Find(id);
            db.System_Track.Remove(system_Track);
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
