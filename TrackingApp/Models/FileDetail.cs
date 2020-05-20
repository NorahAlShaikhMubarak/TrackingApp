using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrackingApp.Models
{
    public class FileDetail
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string Extension { get; set; }

        public int Form_id { get; set; }
        public virtual System_Form system_Form { get; set; }
    }
}