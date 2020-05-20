using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TrackingApp.Models;
using System.IO;

namespace TrackingApp.Controllers
{
    public class System_FormController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private Extension.ConnectMailController CM = new Extension.ConnectMailController();

        // GET: System_Form
        public ActionResult Index()
        {
            if (User.IsInRole("User"))
            {
                int user_id = Convert.ToInt32(System.Web.HttpContext.Current.User.Identity.GetUser_id());
                var system_Form = db.System_Form.Include(s => s.System_User).Where(t => t.User_id == user_id);

                return View(system_Form.ToList());
            }

            else
            {
                int user_id = Convert.ToInt32(System.Web.HttpContext.Current.User.Identity.GetUser_id());
                var system_Form = db.System_Form.Include(s => s.System_User);

                return View(system_Form.ToList());
            }
            
        }

        // GET: System_Form/Details/5
        //[Authorize(Roles = "User")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            System_Form system_Form = db.System_Form.Find(id);
            if (system_Form == null)
            {
                return HttpNotFound();
            }
            return View(system_Form);
        }

        // GET: System_Form/Create
        //[Authorize(Roles = "User")]
        public ActionResult Create()
        {
            int user_id = Convert.ToInt32(System.Web.HttpContext.Current.User.Identity.GetUser_id());

            ViewBag.User_id = new SelectList(db.System_User.Where(t => t.User_id== user_id), "User_id", "User_name");
            return View();
        }

        // POST: System_Form/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[Authorize(Roles = "User")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(System_Form system_Form)
        {
            if (ModelState.IsValid)
            {
                List<FileDetail> fileDetails = new List<FileDetail>();
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var file = Request.Files[i];

                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        FileDetail fileDetail = new FileDetail()
                        {
                            FileName = fileName,
                            Extension = Path.GetExtension(fileName),
                            Id = Guid.NewGuid()
                        };
                        fileDetails.Add(fileDetail);

                        var path = Path.Combine(Server.MapPath("~/App_Data/Upload/"), fileDetail.Id + fileDetail.Extension);
                        file.SaveAs(path);
                    }
                }

                system_Form.FileDetails = fileDetails;
                db.System_Form.Add(system_Form);
                db.SaveChanges();
                int User_id = Convert.ToInt32(System.Web.HttpContext.Current.User.Identity.GetUser_id());
                string useremail = (from u in db.Users where u.User_id == User_id select u.Email).SingleOrDefault();
                string username = (from u in db.System_User where u.User_id == User_id select u.User_name).SingleOrDefault();

                //CM.SendEmail(username, useremail);
                return RedirectToAction("Index");

            }

            return View(system_Form);
        }

        // GET: System_Form/Edit/5
        //[Authorize(Roles = "User")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            System_Form system_Form = db.System_Form.Include(s => s.FileDetails).SingleOrDefault(x => x.Form_id == id);
            if (system_Form == null)
            {
                return HttpNotFound();
            }
            ViewBag.User_id = new SelectList(db.System_User, "User_id", "User_name", system_Form.User_id);
            return View(system_Form);
        }

        public FileResult Download(String p, String d)
        {
            return File(Path.Combine(Server.MapPath("~/App_Data/Upload/"), p), System.Net.Mime.MediaTypeNames.Application.Octet, d);
        }


        // POST: System_Form/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[Authorize(Roles = "User")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(System_Form system_Form)
        {
            if (ModelState.IsValid)
            {

                //New Files
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var file = Request.Files[i];

                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        FileDetail fileDetail = new FileDetail()
                        {
                            FileName = fileName,
                            Extension = Path.GetExtension(fileName),
                            Id = Guid.NewGuid(),
                            Form_id = system_Form.Form_id
                        };
                        var path = Path.Combine(Server.MapPath("~/App_Data/Upload/"), fileDetail.Id + fileDetail.Extension);
                        file.SaveAs(path);

                        db.Entry(fileDetail).State = EntityState.Added;
                    }
                }

                db.Entry(system_Form).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(system_Form);
        }

        //[Authorize(Roles = "User")]
        [HttpPost]
        public JsonResult DeleteFile(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { Result = "Error" });
            }
            try
            {
                Guid guid = new Guid(id);
                FileDetail fileDetail = db.FileDetail.Find(guid);
                if (fileDetail == null)
                {
                    Response.StatusCode = (int)HttpStatusCode.NotFound;
                    return Json(new { Result = "Error" });
                }

                //Remove from database
                db.FileDetail.Remove(fileDetail);
                db.SaveChanges();

                //Delete file from the file system
                var path = Path.Combine(Server.MapPath("~/App_Data/Upload/"), fileDetail.Id + fileDetail.Extension);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }


        //DELETE File
        //[Authorize(Roles = "User")]
        [HttpPost]
        public JsonResult Delete(int id)
        {
            try
            {
                System_Form system_Form = db.System_Form.Find(id);
                if (system_Form == null)
                {
                    Response.StatusCode = (int)HttpStatusCode.NotFound;
                    return Json(new { Result = "Error" });
                }

                //delete files from the file system

                foreach (var item in system_Form.FileDetails)
                {
                    String path = Path.Combine(Server.MapPath("~/App_Data/Upload/"), item.Id + item.Extension);
                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }
                }

                db.System_Form.Remove(system_Form);
                db.SaveChanges();
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
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
