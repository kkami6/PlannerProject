using BusinessLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Configurations
{
    public class AppointmentActivityConfiguration : IEntityTypeConfiguration<AppointmentActivity>
    {
        public void Configure(EntityTypeBuilder<AppointmentActivity> builder)
        {

            builder.Property(t => t.StartTime)
                   .IsRequired();

            builder.Property(t => t.EndTime)
                   .IsRequired();

        }
    }
}
