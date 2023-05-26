using Microsoft.EntityFrameworkCore.Migrations;

namespace LMS_WEB_API_NetCore.Migrations
{
    public partial class finaldb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "EmployeeId",
                table: "ApplyLeave",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AppliedOn",
                table: "ApplyLeave",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ManagerComments",
                table: "ApplyLeave",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "ApplyLeave",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppliedOn",
                table: "ApplyLeave");

            migrationBuilder.DropColumn(
                name: "ManagerComments",
                table: "ApplyLeave");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "ApplyLeave");

            migrationBuilder.AlterColumn<string>(
                name: "EmployeeId",
                table: "ApplyLeave",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
