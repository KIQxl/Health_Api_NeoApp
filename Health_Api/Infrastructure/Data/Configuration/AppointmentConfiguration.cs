using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Configuration
{
    public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.PatientId)
                .HasColumnType("int")
                .IsRequired();

            builder.Property(a => a.DoctorId)
                .HasColumnType("int")
                .IsRequired();

            builder.Property(a => a.AppointmentDate)
                .HasColumnType("datetime")
                .IsRequired();
        }
    }
}
