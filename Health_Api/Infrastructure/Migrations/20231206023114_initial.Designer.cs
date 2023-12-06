﻿// <auto-generated />
using System;
using Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(HealthApiContext))]
    [Migration("20231206023114_initial")]
    partial class initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Entities.Models.Appointment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("AppointmentDate")
                        .HasColumnType("datetime");

                    b.Property<int>("DoctorId")
                        .HasColumnType("int");

                    b.Property<int>("PatientId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DoctorId");

                    b.HasIndex("PatientId");

                    b.ToTable("Appointments");
                });

            modelBuilder.Entity("Entities.Models.Doctor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<string>("CPF")
                        .IsRequired()
                        .HasColumnType("varchar(11)")
                        .IsFixedLength();

                    b.Property<string>("CRM")
                        .IsRequired()
                        .HasColumnType("varchar(12)")
                        .IsFixedLength();

                    b.Property<DateOnly>("DateOfBirth")
                        .HasColumnType("date");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(150)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(150)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("varchar(11)")
                        .IsFixedLength();

                    b.HasKey("Id");

                    b.ToTable("Doctors");
                });

            modelBuilder.Entity("Entities.Models.Patient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<string>("CPF")
                        .IsRequired()
                        .HasColumnType("varchar(11)")
                        .IsFixedLength();

                    b.Property<DateOnly>("DateOfBirth")
                        .HasColumnType("date");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(150)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(150)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("varchar(11)")
                        .IsFixedLength();

                    b.HasKey("Id");

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("Entities.Models.Appointment", b =>
                {
                    b.HasOne("Entities.Models.Doctor", "Doctor")
                        .WithMany("Appointments")
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.Models.Patient", "Patient")
                        .WithMany("Appointments")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Doctor");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("Entities.Models.Doctor", b =>
                {
                    b.Navigation("Appointments");
                });

            modelBuilder.Entity("Entities.Models.Patient", b =>
                {
                    b.Navigation("Appointments");
                });
#pragma warning restore 612, 618
        }
    }
}