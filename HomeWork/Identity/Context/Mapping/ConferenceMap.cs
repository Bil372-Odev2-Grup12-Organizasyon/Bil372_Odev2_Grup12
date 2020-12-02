using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Context.Mapping
{
    public class ConferenceMap : IEntityTypeConfiguration<Conference>
    {
        public void Configure(EntityTypeBuilder<Conference> builder)
        {
            builder.HasKey(I => I.ConfID);
            builder.Property(I => I.ConfID).UseIdentityColumn();
            builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
            builder.Property(x => x.ShortName).HasMaxLength(100).IsRequired();
            builder.Property(x => x.WebSite).HasMaxLength(100).IsRequired();

            builder.HasOne(I => I.AppUser).WithMany(I => I.Conferences).HasForeignKey(I => I.CreateUserId).OnDelete(DeleteBehavior.Cascade);

        }
    }
}