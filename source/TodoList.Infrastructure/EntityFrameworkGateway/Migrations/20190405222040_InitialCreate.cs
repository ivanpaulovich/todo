using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TodoList.Infrastructure.EntityFrameworkGateway.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Item",
                columns : table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                        Title = table.Column<string>(nullable: true),
                        Done = table.Column<bool>(nullable: false)
                },
                constraints : table =>
                {
                    table.PrimaryKey("PK_Item", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Item",
                columns : new [] { "Id", "Done", "Title" },
                values : new object[, ]
                { { new Guid("3b35f11e-7080-45e2-a152-afff5a325508"), true, "Fork the repository" }, { new Guid("4b2f8170-c618-4cd6-91b9-25e3b2bfa53e"), false, "Clone the repository" }, { new Guid("360644f3-abb5-410b-939d-78a6d07bd075"), false, "Create a branch" }, { new Guid("f1f0adf8-255f-45ef-9528-d6c2c326240b"), false, "Make necessary changes and commit those changes" }, { new Guid("72af359b-48d7-41cd-978b-38c82e1206d4"), false, "Push changes to GitHub" }, { new Guid("e3da5d74-3fa4-4856-b0dd-d098e0f637ed"), false, "Submit your changes for review" }, { new Guid("1798c99c-371e-497c-a529-a1e3667b9ece"), false, "Keeping your fork synced with this repository" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Item");
        }
    }
}