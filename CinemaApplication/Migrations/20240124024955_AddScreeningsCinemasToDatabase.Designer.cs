﻿// <auto-generated />
using System;
using CinemaApplication.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CinemaApplication.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240124024955_AddScreeningsCinemasToDatabase")]
    partial class AddScreeningsCinemasToDatabase
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CinemaApplication.Models.Cinema", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Seats")
                        .HasColumnType("int");

                    b.Property<string>("ThreeDim")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Name");

                    b.ToTable("Cinemas");
                });

            modelBuilder.Entity("CinemaApplication.Models.Movie", b =>
                {
                    b.Property<string>("MovieName")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ContentAdminUsername")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Director")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Genre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Length")
                        .HasColumnType("int");

                    b.Property<string>("MovieDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MovieName");

                    b.HasIndex("ContentAdminUsername");

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("CinemaApplication.Models.Screening", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CinemaName")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ContentAdminUsername")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("MovieName")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CinemaName");

                    b.HasIndex("ContentAdminUsername");

                    b.HasIndex("MovieName");

                    b.ToTable("Screenings");
                });

            modelBuilder.Entity("CinemaApplication.Models.User", b =>
                {
                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Username");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("CinemaApplication.Models.Admin", b =>
                {
                    b.HasBaseType("CinemaApplication.Models.User");

                    b.Property<Guid>("AdminId")
                        .HasColumnType("uniqueidentifier");

                    b.HasIndex("AdminId")
                        .IsUnique()
                        .HasFilter("[AdminId] IS NOT NULL");

                    b.ToTable("Admins", (string)null);
                });

            modelBuilder.Entity("CinemaApplication.Models.ContentAdmin", b =>
                {
                    b.HasBaseType("CinemaApplication.Models.User");

                    b.Property<Guid>("ContentAdminId")
                        .HasColumnType("uniqueidentifier");

                    b.HasIndex("ContentAdminId")
                        .IsUnique()
                        .HasFilter("[ContentAdminId] IS NOT NULL");

                    b.ToTable("ContentAdmins", (string)null);
                });

            modelBuilder.Entity("CinemaApplication.Models.Customer", b =>
                {
                    b.HasBaseType("CinemaApplication.Models.User");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.HasIndex("CustomerId")
                        .IsUnique()
                        .HasFilter("[CustomerId] IS NOT NULL");

                    b.ToTable("Customers", (string)null);
                });

            modelBuilder.Entity("CinemaApplication.Models.Movie", b =>
                {
                    b.HasOne("CinemaApplication.Models.ContentAdmin", "ContentAdmin")
                        .WithMany("Movies")
                        .HasForeignKey("ContentAdminUsername")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ContentAdmin");
                });

            modelBuilder.Entity("CinemaApplication.Models.Screening", b =>
                {
                    b.HasOne("CinemaApplication.Models.Cinema", "Cinema")
                        .WithMany("Screenings")
                        .HasForeignKey("CinemaName");

                    b.HasOne("CinemaApplication.Models.ContentAdmin", "ContentAdmin")
                        .WithMany("Screenings")
                        .HasForeignKey("ContentAdminUsername")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CinemaApplication.Models.Movie", "Movie")
                        .WithMany("Screenings")
                        .HasForeignKey("MovieName");

                    b.Navigation("Cinema");

                    b.Navigation("ContentAdmin");

                    b.Navigation("Movie");
                });

            modelBuilder.Entity("CinemaApplication.Models.Admin", b =>
                {
                    b.HasOne("CinemaApplication.Models.User", null)
                        .WithOne()
                        .HasForeignKey("CinemaApplication.Models.Admin", "Username")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CinemaApplication.Models.ContentAdmin", b =>
                {
                    b.HasOne("CinemaApplication.Models.User", null)
                        .WithOne()
                        .HasForeignKey("CinemaApplication.Models.ContentAdmin", "Username")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CinemaApplication.Models.Customer", b =>
                {
                    b.HasOne("CinemaApplication.Models.User", null)
                        .WithOne()
                        .HasForeignKey("CinemaApplication.Models.Customer", "Username")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CinemaApplication.Models.Cinema", b =>
                {
                    b.Navigation("Screenings");
                });

            modelBuilder.Entity("CinemaApplication.Models.Movie", b =>
                {
                    b.Navigation("Screenings");
                });

            modelBuilder.Entity("CinemaApplication.Models.ContentAdmin", b =>
                {
                    b.Navigation("Movies");

                    b.Navigation("Screenings");
                });
#pragma warning restore 612, 618
        }
    }
}