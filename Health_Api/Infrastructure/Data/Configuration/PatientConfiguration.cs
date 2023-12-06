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
    public class PatientConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .HasColumnType("varchar(150)")
                .IsRequired();

            builder.Property(p => p.DateOfBirth)
                .HasColumnType("date")
                .IsRequired();

            builder.Property(p => p.CPF)
                .HasColumnType("varchar(11)")
                .IsFixedLength(true)
                .IsRequired();

            builder.Property(p => p.Email)
                .HasColumnType("varchar(150)")
                .IsRequired();

            builder.Property(p => p.Phone)
                .HasColumnType("varchar(11)")
                .IsFixedLength(true)
                .IsRequired();

            builder.Property(p => p.Address)
                .HasColumnType("varchar(200)")
                .IsRequired();
        }
    }
}
