﻿// <auto-generated />
using System;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NetTopologySuite.Geometries;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250225110253_SeedData")]
    partial class SeedData
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.HasPostgresExtension(modelBuilder, "postgis");
            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.Area", b =>
                {
                    b.Property<int>("AreaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("AreaId"));

                    b.Property<Polygon>("Boundary")
                        .IsRequired()
                        .HasColumnType("geometry");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("ExpositionId")
                        .HasColumnType("integer");

                    b.Property<double>("Latitude")
                        .HasColumnType("double precision");

                    b.Property<double>("Longitude")
                        .HasColumnType("double precision");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("AreaId");

                    b.HasIndex("ExpositionId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Areas");
                });

            modelBuilder.Entity("Domain.Entities.BiometryRecord", b =>
                {
                    b.Property<int>("BiometryRecordId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("BiometryRecordId"));

                    b.Property<int?>("BudsCount")
                        .HasColumnType("integer");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double?>("FlowerDiameter")
                        .HasColumnType("double precision");

                    b.Property<double?>("HeightCm")
                        .HasColumnType("double precision");

                    b.Property<DateTime>("MeasureDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("PlantId")
                        .HasColumnType("integer");

                    b.HasKey("BiometryRecordId");

                    b.HasIndex("PlantId");

                    b.ToTable("BiometryRecords");
                });

            modelBuilder.Entity("Domain.Entities.Exposition", b =>
                {
                    b.Property<int>("ExpositionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ExpositionId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ExpositionName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ExpositionId");

                    b.HasIndex("ExpositionName")
                        .IsUnique();

                    b.ToTable("Expositions");

                    b.HasData(
                        new
                        {
                            ExpositionId = 1,
                            Description = "Коллекция древесных растений",
                            ExpositionName = "Дендрология"
                        },
                        new
                        {
                            ExpositionId = 2,
                            Description = "Коллекция растений местной флоры",
                            ExpositionName = "Флора"
                        },
                        new
                        {
                            ExpositionId = 3,
                            Description = "Коллекция декоративных цветочных растений",
                            ExpositionName = "Цветоводство"
                        });
                });

            modelBuilder.Entity("Domain.Entities.PhenologyRecord", b =>
                {
                    b.Property<int>("PhenologyRecordId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("PhenologyRecordId"));

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("DateBloom")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DateBudBurst")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DateFruiting")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DateLeafFall")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("ObservationYear")
                        .HasColumnType("integer");

                    b.Property<int>("PlantId")
                        .HasColumnType("integer");

                    b.HasKey("PhenologyRecordId");

                    b.HasIndex("PlantId");

                    b.ToTable("PhenologyRecords");
                });

            modelBuilder.Entity("Domain.Entities.Plant", b =>
                {
                    b.Property<int>("PlantId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("PlantId"));

                    b.Property<int?>("AreaId")
                        .HasColumnType("integer");

                    b.Property<string>("ConservationStatus")
                        .HasColumnType("text");

                    b.Property<int?>("CreatedById")
                        .HasColumnType("integer");

                    b.Property<string>("Cultivar")
                        .HasColumnType("text");

                    b.Property<string>("EcologyBiology")
                        .HasColumnType("text");

                    b.Property<int>("ExpositionId")
                        .HasColumnType("integer");

                    b.Property<string>("Form")
                        .HasColumnType("text");

                    b.Property<string>("Genus")
                        .HasColumnType("text");

                    b.Property<string>("InventoryNumber")
                        .HasColumnType("text");

                    b.Property<double?>("Latitude")
                        .HasColumnType("double precision");

                    b.Property<Point>("Location")
                        .HasColumnType("geometry");

                    b.Property<double?>("Longitude")
                        .HasColumnType("double precision");

                    b.Property<string>("NaturalRange")
                        .HasColumnType("text");

                    b.Property<string>("Note")
                        .HasColumnType("text");

                    b.Property<string>("Origin")
                        .HasColumnType("text");

                    b.Property<int>("PlantFamilyId")
                        .HasColumnType("integer");

                    b.Property<string>("Species")
                        .HasColumnType("text");

                    b.Property<string>("Usage")
                        .HasColumnType("text");

                    b.Property<int?>("YearPlanted")
                        .HasColumnType("integer");

                    b.HasKey("PlantId");

                    b.HasIndex("AreaId");

                    b.HasIndex("CreatedById");

                    b.HasIndex("ExpositionId");

                    b.HasIndex("InventoryNumber")
                        .IsUnique();

                    b.HasIndex("PlantFamilyId");

                    b.ToTable("Plants");
                });

            modelBuilder.Entity("Domain.Entities.PlantFamily", b =>
                {
                    b.Property<int>("PlantFamilyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("PlantFamilyId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("PlantFamilyId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("PlantFamilies");
                });

            modelBuilder.Entity("Domain.Entities.PlantImage", b =>
                {
                    b.Property<int>("PlantImageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("PlantImageId"));

                    b.Property<string>("Caption")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ImagePath")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("PlantId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UploadDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("UploadedById")
                        .HasColumnType("integer");

                    b.HasKey("PlantImageId");

                    b.HasIndex("PlantId");

                    b.HasIndex("UploadedById");

                    b.ToTable("PlantImages");
                });

            modelBuilder.Entity("Domain.Entities.PlantMovement", b =>
                {
                    b.Property<int>("PlantMovementId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("PlantMovementId"));

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("DestinationAreaId")
                        .HasColumnType("integer");

                    b.Property<int>("MovedById")
                        .HasColumnType("integer");

                    b.Property<DateTime>("MovementDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Point>("NewLocation")
                        .IsRequired()
                        .HasColumnType("geometry");

                    b.Property<Point>("OldLocation")
                        .IsRequired()
                        .HasColumnType("geometry");

                    b.Property<int>("PlantId")
                        .HasColumnType("integer");

                    b.Property<string>("Reason")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("SourceAreaId")
                        .HasColumnType("integer");

                    b.HasKey("PlantMovementId");

                    b.HasIndex("DestinationAreaId");

                    b.HasIndex("MovedById");

                    b.HasIndex("PlantId");

                    b.HasIndex("SourceAreaId");

                    b.ToTable("PlantMovements");
                });

            modelBuilder.Entity("Domain.Entities.PlantStatus", b =>
                {
                    b.Property<int>("PlantStatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("PlantStatusId"));

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("PlantId")
                        .HasColumnType("integer");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("StatusDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("UpdatedById")
                        .HasColumnType("integer");

                    b.HasKey("PlantStatusId");

                    b.HasIndex("PlantId");

                    b.HasIndex("UpdatedById");

                    b.ToTable("PlantStatuses");
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("UserId"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("UserId");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Domain.Entities.Area", b =>
                {
                    b.HasOne("Domain.Entities.Exposition", "Exposition")
                        .WithMany()
                        .HasForeignKey("ExpositionId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Exposition");
                });

            modelBuilder.Entity("Domain.Entities.BiometryRecord", b =>
                {
                    b.HasOne("Domain.Entities.Plant", "Plant")
                        .WithMany("BiometryRecords")
                        .HasForeignKey("PlantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Plant");
                });

            modelBuilder.Entity("Domain.Entities.PhenologyRecord", b =>
                {
                    b.HasOne("Domain.Entities.Plant", "Plant")
                        .WithMany("PhenologyRecords")
                        .HasForeignKey("PlantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Plant");
                });

            modelBuilder.Entity("Domain.Entities.Plant", b =>
                {
                    b.HasOne("Domain.Entities.Area", "Area")
                        .WithMany("Plants")
                        .HasForeignKey("AreaId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("Domain.Entities.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Domain.Entities.Exposition", "Exposition")
                        .WithMany()
                        .HasForeignKey("ExpositionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Domain.Entities.PlantFamily", "PlantFamily")
                        .WithMany()
                        .HasForeignKey("PlantFamilyId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Area");

                    b.Navigation("CreatedBy");

                    b.Navigation("Exposition");

                    b.Navigation("PlantFamily");
                });

            modelBuilder.Entity("Domain.Entities.PlantImage", b =>
                {
                    b.HasOne("Domain.Entities.Plant", "Plant")
                        .WithMany("PlantImages")
                        .HasForeignKey("PlantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.User", "UploadedBy")
                        .WithMany()
                        .HasForeignKey("UploadedById")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Plant");

                    b.Navigation("UploadedBy");
                });

            modelBuilder.Entity("Domain.Entities.PlantMovement", b =>
                {
                    b.HasOne("Domain.Entities.Area", "DestinationArea")
                        .WithMany()
                        .HasForeignKey("DestinationAreaId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("Domain.Entities.User", "MovedBy")
                        .WithMany()
                        .HasForeignKey("MovedById")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Plant", "Plant")
                        .WithMany("PlantMovements")
                        .HasForeignKey("PlantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Area", "SourceArea")
                        .WithMany()
                        .HasForeignKey("SourceAreaId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("DestinationArea");

                    b.Navigation("MovedBy");

                    b.Navigation("Plant");

                    b.Navigation("SourceArea");
                });

            modelBuilder.Entity("Domain.Entities.PlantStatus", b =>
                {
                    b.HasOne("Domain.Entities.Plant", "Plant")
                        .WithMany("PlantStatuses")
                        .HasForeignKey("PlantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.User", "UpdatedBy")
                        .WithMany()
                        .HasForeignKey("UpdatedById")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Plant");

                    b.Navigation("UpdatedBy");
                });

            modelBuilder.Entity("Domain.Entities.Area", b =>
                {
                    b.Navigation("Plants");
                });

            modelBuilder.Entity("Domain.Entities.Plant", b =>
                {
                    b.Navigation("BiometryRecords");

                    b.Navigation("PhenologyRecords");

                    b.Navigation("PlantImages");

                    b.Navigation("PlantMovements");

                    b.Navigation("PlantStatuses");
                });
#pragma warning restore 612, 618
        }
    }
}
