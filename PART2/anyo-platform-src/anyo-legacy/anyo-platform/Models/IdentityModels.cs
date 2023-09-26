using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace anyo_platform.Models
{
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            // Add custom user claims here

            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext() : base("DefaultConnection", throwIfV1Schema: false)
        {

        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<anyo_platform.Models.IntergalacticGroup> IntergalacticGroups { get; set; }

        public System.Data.Entity.DbSet<anyo_platform.Models.IntergalacticMissions> IntergalaticMissions { get; set; }

        public System.Data.Entity.DbSet<anyo_platform.Models.IntergalacticPackages> IntergalacticPackages { get; set; }

        public System.Data.Entity.DbSet<anyo_platform.Models.IntergalacticDonation> IntergalacticDonations { get; set; }
    }
}