using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrackingApp.Models
{
    public class System_Track
    {
        [Key]
        [Display(Name = "Track id")]
        public int Track_id { get; set; }

        [ForeignKey("Form_id")]
        public virtual System_Form System_Form { get; set; }
        [Display(Name = "Form id")]
        public int Form_id { get; set; }

        [ForeignKey("Status_track_id")]
        public virtual System_Status_Track System_Status_Track { get; set; }
        [Display(Name = "Status trackId")]
        public int Status_track_id { get; set; }

        [Display(Name = "Track remark")]
        public String Track_remark { get; set; }

        [Required]
        [Display(Name = "Track title")]
        public String Track_title { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Track date")]
        public DateTime Track_date { get; set; }
    }
}