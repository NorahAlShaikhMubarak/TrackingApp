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
    public class System_Status_TrackController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: System_Status_Track
        [Authorize(Roles = "User")]
        public ActionResult Index()
        {
            return View(db.System_Status_Track.ToList());
        }

        // GET: System_Status_Track/Details/5
        [Authorize(Roles = "User")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            System_Status_Track system_Status_Track = db.System_Status_Track.Find(id);
            if (system_Status_Track == null)
            {
                return HttpNotFound();
            }
            return View(system_Status_Track);
        }

        // GET: System_Status_Track/Create
        [Authorize(Roles = "User")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: System_Status_Track/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "User")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Status_track_id,Status_track_name")] System_Status_Track system_Status_Track)
        {
            if (ModelState.IsValid)
            {
                db.System_Status_Track.Add(system_Status_Track);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(system_Status_Track);
        }

        // GET: System_Status_Track/Edit/5
        [Authorize(Roles = "User")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            System_Status_Track system_Status_Track = db.System_Status_Track.Find(id);
            if (system_Status_Track == null)
            {
                return HttpNotFound();
            }
            return View(system_Status_Track);
        }

        // POST: System_Status_Track/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "User")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Status_track_id,Status_track_name")] System_Status_Track system_Status_Track)
        {
            if (ModelState.IsValid)
            {
                db.Entry(system_Status_Track).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(system_Status_Track);
        }

        // GET: System_Status_Track/Delete/5
        [Authorize(Roles = "User")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            System_Status_Track system_Status_Track = db.System_Status_Track.Find(id);
            if (system_Status_Track == null)
            {
                return HttpNotFound();
            }
            return View(system_Status_Track);
        }

        // POST: System_Status_Track/Delete/5
        [Authorize(Roles = "User")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            System_Status_Track system_Status_Track = db.System_Status_Track.Find(id);
            db.System_Status_Track.Remove(system_Status_Track);
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
