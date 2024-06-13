﻿// <auto-generated />
using System;
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
    [Migration("20240613183220_AddedGenderNew")]
    partial class AddedGenderNew
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("LibraryManagementApp.Model.Author", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(1)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PlaceOfBirthId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PlaceOfBirthId");

                    b.ToTable("Authors");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DateOfBirth = new DateTime(1775, 12, 16, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "jane@example.com",
                            FirstName = "Jane",
                            Gender = "F",
                            LastName = "Austen",
                            PlaceOfBirthId = 1
                        },
                        new
                        {
                            Id = 2,
                            DateOfBirth = new DateTime(1903, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "george@example.com",
                            FirstName = "George",
                            Gender = "M",
                            LastName = "Orwell",
                            PlaceOfBirthId = 2
                        },
                        new
                        {
                            Id = 3,
                            DateOfBirth = new DateTime(1890, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "agatha@example.com",
                            FirstName = "Agatha",
                            Gender = "F",
                            LastName = "Christie",
                            PlaceOfBirthId = 3
                        },
                        new
                        {
                            Id = 4,
                            DateOfBirth = new DateTime(1947, 9, 21, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "stephen@example.com",
                            FirstName = "Stephen",
                            Gender = "M",
                            LastName = "King",
                            PlaceOfBirthId = 4
                        },
                        new
                        {
                            Id = 5,
                            DateOfBirth = new DateTime(1965, 7, 31, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "jk@example.com",
                            FirstName = "J.K.",
                            Gender = "F",
                            LastName = "Rowling",
                            PlaceOfBirthId = 5
                        });
                });

            modelBuilder.Entity("LibraryManagementApp.Model.AuthorBook", b =>
                {
                    b.Property<int>("AuthorId")
                        .HasColumnType("int");

                    b.Property<int>("BookId")
                        .HasColumnType("int");

                    b.HasKey("AuthorId", "BookId");

                    b.HasIndex("BookId");

                    b.ToTable("AuthorBooks");

                    b.HasData(
                        new
                        {
                            AuthorId = 1,
                            BookId = 1
                        },
                        new
                        {
                            AuthorId = 1,
                            BookId = 2
                        },
                        new
                        {
                            AuthorId = 1,
                            BookId = 3
                        },
                        new
                        {
                            AuthorId = 1,
                            BookId = 4
                        },
                        new
                        {
                            AuthorId = 1,
                            BookId = 5
                        },
                        new
                        {
                            AuthorId = 2,
                            BookId = 1
                        },
                        new
                        {
                            AuthorId = 2,
                            BookId = 2
                        },
                        new
                        {
                            AuthorId = 2,
                            BookId = 3
                        },
                        new
                        {
                            AuthorId = 2,
                            BookId = 4
                        },
                        new
                        {
                            AuthorId = 3,
                            BookId = 1
                        },
                        new
                        {
                            AuthorId = 3,
                            BookId = 2
                        },
                        new
                        {
                            AuthorId = 3,
                            BookId = 3
                        },
                        new
                        {
                            AuthorId = 4,
                            BookId = 1
                        },
                        new
                        {
                            AuthorId = 4,
                            BookId = 2
                        },
                        new
                        {
                            AuthorId = 5,
                            BookId = 5
                        },
                        new
                        {
                            AuthorId = 5,
                            BookId = 6
                        },
                        new
                        {
                            AuthorId = 5,
                            BookId = 7
                        },
                        new
                        {
                            AuthorId = 5,
                            BookId = 8
                        },
                        new
                        {
                            AuthorId = 5,
                            BookId = 9
                        },
                        new
                        {
                            AuthorId = 5,
                            BookId = 10
                        },
                        new
                        {
                            AuthorId = 5,
                            BookId = 11
                        });
                });

            modelBuilder.Entity("LibraryManagementApp.Model.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("GenreId")
                        .HasColumnType("int");

                    b.Property<DateTime>("PublishDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("GenreId");

                    b.ToTable("Books");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "A romantic novel of manners.",
                            GenreId = 1,
                            PublishDate = new DateTime(1813, 1, 28, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Pride and Prejudice"
                        },
                        new
                        {
                            Id = 2,
                            Description = "A dystopian social science fiction novel.",
                            GenreId = 9,
                            PublishDate = new DateTime(1949, 6, 8, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "1984"
                        },
                        new
                        {
                            Id = 3,
                            Description = "A detective novel.",
                            GenreId = 3,
                            PublishDate = new DateTime(1934, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Murder on the Orient Express"
                        },
                        new
                        {
                            Id = 4,
                            Description = "A horror novel.",
                            GenreId = 4,
                            PublishDate = new DateTime(1977, 1, 28, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "The Shining"
                        },
                        new
                        {
                            Id = 5,
                            Description = "A fantasy novel.",
                            GenreId = 5,
                            PublishDate = new DateTime(1997, 6, 26, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Harry Potter and the Philosopher's Stone"
                        },
                        new
                        {
                            Id = 6,
                            Description = "The second novel in the Harry Potter series.",
                            GenreId = 5,
                            PublishDate = new DateTime(1998, 7, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Harry Potter and the Chamber of Secrets"
                        },
                        new
                        {
                            Id = 7,
                            Description = "The third novel in the Harry Potter series.",
                            GenreId = 5,
                            PublishDate = new DateTime(1999, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Harry Potter and the Prisoner of Azkaban"
                        },
                        new
                        {
                            Id = 8,
                            Description = "The fourth novel in the Harry Potter series.",
                            GenreId = 5,
                            PublishDate = new DateTime(2000, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Harry Potter and the Goblet of Fire"
                        },
                        new
                        {
                            Id = 9,
                            Description = "The fifth novel in the Harry Potter series.",
                            GenreId = 5,
                            PublishDate = new DateTime(2003, 6, 21, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Harry Potter and the Order of the Phoenix"
                        },
                        new
                        {
                            Id = 10,
                            Description = "The sixth novel in the Harry Potter series.",
                            GenreId = 5,
                            PublishDate = new DateTime(2005, 7, 16, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Harry Potter and the Half-Blood Prince"
                        },
                        new
                        {
                            Id = 11,
                            Description = "The seventh novel in the Harry Potter series.",
                            GenreId = 5,
                            PublishDate = new DateTime(2007, 7, 21, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Harry Potter and the Deathly Hallows"
                        });
                });

            modelBuilder.Entity("LibraryManagementApp.Model.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Cities");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Country = "United States",
                            Name = "New York"
                        },
                        new
                        {
                            Id = 2,
                            Country = "Canada",
                            Name = "Toronto"
                        },
                        new
                        {
                            Id = 3,
                            Country = "United Kingdom",
                            Name = "London"
                        },
                        new
                        {
                            Id = 4,
                            Country = "Australia",
                            Name = "Sydney"
                        },
                        new
                        {
                            Id = 5,
                            Country = "Germany",
                            Name = "Berlin"
                        },
                        new
                        {
                            Id = 6,
                            Country = "France",
                            Name = "Paris"
                        },
                        new
                        {
                            Id = 7,
                            Country = "Italy",
                            Name = "Rome"
                        },
                        new
                        {
                            Id = 8,
                            Country = "Spain",
                            Name = "Madrid"
                        },
                        new
                        {
                            Id = 9,
                            Country = "Brazil",
                            Name = "Rio de Janeiro"
                        },
                        new
                        {
                            Id = 10,
                            Country = "India",
                            Name = "Mumbai"
                        },
                        new
                        {
                            Id = 11,
                            Country = "Japan",
                            Name = "Tokyo"
                        },
                        new
                        {
                            Id = 12,
                            Country = "China",
                            Name = "Beijing"
                        });
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

            modelBuilder.Entity("LibraryManagementApp.Model.Author", b =>
                {
                    b.HasOne("LibraryManagementApp.Model.City", "PlaceOfBirth")
                        .WithMany()
                        .HasForeignKey("PlaceOfBirthId");

                    b.Navigation("PlaceOfBirth");
                });

            modelBuilder.Entity("LibraryManagementApp.Model.AuthorBook", b =>
                {
                    b.HasOne("LibraryManagementApp.Model.Author", "Author")
                        .WithMany("AuthorBooks")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LibraryManagementApp.Model.Book", "Book")
                        .WithMany("AuthorBooks")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("Book");
                });

            modelBuilder.Entity("LibraryManagementApp.Model.Book", b =>
                {
                    b.HasOne("LibraryManagementApp.Model.Genre", "BookGenre")
                        .WithMany()
                        .HasForeignKey("GenreId");

                    b.Navigation("BookGenre");
                });

            modelBuilder.Entity("LibraryManagementApp.Model.Author", b =>
                {
                    b.Navigation("AuthorBooks");
                });

            modelBuilder.Entity("LibraryManagementApp.Model.Book", b =>
                {
                    b.Navigation("AuthorBooks");
                });
#pragma warning restore 612, 618
        }
    }
}
