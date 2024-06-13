using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibraryManagementApp.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddedGender : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AuthorBooks",
                keyColumns: new[] { "AuthorId", "BookId" },
                keyValues: new object[] { 5, 1 });

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Authors",
                type: "nvarchar(1)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AuthorBooks",
                columns: new[] { "AuthorId", "BookId" },
                values: new object[] { 5, 5 });

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1,
                column: "Gender",
                value: "F");

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 2,
                column: "Gender",
                value: "M");

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 3,
                column: "Gender",
                value: "F");

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 4,
                column: "Gender",
                value: "M");

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 5,
                column: "Gender",
                value: "F");

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Description", "GenreId", "PublishDate", "Title" },
                values: new object[,]
                {
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
                    { 5, 6 },
                    { 5, 7 },
                    { 5, 8 },
                    { 5, 9 },
                    { 5, 10 },
                    { 5, 11 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AuthorBooks",
                keyColumns: new[] { "AuthorId", "BookId" },
                keyValues: new object[] { 5, 5 });

            migrationBuilder.DeleteData(
                table: "AuthorBooks",
                keyColumns: new[] { "AuthorId", "BookId" },
                keyValues: new object[] { 5, 6 });

            migrationBuilder.DeleteData(
                table: "AuthorBooks",
                keyColumns: new[] { "AuthorId", "BookId" },
                keyValues: new object[] { 5, 7 });

            migrationBuilder.DeleteData(
                table: "AuthorBooks",
                keyColumns: new[] { "AuthorId", "BookId" },
                keyValues: new object[] { 5, 8 });

            migrationBuilder.DeleteData(
                table: "AuthorBooks",
                keyColumns: new[] { "AuthorId", "BookId" },
                keyValues: new object[] { 5, 9 });

            migrationBuilder.DeleteData(
                table: "AuthorBooks",
                keyColumns: new[] { "AuthorId", "BookId" },
                keyValues: new object[] { 5, 10 });

            migrationBuilder.DeleteData(
                table: "AuthorBooks",
                keyColumns: new[] { "AuthorId", "BookId" },
                keyValues: new object[] { 5, 11 });

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Authors");

            migrationBuilder.InsertData(
                table: "AuthorBooks",
                columns: new[] { "AuthorId", "BookId" },
                values: new object[] { 5, 1 });
        }
    }
}
