﻿// <auto-generated />
using FeatureDemo.Api.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace FeatureDemo.Api.Migrations
{
    [DbContext(typeof(FeatureContext))]
    partial class FeatureContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("Feature")
                .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452");

            modelBuilder.Entity("FeatureDemo.Api.Models.Atm", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("InstitutionId");

                    b.Property<bool>("IsVisible");

                    b.Property<double>("Latitude");

                    b.Property<double>("Longitude");

                    b.Property<bool>("ShowCallout");

                    b.Property<string>("Subtitle");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("InstitutionId");

                    b.ToTable("Atms");
                });

            modelBuilder.Entity("FeatureDemo.Api.Models.Institution", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code");

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.ToTable("Institutions");
                });

            modelBuilder.Entity("FeatureDemo.Api.Models.Item", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.ToTable("Item");
                });

            modelBuilder.Entity("FeatureDemo.Api.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email")
                        .HasMaxLength(50);

                    b.Property<string>("FirstName")
                        .HasMaxLength(50);

                    b.Property<Guid>("InstitutionId");

                    b.Property<string>("LastName")
                        .HasMaxLength(50);

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.HasIndex("InstitutionId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("FeatureDemo.Api.Models.Atm", b =>
                {
                    b.HasOne("FeatureDemo.Api.Models.Institution", "Institution")
                        .WithMany("Atms")
                        .HasForeignKey("InstitutionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FeatureDemo.Api.Models.User", b =>
                {
                    b.HasOne("FeatureDemo.Api.Models.Institution", "Institution")
                        .WithMany("Users")
                        .HasForeignKey("InstitutionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
