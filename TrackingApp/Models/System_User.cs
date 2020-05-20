using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TrackingApp.Models
{
    public class System_User
    {
        [Key]
        [Display(Name = "UserID")]
        public int User_id { get; set; }

        [Required]
        [Display(Name = "Username")]
        public String User_name { get; set; }

        [Required]
        [Display(Name = "User password")]
        [DataType(DataType.Password)]
        public int User_password { get; set; }

        [Required]
        [Display(Name = "User department")]
        public String User_department { get; set; }

        [Required]
        [RegularExpression(@"^[_A-Za-z0-9-//+]+(//.[_A-Za-z0-9-//+])*@ksau-hs.edu.sa$", ErrorMessage = "Email should include @ksau-hs.edu.sa")]
        [Display(Name = "User email")]
        public String User_email { get; set; }

        [Required]
        [RegularExpression(@"^\d{6}$", ErrorMessage = "Badge should be 6 numbers only")]
        [Display(Name = "User badge")]
        public String User_badge { get; set; }
    }
}