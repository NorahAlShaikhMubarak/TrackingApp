using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TrackingApp.Models
{
    public class System_Status_Track
    {
        [Key]
        [Display(Name = "Status trackId")]
        public int Status_track_id { get; set; }

        [Required]
        [Display(Name = "Status trackName")]
        public String Status_track_name { get; set; }
    }
}