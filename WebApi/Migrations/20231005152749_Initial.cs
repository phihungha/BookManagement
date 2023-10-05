using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Presses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Category = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Presses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    ISBN = table.Column<string>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Author = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false),
                    Location_City = table.Column<string>(type: "TEXT", nullable: false),
                    Location_Street = table.Column<string>(type: "TEXT", nullable: false),
                    PressId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_Presses_PressId",
                        column: x => x.PressId,
                        principalTable: "Presses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Presses",
                columns: new[] { "Id", "Category", "Name" },
                values: new object[] { 1, 0, "Addison-Wesley" });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Location_City", "Location_Street", "Author", "ISBN", "PressId", "Price", "Title" },
                values: new object[] { 1, "HCM City", "D2, Thu Duc District", "Mark Michaelis", "987-0-321-87758-1", 1, 59.99m, "Essential C#5.0" });

            migrationBuilder.CreateIndex(
                name: "IX_Books_PressId",
                table: "Books",
                column: "PressId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Presses");
        }
    }
}
