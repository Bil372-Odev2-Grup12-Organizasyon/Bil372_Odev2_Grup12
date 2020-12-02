using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Context.Mapping
{
    public class ConferenceTagMap : IEntityTypeConfiguration<ConferenceTags>
    {
        public void Configure(EntityTypeBuilder<ConferenceTags> builder)
        {

            builder.HasKey(I => I.Id);
            builder.Property(I => I.Id).UseIdentityColumn();
            builder.Property(x => x.Tags).HasMaxLength(100).IsRequired();

            builder.HasOne(I => I.Conference).WithMany(I => I.ConferenceTags).HasForeignKey(I => I.ConfID).OnDelete(DeleteBehavior.Cascade);

        }
    }
}
