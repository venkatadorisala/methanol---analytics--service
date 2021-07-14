using Microsoft.EntityFrameworkCore.Migrations;

namespace JM.Integration.Methanol.DB.Migrations
{
    public partial class addtableSiteKpi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Plant_Molecular_wt",
                table: "Plant",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SectionKPI",
                columns: table => new
                {
                    SID = table.Column<int>(nullable: false),
                    Convertor_Convertor_SID = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    KPI_Name = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    VARIABLE_NAME = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    ANALYSIS = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Methanol_Prod_actual = table.Column<double>(nullable: true),
                    Methanol_Prod_actual_unit = table.Column<string>(unicode: false, maxLength: 30, nullable: true),
                    Methanol_Prod_actual_perc = table.Column<double>(nullable: true),
                    Methanol_Prod_Total_prod = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("SectionKPI_PK", x => x.SID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SectionKPI");

            migrationBuilder.DropColumn(
                name: "Plant_Molecular_wt",
                table: "Plant");
        }
    }
}
