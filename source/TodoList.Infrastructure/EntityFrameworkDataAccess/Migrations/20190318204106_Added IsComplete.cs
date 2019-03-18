using Microsoft.EntityFrameworkCore.Migrations;

namespace TodoList.Infrastructure.EntityFrameworkDataAccess.Migrations
{
    public partial class AddedIsComplete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCompleted",
                table: "TodoItem",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCompleted",
                table: "TodoItem");
        }
    }
}
