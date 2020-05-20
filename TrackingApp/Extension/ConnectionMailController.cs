using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrackingApp.Models;

namespace TrackingApp.Extension
{
    public class ConnectMailController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public void SendEmail()
        {

            string mailhost = "smtp.live.com";
            int mailport = 25;
            string mailemail = "norahhamad98@outlook.com";
            string mailpassword = "Nourah98fplij";
            IEnumerable<int> Forms = db.System_Form.Select(x => x.Form_id).ToList();
            foreach (int id in Forms)
            {
                var massage = new MimeMessage();
                massage.From.Add(new MailboxAddress("Tracking System", "norahhamad98@outlook.com"));
                var userid = (from u in db.System_Form where u.Form_id == id select u.User_id).SingleOrDefault();
                var username = (from u in db.System_User where u.User_id == userid select u.User_name).SingleOrDefault();
                var useremail = (from u in db.Users where u.User_id == userid select u.Email).SingleOrDefault();
                string formname = (from u in db.System_Form where u.Form_id == id select u.Form_title).SingleOrDefault();
                Boolean checknotification = (from u in db.System_Form where u.Form_id == id select u.Form_notification).SingleOrDefault();


                if (checknotification == true)
                {
                    massage.To.Add(new MailboxAddress(username, useremail));
                    massage.Cc.Add(new MailboxAddress("Tracking System", "norahhamad98@outlook.com"));
                    massage.Subject = "Tracking System Reminder";
                    var bodybuilder = new BodyBuilder();
                    bodybuilder.HtmlBody = @"<strong style='color: blue;'
                                                >Hello! Dear " + username
                        + @"</strong></p></br> Kindly be notified this is a remider of your form <strong style='color: red;'>" + formname + "</strong> with this id <a href='https://localhost:53896/System_Form/Details/'> " + id + "</a>";
                    massage.Body = bodybuilder.ToMessageBody();

                    using (var client = new SmtpClient())
                    {

                        client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                        client.Connect(mailhost, mailport, false);
                        client.Authenticate(mailemail, mailpassword);
                        client.Send(massage);
                        client.Disconnect(true);
                    }

                }

            }
        }
    }
}
