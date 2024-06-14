using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibraryManagementApp.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PlaceOfBirthId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Authors_Cities_PlaceOfBirthId",
                        column: x => x.PlaceOfBirthId,
                        principalTable: "Cities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PublishDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GenreId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AuthorBooks",
                columns: table => new
                {
                    AuthorId = table.Column<int>(type: "int", nullable: false),
                    BookId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorBooks", x => new { x.AuthorId, x.BookId });
                    table.ForeignKey(
                        name: "FK_AuthorBooks_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuthorBooks_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "Country", "Name" },
                values: new object[,]
                {
                    { 1, "United States", "New York" },
                    { 2, "Canada", "Toronto" },
                    { 3, "United Kingdom", "London" },
                    { 4, "Australia", "Sydney" },
                    { 5, "Germany", "Berlin" },
                    { 6, "France", "Paris" },
                    { 7, "Italy", "Rome" },
                    { 8, "Spain", "Madrid" },
                    { 9, "Brazil", "Rio de Janeiro" },
                    { 10, "India", "Mumbai" },
                    { 11, "Japan", "Tokyo" },
                    { 12, "China", "Beijing" }
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Romance novel" },
                    { 2, "Science fiction" },
                    { 3, "Mystery" },
                    { 4, "Horror" },
                    { 5, "Fantasy" },
                    { 6, "Young adult" },
                    { 7, "Adventure fiction" },
                    { 8, "Biography" },
                    { 9, "Dystopian" },
                    { 10, "Historical Fiction" },
                    { 11, "History" },
                    { 12, "Crime and Thriller" }
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "DateOfBirth", "Email", "FirstName", "Gender", "LastName", "PlaceOfBirthId" },
                values: new object[,]
                {
                    { 1, new DateTime(1775, 12, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "jane@example.com", "Jane", "F", "Austen", 1 },
                    { 2, new DateTime(1903, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "george@example.com", "George", "M", "Orwell", 2 },
                    { 3, new DateTime(1890, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "agatha@example.com", "Agatha", "F", "Christie", 3 },
                    { 4, new DateTime(1947, 9, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "stephen@example.com", "Stephen", "M", "King", 4 },
                    { 5, new DateTime(1965, 7, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "jk@example.com", "J.K.", "F", "Rowling", 5 }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Description", "GenreId", "PublishDate", "Title" },
                values: new object[,]
                {
                    { 1, "A romantic novel of manners.", 1, new DateTime(1813, 1, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pride and Prejudice" },
                    { 2, "A dystopian social science fiction novel.", 9, new DateTime(1949, 6, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "1984" },
                    { 3, "A detective novel.", 3, new DateTime(1934, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Murder on the Orient Express" },
                    { 4, "A horror novel.", 4, new DateTime(1977, 1, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Shining" },
                    { 5, "A fantasy novel.", 5, new DateTime(1997, 6, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Harry Potter and the Philosopher's Stone" },
                    { 6, "The second novel in the Harry Potter series.", 5, new DateTime(1998, 7, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Harry Potter and the Chamber of Secrets" },
                    { 7, "The third novel in the Harry Potter series.", 5, new DateTime(1999, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Harry Potter and the Prisoner of Azkaban" },
                    { 8, "The fourth novel in the Harry Potter series.", 5, new DateTime(2000, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Harry Potter and the Goblet of Fire" },
                    { 9, "The fifth novel in the Harry Potter series.", 5, new DateTime(2003, 6, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Harry Potter and the Order of the Phoenix" },
                    { 10, "The sixth novel in the Harry Potter series.", 5, new DateTime(2005, 7, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Harry Potter and the Half-Blood Prince" },
                    { 11, "The seventh novel in the Harry Potter series.", 5, new DateTime(2007, 7, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Harry Potter and the Deathly Hallows" }
                });

            migrationBuilder.InsertData(
                table: "AuthorBooks",
                columns: new[] { "AuthorId", "BookId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 1, 3 },
                    { 1, 4 },
                    { 1, 5 },
                    { 2, 1 },
                    { 2, 2 },
                    { 2, 3 },
                    { 2, 4 },
                    { 3, 1 },
                    { 3, 2 },
                    { 3, 3 },
                    { 4, 1 },
                    { 4, 2 },
                    { 5, 5 },
                    { 5, 6 },
                    { 5, 7 },
                    { 5, 8 },
                    { 5, 9 },
                    { 5, 10 },
                    { 5, 11 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuthorBooks_BookId",
                table: "AuthorBooks",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Authors_PlaceOfBirthId",
                table: "Authors",
                column: "PlaceOfBirthId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_GenreId",
                table: "Books",
                column: "GenreId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthorBooks");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Genres");
        }
    }
}
