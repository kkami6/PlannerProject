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
    public class BirthdayActivityConfiguration : IEntityTypeConfiguration<BirthdayActivity>
    {
        public void Configure(EntityTypeBuilder<BirthdayActivity> builder)
        {

            builder.Property(t => t.BirthdayPerson)
                   .IsRequired();

        }
    }
}
