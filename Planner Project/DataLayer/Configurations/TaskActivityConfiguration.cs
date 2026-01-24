using BusinessLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    namespace DataLayer
    {
        public class TaskActivityConfiguration : IEntityTypeConfiguration<TaskActivity>
        {
            public void Configure(EntityTypeBuilder<TaskActivity> builder)
            {

                builder.Property(t => t.DueDate)
                       .IsRequired();

                builder.Property(t => t.IsCompleted)
                       .IsRequired();
            }
        }
    }
}
