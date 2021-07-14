using Microsoft.EntityFrameworkCore.Migrations;

namespace JM.Integration.Methanol.DB.Migrations
{
    public partial class alterTblProcessDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Pre_Chk_Status",
                table: "ProcessDetails",
                unicode: false,
                maxLength: 120,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldUnicode: false,
                oldMaxLength: 50,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Pre_Chk_Status",
                table: "ProcessDetails",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldUnicode: false,
                oldMaxLength: 120,
                oldNullable: true);
        }
    }
}
