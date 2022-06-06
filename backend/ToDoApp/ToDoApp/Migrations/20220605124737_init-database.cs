using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoApp.Migrations
{
    public partial class initdatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Todo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsCompleted = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpectedCompletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Todo", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Todo",
                columns: new[] { "Id", "Created", "Description", "ExpectedCompletionTime", "Name", "Updated" },
                values: new object[] { new Guid("6420c9f3-d708-46c6-b278-5b4186ba1054"), new DateTime(2022, 6, 5, 12, 47, 36, 696, DateTimeKind.Utc).AddTicks(683), "None", null, "Learn TypeScript", new DateTime(2022, 6, 5, 12, 47, 36, 696, DateTimeKind.Utc).AddTicks(685) });

            migrationBuilder.InsertData(
                table: "Todo",
                columns: new[] { "Id", "Created", "Description", "ExpectedCompletionTime", "Name", "Updated" },
                values: new object[] { new Guid("beb014b1-ac25-4a85-9544-2a31ed1b53ab"), new DateTime(2022, 6, 5, 12, 47, 36, 696, DateTimeKind.Utc).AddTicks(691), "", null, "Learn GraphQL", new DateTime(2022, 6, 5, 12, 47, 36, 696, DateTimeKind.Utc).AddTicks(692) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Todo");
        }
    }
}
