using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using anyo_platform.Models;

namespace anyo_platform_core.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<anyo_platform.Models.IntergalacticGroup> IntergalacticGroups { get; set; } = default!;
        public DbSet<anyo_platform.Models.IntergalacticMissions> IntergalacticMissions { get; set; } = default!;
        public DbSet<anyo_platform.Models.IntergalacticPackages> IntergalacticPackages { get; set; } = default!;
        public DbSet<anyo_platform.Models.IntergalacticDonation> IntergalacticDonation { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}