using Microsoft.EntityFrameworkCore.Migrations;

namespace ERP.Migrations
{
    public partial class UserNameChangeLimit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UsernameChangeLimit",
                table: "UserMaster",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UsernameChangeLimit",
                table: "UserMaster");
        }
    }
}
