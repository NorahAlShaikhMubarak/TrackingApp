using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FluentScheduler;
using System.Web.Hosting;

namespace TrackingApp.Extension

{
    public class MyRegistry : Registry
    {
        private Extension.ConnectMailController fn = new Extension.ConnectMailController();
        public MyRegistry(int User_id)
        {
            Schedule(() => fn.SendEmail()).ToRunEvery(2).Weeks().At(10, 0);
        }
    }
}