using Microsoft.EntityFrameworkCore.Migrations;

namespace JM.Integration.Methanol.DB.Migrations
{
    public partial class altertTblProcessDetailsSummary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Summary",
                table: "ProcessDetails",
                unicode: false,
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(40)",
                oldUnicode: false,
                oldMaxLength: 40,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Report",
                table: "ProcessDetails",
                unicode: false,
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Report",
                table: "ProcessDetails");

            migrationBuilder.AlterColumn<string>(
                name: "Summary",
                table: "ProcessDetails",
                type: "varchar(40)",
                unicode: false,
                maxLength: 40,
                nullable: true,
                oldClrType: typeof(string),
                oldUnicode: false,
                oldMaxLength: 200,
                oldNullable: true);
        }
    }
}
