﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;
using WebApp.Data;

namespace WebApp.Data.Migrations.ApplicationDbContextMigrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20180526165949_RemoveNameConstraint")]
    partial class RemoveNameConstraint
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WebApp.Models.DataFiles.DataFile", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<byte[]>("Content")
                        .IsRequired();

                    b.Property<byte[]>("Hash");

                    b.Property<bool>("IsProcessed");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("ProcessingError");

                    b.Property<string>("Type");

                    b.HasKey("Id");

                    b.HasIndex("Hash")
                        .IsUnique()
                        .HasFilter("[Hash] IS NOT NULL");

                    b.ToTable("DataFiles");
                });

            modelBuilder.Entity("WebApp.Models.TradePoints.TradePoint", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<Guid?>("TypeId");

                    b.HasKey("Id");

                    b.HasIndex("TypeId");

                    b.ToTable("TradePoints");
                });

            modelBuilder.Entity("WebApp.Models.TradePoints.TradePointSource", b =>
                {
                    b.Property<Guid>("TradePointSourceId");

                    b.Property<string>("DataFileType");

                    b.Property<Guid?>("TradePointId");

                    b.HasKey("TradePointSourceId", "DataFileType");

                    b.HasIndex("TradePointId");

                    b.ToTable("TradePointSources");
                });

            modelBuilder.Entity("WebApp.Models.TradePoints.TradePointType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasFilter("[Name] IS NOT NULL");

                    b.ToTable("TradePointTypes");
                });

            modelBuilder.Entity("WebApp.Models.TradePoints.TradePoint", b =>
                {
                    b.HasOne("WebApp.Models.TradePoints.TradePointType", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId");

                    b.OwnsOne("WebApp.Models.TradePoints.Location", "Location", b1 =>
                        {
                            b1.Property<Guid>("TradePointId");

                            b1.Property<double>("Latitude")
                                .HasColumnName("Latitude");

                            b1.Property<double>("Longitude")
                                .HasColumnName("Longitude");

                            b1.ToTable("TradePoints");

                            b1.HasOne("WebApp.Models.TradePoints.TradePoint")
                                .WithOne("Location")
                                .HasForeignKey("WebApp.Models.TradePoints.Location", "TradePointId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });

            modelBuilder.Entity("WebApp.Models.TradePoints.TradePointSource", b =>
                {
                    b.HasOne("WebApp.Models.TradePoints.TradePoint", "TradePoint")
                        .WithMany()
                        .HasForeignKey("TradePointId");
                });
#pragma warning restore 612, 618
        }
    }
}
