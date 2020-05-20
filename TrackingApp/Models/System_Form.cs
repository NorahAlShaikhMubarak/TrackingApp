using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrackingApp.Models
{
    public class System_Form
    {
        [Key]
        [Display(Name = "Form id")]
        public int Form_id { get; set; }

        [Display(Name = "User id")]
        [ForeignKey("User_id")]
        public virtual System_User System_User { get; set; }
        public int User_id { get; set; }

        //[NotMapped]
        ////[DataType(DataType.Upload)]
        //public HttpPostedFileBase File { get; set; }
        //[Display(Name = "Upload File")]
        //[Required (ErrorMessage ="Please choose file")]
        //public string Filename { get; set; }

        [Display(Name = "Form note")]
        public String Form_note { get; set; }

        [Display(Name = "Form remark")]
        public String Form_remark { get; set; }

        [Required]
        [Display(Name = "Form title")]
        public String Form_title { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Form date")]
        public DateTime Form_date { get; set; }

        [Display(Name = "Form notification")]
        public Boolean Form_notification { get; set; }
        [Display(Name = "Form status")]
        public String Form_status { get; set; }

        public virtual ICollection<FileDetail> FileDetails { get; set; }
    }
}