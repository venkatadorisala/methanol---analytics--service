using Microsoft.EntityFrameworkCore.Migrations;

namespace JM.Integration.Methanol.DB.Migrations
{
    public partial class Sprint2TablesModifications : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "MasterTemplate_Customer_FK",
                table: "MasterTEmplate");

            migrationBuilder.DropForeignKey(
                name: "Master_Template_Data_Template_FK",
                table: "MasterTEmplate");

            migrationBuilder.DropTable(
                name: "PlantProvisonalUnits");

            migrationBuilder.DropIndex(
                name: "Master_Template__IDX",
                table: "MasterTEmplate");

            migrationBuilder.DropColumn(
                name: "Plant_SID",
                table: "MasterTEmplate");

            migrationBuilder.AlterColumn<string>(
                name: "Converter_Converter_SID",
                table: "PlantKPI",
                unicode: false,
                maxLength: 10,
                nullable: false,
                defaultValueSql: "('')",
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldUnicode: false,
                oldMaxLength: 10);

            migrationBuilder.AddColumn<string>(
                name: "Plant_Plant_SID",
                table: "MasterTEmplate",
                unicode: false,
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "PlantProvisionalUnits",
                columns: table => new
                {
                    SID = table.Column<int>(nullable: false),
                    Plant_Plant_SID = table.Column<string>(unicode: false, maxLength: 10, nullable: false),
                    UOM_UNIT_SID = table.Column<int>(nullable: false),
                    Provisional_Unit_Name = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Provisional_Unit_symbol = table.Column<string>(unicode: false, maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Plant_Provisonal_Units_PK", x => x.SID);
                    table.ForeignKey(
                        name: "Plant_Provisonal_Units_Plant_FK",
                        column: x => x.Plant_Plant_SID,
                        principalTable: "Plant",
                        principalColumn: "Plant_SID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "Plant_Provisonal_Units_Units_of_Measurement_FK",
                        column: x => x.UOM_UNIT_SID,
                        principalTable: "UnitsofMeasurement",
                        principalColumn: "UNIT_SID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MasterTEmplate_Plant_Plant_SID",
                table: "MasterTEmplate",
                column: "Plant_Plant_SID");

            migrationBuilder.CreateIndex(
                name: "IX_MasterTEmplate_Site_Site_Id",
                table: "MasterTEmplate",
                column: "Site_Site_Id");

            migrationBuilder.CreateIndex(
                name: "IX_PlantProvisionalUnits_Plant_Plant_SID",
                table: "PlantProvisionalUnits",
                column: "Plant_Plant_SID");

            migrationBuilder.CreateIndex(
                name: "IX_PlantProvisionalUnits_UOM_UNIT_SID",
                table: "PlantProvisionalUnits",
                column: "UOM_UNIT_SID");

            migrationBuilder.AddForeignKey(
                name: "Master_Template_Customer_FK",
                table: "MasterTEmplate",
                column: "Customer_SID",
                principalTable: "Customer",
                principalColumn: "SID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "MasterTemplate_Plant_FK",
                table: "MasterTEmplate",
                column: "Plant_Plant_SID",
                principalTable: "Plant",
                principalColumn: "Plant_SID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "Master_Template_Site_FK",
                table: "MasterTEmplate",
                column: "Site_Site_Id",
                principalTable: "Site",
                principalColumn: "Site_Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "Master_Template_Customer_FK",
                table: "MasterTEmplate");

            migrationBuilder.DropForeignKey(
                name: "MasterTemplate_Plant_FK",
                table: "MasterTEmplate");

            migrationBuilder.DropForeignKey(
                name: "Master_Template_Site_FK",
                table: "MasterTEmplate");

            migrationBuilder.DropTable(
                name: "PlantProvisionalUnits");

            migrationBuilder.DropIndex(
                name: "IX_MasterTEmplate_Plant_Plant_SID",
                table: "MasterTEmplate");

            migrationBuilder.DropIndex(
                name: "IX_MasterTEmplate_Site_Site_Id",
                table: "MasterTEmplate");

            migrationBuilder.DropColumn(
                name: "Plant_Plant_SID",
                table: "MasterTEmplate");

            migrationBuilder.AlterColumn<string>(
                name: "Converter_Converter_SID",
                table: "PlantKPI",
                type: "varchar(10)",
                unicode: false,
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldUnicode: false,
                oldMaxLength: 10,
                oldDefaultValueSql: "('')");

            migrationBuilder.AddColumn<string>(
                name: "Plant_SID",
                table: "MasterTEmplate",
                type: "varchar(10)",
                unicode: false,
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "PlantProvisonalUnits",
                columns: table => new
                {
                    Plant_Plant_SID = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    Units_of_Measurement_UNIT_SID = table.Column<int>(type: "int", nullable: false),
                    Provisional_Unit = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Plant_Provisonal_Units_PK", x => new { x.Plant_Plant_SID, x.Units_of_Measurement_UNIT_SID });
                    table.ForeignKey(
                        name: "Plant_Provisonal_Units_Plant_FK",
                        column: x => x.Plant_Plant_SID,
                        principalTable: "Plant",
                        principalColumn: "Plant_SID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "Plant_Provisonal_Units_Units_of_Measurement_FK",
                        column: x => x.Units_of_Measurement_UNIT_SID,
                        principalTable: "UnitsofMeasurement",
                        principalColumn: "UNIT_SID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "Master_Template__IDX",
                table: "MasterTEmplate",
                column: "Plant_SID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlantProvisonalUnits_Units_of_Measurement_UNIT_SID",
                table: "PlantProvisonalUnits",
                column: "Units_of_Measurement_UNIT_SID");

            migrationBuilder.AddForeignKey(
                name: "MasterTemplate_Customer_FK",
                table: "MasterTEmplate",
                column: "Customer_SID",
                principalTable: "Customer",
                principalColumn: "SID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "Master_Template_Data_Template_FK",
                table: "MasterTEmplate",
                column: "Plant_SID",
                principalTable: "Plant",
                principalColumn: "Plant_SID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
