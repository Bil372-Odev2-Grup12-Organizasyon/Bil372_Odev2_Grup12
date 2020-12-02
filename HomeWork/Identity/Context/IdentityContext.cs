using Identity.Context.Mapping;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Identity.Context
{
    public class IdentityContext : IdentityDbContext<AppUser, AppRole, int>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            optionsBuilder.UseSqlServer("Server=DESKTOP-M1RS6NT;Database=Identity;Trusted_Connection=true;");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ConferenceMap());
            builder.ApplyConfiguration(new ConferenceTagMap());
            builder.ApplyConfiguration(new SubmissionMap());
          

            base.OnModelCreating(builder);
        }

        public DbSet<Conference> Conferences { get; set; }
        public DbSet<ConferenceTags> ConferenceTags { get; set; }
        public DbSet<UserLog> UserLogs { get; set; }
        public DbSet<Submissions> Submissions { get; set; }
       


    }
}