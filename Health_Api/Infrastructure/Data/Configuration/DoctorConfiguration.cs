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
    internal class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.HasKey(d => d.Id);

            builder.Property(d => d.Name)
                .HasColumnType("varchar(150)")
                .IsRequired();

            builder.Property(d => d.DateOfBirth)
                .HasColumnType("date")
                .IsRequired();

            builder.Property(d => d.CPF)
                .HasColumnType("varchar(11)")
                .IsFixedLength(true)
                .IsRequired();

            builder.Property(d => d .Email)
                .HasColumnType("varchar(150)")
                .IsRequired();

            builder.Property(d => d.Phone)
                .HasColumnType("varchar(11)")
                .IsFixedLength(true)
                .IsRequired();

            builder.Property(d => d.Address)
                .HasColumnType("varchar(200)")
                .IsRequired();

            builder.Property(d => d.CRM)
                .HasColumnType("varchar(12)")
                .IsFixedLength(true)
                .IsRequired();
        }
    }
}
