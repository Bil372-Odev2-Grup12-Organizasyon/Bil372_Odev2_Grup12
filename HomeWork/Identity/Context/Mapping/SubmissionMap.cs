using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Context.Mapping
{
    public class SubmissionMap : IEntityTypeConfiguration<Submissions>
    {
        public void Configure(EntityTypeBuilder<Submissions> builder)
        {
            builder.HasKey(I => I.SubmissionID);
            builder.Property(I => I.SubmissionID).UseIdentityColumn();

            
            builder.HasOne(I => I.Conference).WithMany(I => I.Submissions).HasForeignKey(I => I.ConfId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
