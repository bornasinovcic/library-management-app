using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibraryManagementApp.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddedIds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Genres_genreId",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "genreId",
                table: "Books",
                newName: "GenreId");

            migrationBuilder.RenameIndex(
                name: "IX_Books_genreId",
                table: "Books",
                newName: "IX_Books_GenreId");

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "DateOfBirth", "FirstName", "LastName", "PlaceOfBirthId" },
                values: new object[,]
                {
                    { 1, new DateTime(1775, 12, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jane", "Austen", 1 },
                    { 2, new DateTime(1903, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "George", "Orwell", 2 },
                    { 3, new DateTime(1890, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Agatha", "Christie", 3 },
                    { 4, new DateTime(1947, 9, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Stephen", "King", 4 },
                    { 5, new DateTime(1965, 7, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "J.K.", "Rowling", 5 }
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
                    { 5, "A fantasy novel.", 5, new DateTime(1997, 6, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Harry Potter and the Philosopher's Stone" }
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
                    { 5, 1 }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Genres_GenreId",
                table: "Books",
                column: "GenreId",
                principalTable: "Genres",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Genres_GenreId",
                table: "Books");

            migrationBuilder.DeleteData(
                table: "AuthorBooks",
                keyColumns: new[] { "AuthorId", "BookId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "AuthorBooks",
                keyColumns: new[] { "AuthorId", "BookId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "AuthorBooks",
                keyColumns: new[] { "AuthorId", "BookId" },
                keyValues: new object[] { 1, 3 });

            migrationBuilder.DeleteData(
                table: "AuthorBooks",
                keyColumns: new[] { "AuthorId", "BookId" },
                keyValues: new object[] { 1, 4 });

            migrationBuilder.DeleteData(
                table: "AuthorBooks",
                keyColumns: new[] { "AuthorId", "BookId" },
                keyValues: new object[] { 1, 5 });

            migrationBuilder.DeleteData(
                table: "AuthorBooks",
                keyColumns: new[] { "AuthorId", "BookId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "AuthorBooks",
                keyColumns: new[] { "AuthorId", "BookId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "AuthorBooks",
                keyColumns: new[] { "AuthorId", "BookId" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DeleteData(
                table: "AuthorBooks",
                keyColumns: new[] { "AuthorId", "BookId" },
                keyValues: new object[] { 2, 4 });

            migrationBuilder.DeleteData(
                table: "AuthorBooks",
                keyColumns: new[] { "AuthorId", "BookId" },
                keyValues: new object[] { 3, 1 });

            migrationBuilder.DeleteData(
                table: "AuthorBooks",
                keyColumns: new[] { "AuthorId", "BookId" },
                keyValues: new object[] { 3, 2 });

            migrationBuilder.DeleteData(
                table: "AuthorBooks",
                keyColumns: new[] { "AuthorId", "BookId" },
                keyValues: new object[] { 3, 3 });

            migrationBuilder.DeleteData(
                table: "AuthorBooks",
                keyColumns: new[] { "AuthorId", "BookId" },
                keyValues: new object[] { 4, 1 });

            migrationBuilder.DeleteData(
                table: "AuthorBooks",
                keyColumns: new[] { "AuthorId", "BookId" },
                keyValues: new object[] { 4, 2 });

            migrationBuilder.DeleteData(
                table: "AuthorBooks",
                keyColumns: new[] { "AuthorId", "BookId" },
                keyValues: new object[] { 5, 1 });

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.RenameColumn(
                name: "GenreId",
                table: "Books",
                newName: "genreId");

            migrationBuilder.RenameIndex(
                name: "IX_Books_GenreId",
                table: "Books",
                newName: "IX_Books_genreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Genres_genreId",
                table: "Books",
                column: "genreId",
                principalTable: "Genres",
                principalColumn: "Id");
        }
    }
}
