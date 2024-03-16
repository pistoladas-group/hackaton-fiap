﻿// <auto-generated />
using System;
using Hackaton.Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Hackaton.Api.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240316184115_FirstMigration")]
    partial class FirstMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Hackaton.Api.Data.Entities.File", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("UNIQUEIDENTIFIER")
                        .HasColumnName("Id");

                    b.Property<string>("ContentType")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)")
                        .HasColumnName("ContentType");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("DATETIME")
                        .HasColumnName("CreatedAt");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("BIT")
                        .HasColumnName("IsDeleted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)")
                        .HasColumnName("Name");

                    b.Property<int>("ProcessStatusId")
                        .HasColumnType("int");

                    b.Property<int>("SizeInBytes")
                        .HasColumnType("INT")
                        .HasColumnName("SizeInBytes");

                    b.Property<string>("Url")
                        .HasColumnType("VARCHAR(2500)")
                        .HasColumnName("Url");

                    b.HasKey("Id")
                        .HasName("PK_Files");

                    b.ToTable("Files", (string)null);
                });

            modelBuilder.Entity("Hackaton.Api.Data.Entities.FileProcess", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("UNIQUEIDENTIFIER")
                        .HasColumnName("Id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("DATETIME")
                        .HasColumnName("CreatedAt");

                    b.Property<string>("ErrorMessage")
                        .HasColumnType("VARCHAR(1000)")
                        .HasColumnName("ErrorMessage");

                    b.Property<Guid>("FileId")
                        .HasColumnType("UNIQUEIDENTIFIER")
                        .HasColumnName("FileId");

                    b.Property<DateTime?>("FinishedAt")
                        .HasColumnType("DATETIME")
                        .HasColumnName("FinishedAt");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("BIT")
                        .HasColumnName("IsDeleted");

                    b.Property<byte>("ProcessStatusId")
                        .HasColumnType("TINYINT")
                        .HasColumnName("ProcessStatusId");

                    b.Property<byte>("ProcessTypeId")
                        .HasColumnType("TINYINT")
                        .HasColumnName("ProcessTypeId");

                    b.Property<DateTime?>("StartedAt")
                        .HasColumnType("DATETIME")
                        .HasColumnName("StartedAt");

                    b.HasKey("Id")
                        .HasName("PK_FileProcesses");

                    b.ToTable("FileProcesses", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
