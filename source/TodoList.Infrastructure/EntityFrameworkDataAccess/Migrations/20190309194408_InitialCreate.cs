using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TodoList.Infrastructure.EntityFrameworkDataAccess.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TodoItem",
                columns : table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                        Title = table.Column<string>(nullable: true)
                },
                constraints : table =>
                {
                    table.PrimaryKey("PK_TodoItem", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "TodoItem",
                columns : new [] { "Id", "Title" },
                values : new object[, ]
                { { new Guid("3b35f11e-7080-45e2-a152-afff5a325508"), "Create Repository" }, { new Guid("4b2f8170-c618-4cd6-91b9-25e3b2bfa53e"), "Create solution" }, { new Guid("360644f3-abb5-410b-939d-78a6d07bd075"), "Add projects" }, { new Guid("f1f0adf8-255f-45ef-9528-d6c2c326240b"), "Commit code" }, { new Guid("72af359b-48d7-41cd-978b-38c82e1206d4"), "Push" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TodoItem");
        }
    }
}