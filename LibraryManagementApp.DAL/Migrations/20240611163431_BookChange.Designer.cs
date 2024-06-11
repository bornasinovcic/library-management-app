﻿// <auto-generated />
using LibraryManagementApp.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LibraryManagementApp.DAL.Migrations
{
    [DbContext(typeof(LibraryDbContext))]
    [Migration("20240611163431_BookChange")]
    partial class BookChange
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("LibraryManagementApp.Model.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("LibraryManagementApp.Model.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Genres");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Romance novel"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Science fiction"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Mystery"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Horror"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Fantasy"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Young adult"
                        },
                        new
                        {
                            Id = 7,
                            Name = "Adventure fiction"
                        },
                        new
                        {
                            Id = 8,
                            Name = "Biography"
                        },
                        new
                        {
                            Id = 9,
                            Name = "Dystopian"
                        },
                        new
                        {
                            Id = 10,
                            Name = "Historical Fiction"
                        },
                        new
                        {
                            Id = 11,
                            Name = "History"
                        },
                        new
                        {
                            Id = 12,
                            Name = "Crime and Thriller"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
