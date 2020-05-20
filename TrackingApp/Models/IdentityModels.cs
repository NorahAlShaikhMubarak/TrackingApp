using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace TrackingApp.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public int User_id { get; set; }
        public virtual System_User System_User { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            userIdentity.AddClaim(new Claim("User_id", this.User_id.ToString()));
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<TrackingApp.Models.System_User> System_User { get; set; }

        public System.Data.Entity.DbSet<TrackingApp.Models.System_Form> System_Form { get; set; }

        public System.Data.Entity.DbSet<TrackingApp.Models.System_Track> System_Track { get; set; }

        public System.Data.Entity.DbSet<TrackingApp.Models.System_Status_Track> System_Status_Track { get; set; }

        public System.Data.Entity.DbSet<TrackingApp.Models.FileDetail> FileDetail { get; set; }


    }
}