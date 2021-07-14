using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace JM.Integration.Methanol.DB.Migrations
{
    public partial class ConverterKPI : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "PlantKPI_Convertor_FK",
                table: "PlantKPI");

            migrationBuilder.DropTable(
                name: "ConvertorKPI");

            migrationBuilder.DropTable(
                name: "Convertor");

            migrationBuilder.DropIndex(
                name: "IX_PlantKPI_Convertor_Convertor_SID",
                table: "PlantKPI");

            migrationBuilder.DropColumn(
                name: "Convertor_Convertor_SID",
                table: "PlantKPI");

            migrationBuilder.DropColumn(
                name: "Unit",
                table: "DataTemplate");

            migrationBuilder.DropColumn(
                name: "Value_RU",
                table: "DataMetrics");

            migrationBuilder.AddColumn<string>(
                name: "Converter_Converter_SID",
                table: "PlantKPI",
                unicode: false,
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Provisional_unit",
                table: "DataTemplate",
                unicode: false,
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Value_PU",
                table: "DataMetrics",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Converter",
                columns: table => new
                {
                    Converter_SID = table.Column<string>(unicode: false, maxLength: 10, nullable: false),
                    Section_Section_SID = table.Column<string>(unicode: false, maxLength: 10, nullable: false),
                    Converter_Name = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Date_of_establish = table.Column<DateTime>(type: "date", nullable: true),
                    Reactor_Trgt_max_temp = table.Column<double>(nullable: true),
                    Reactor_Trgt_max_temp_unit = table.Column<string>(unicode: false, maxLength: 30, nullable: true),
                    Reactor_Safe_max_temp = table.Column<double>(nullable: true),
                    Reactor_Safe_max_temp_unit = table.Column<string>(unicode: false, maxLength: 30, nullable: true),
                    Reactor_Ref_flow_rate = table.Column<double>(nullable: true),
                    Reactor_Ref_flow_rate_unit = table.Column<string>(unicode: false, maxLength: 30, nullable: true),
                    Reactor_duty = table.Column<string>(unicode: false, maxLength: 30, nullable: true),
                    Reactor_type = table.Column<string>(unicode: false, maxLength: 30, nullable: true),
                    Catalyst_name = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Catalyst_supplier = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Catalyst_volume = table.Column<double>(nullable: true),
                    Catalyst_volume_unit = table.Column<string>(unicode: false, maxLength: 30, nullable: true),
                    Catalyst_startdate = table.Column<DateTime>(name: "Catalyst_start date", type: "date", nullable: true),
                    Catalyst_exp_chg_date = table.Column<DateTime>(type: "date", nullable: true),
                    Reactor_AET_type = table.Column<double>(nullable: true),
                    Reactor_AET_type_unit = table.Column<string>(unicode: false, maxLength: 30, nullable: true),
                    Reactor_safe_AET_Temp = table.Column<double>(nullable: true),
                    Reactor_safe_AET_Temp_unit = table.Column<string>(unicode: false, maxLength: 30, nullable: true),
                    Converter_Status = table.Column<string>(unicode: false, maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Converter_PK", x => x.Converter_SID);
                    table.ForeignKey(
                        name: "Converter_Section_FK",
                        column: x => x.Section_Section_SID,
                        principalTable: "Section",
                        principalColumn: "Section_SID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ConverterKPI",
                columns: table => new
                {
                    SID = table.Column<int>(nullable: false),
                    Converter_Converter_SID = table.Column<string>(unicode: false, maxLength: 10, nullable: false),
                    KPI_Name = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    VARIABLE_NAME = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    ANALYSIS = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    CONV_PEAK_TEMP = table.Column<double>(nullable: true),
                    CONV_PEAK_TEMP_UNIT = table.Column<string>(unicode: false, maxLength: 30, nullable: true),
                    CONV_PEAK_TEMP_TRGT_MAX = table.Column<double>(nullable: true),
                    CONV_PEAK_TEMP_TRGT_MAX_INCR = table.Column<double>(nullable: true),
                    CONV_PEAK_TEMP_TRGT_MAX_DECR = table.Column<double>(nullable: true),
                    CONV_PEAK_TEMP_SFTY_MAX = table.Column<double>(nullable: true),
                    CONV_PEAK_TEMP_SFTY_MAX_INCR = table.Column<double>(nullable: true),
                    CONV_PEAK_TEMP_SFTY_MAX_DECR = table.Column<double>(nullable: true),
                    CONV_PRESS_DROP_MSD = table.Column<double>(nullable: true),
                    CONV_PRESS_DROP_MSD_UNIT = table.Column<string>(unicode: false, maxLength: 30, nullable: true),
                    CONV_PRESS_DROP_LF_AVG_NRM = table.Column<double>(nullable: true),
                    CONV_PRESS_DROP_FW_AVG_NRM = table.Column<double>(nullable: true),
                    CONV_PRESS_DROP_AVG_NRM_UNIT = table.Column<string>(unicode: false, maxLength: 30, nullable: true),
                    CONV_PRESS_DRP_FW_AVG_NRM_INCR = table.Column<double>(nullable: true),
                    CONV_PRESS_DRP_FW_AVG_NRM_DECR = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Converter_KPI_PK", x => x.SID);
                    table.ForeignKey(
                        name: "Converter_KPI_Convertor_FK",
                        column: x => x.Converter_Converter_SID,
                        principalTable: "Converter",
                        principalColumn: "Converter_SID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlantKPI_Converter_Converter_SID",
                table: "PlantKPI",
                column: "Converter_Converter_SID");

            migrationBuilder.CreateIndex(
                name: "IX_Converter_Section_Section_SID",
                table: "Converter",
                column: "Section_Section_SID");

            migrationBuilder.CreateIndex(
                name: "IX_ConverterKPI_Converter_Converter_SID",
                table: "ConverterKPI",
                column: "Converter_Converter_SID");

            migrationBuilder.AddForeignKey(
                name: "PlantKPI_Convertor_FK",
                table: "PlantKPI",
                column: "Converter_Converter_SID",
                principalTable: "Converter",
                principalColumn: "Converter_SID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "PlantKPI_Convertor_FK",
                table: "PlantKPI");

            migrationBuilder.DropTable(
                name: "ConverterKPI");

            migrationBuilder.DropTable(
                name: "Converter");

            migrationBuilder.DropIndex(
                name: "IX_PlantKPI_Converter_Converter_SID",
                table: "PlantKPI");

            migrationBuilder.DropColumn(
                name: "Converter_Converter_SID",
                table: "PlantKPI");

            migrationBuilder.DropColumn(
                name: "Provisional_unit",
                table: "DataTemplate");

            migrationBuilder.DropColumn(
                name: "Value_PU",
                table: "DataMetrics");

            migrationBuilder.AddColumn<string>(
                name: "Convertor_Convertor_SID",
                table: "PlantKPI",
                type: "varchar(10)",
                unicode: false,
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Unit",
                table: "DataTemplate",
                type: "varchar(30)",
                unicode: false,
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Value_RU",
                table: "DataMetrics",
                type: "float",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Convertor",
                columns: table => new
                {
                    Convertor_SID = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    Catalyst_exp_chg_date = table.Column<DateTime>(type: "date", nullable: true),
                    Catalyst_name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Catalyst_startdate = table.Column<DateTime>(name: "Catalyst_start date", type: "date", nullable: true),
                    Catalyst_supplier = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Catalyst_volume = table.Column<double>(type: "float", nullable: true),
                    Catalyst_volume_unit = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    Converter_Name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Convertor_Status = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    Date_of_establish = table.Column<DateTime>(type: "date", nullable: true),
                    Reactor_AET_type = table.Column<double>(type: "float", nullable: true),
                    Reactor_AET_type_unit = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    Reactor_duty = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    Reactor_Ref_flow_rate = table.Column<double>(type: "float", nullable: true),
                    Reactor_Ref_flow_rate_unit = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    Reactor_safe_AET_Temp = table.Column<double>(type: "float", nullable: true),
                    Reactor_safe_AET_Temp_unit = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    Reactor_Safe_max_temp = table.Column<double>(type: "float", nullable: true),
                    Reactor_Safe_max_temp_unit = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    Reactor_Trgt_max_temp = table.Column<double>(type: "float", nullable: true),
                    Reactor_Trgt_max_temp_unit = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    Reactor_type = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    Section_Section_SID = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Convertor_PK", x => x.Convertor_SID);
                    table.ForeignKey(
                        name: "Convertor_Section_FK",
                        column: x => x.Section_Section_SID,
                        principalTable: "Section",
                        principalColumn: "Section_SID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ConvertorKPI",
                columns: table => new
                {
                    SID = table.Column<int>(type: "int", nullable: false),
                    CONV_PEAK_TEMP = table.Column<double>(type: "float", nullable: true),
                    CONV_PEAK_TEMP_ANALYSIS = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    CONV_PEAK_TEMP_SFTY_MAX = table.Column<double>(type: "float", nullable: true),
                    CONV_PEAK_TEMP_SFTY_MAX_DECR = table.Column<double>(type: "float", nullable: true),
                    CONV_PEAK_TEMP_SFTY_MAX_INCR = table.Column<double>(type: "float", nullable: true),
                    CONV_PEAK_TEMP_TRGT_MAX = table.Column<double>(type: "float", nullable: true),
                    CONV_PEAK_TEMP_TRGT_MAX_DECR = table.Column<double>(type: "float", nullable: true),
                    CONV_PEAK_TEMP_TRGT_MAX_INCR = table.Column<double>(type: "float", nullable: true),
                    CONV_PEAK_TEMP_UNIT = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    Convertor_Convertor_SID = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    KPI_Name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    VARIABLE_NAME = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Converter_KPI_PK", x => x.SID);
                    table.ForeignKey(
                        name: "Converter_KPI_Convertor_FK",
                        column: x => x.Convertor_Convertor_SID,
                        principalTable: "Convertor",
                        principalColumn: "Convertor_SID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlantKPI_Convertor_Convertor_SID",
                table: "PlantKPI",
                column: "Convertor_Convertor_SID");

            migrationBuilder.CreateIndex(
                name: "IX_Convertor_Section_Section_SID",
                table: "Convertor",
                column: "Section_Section_SID");

            migrationBuilder.CreateIndex(
                name: "IX_ConvertorKPI_Convertor_Convertor_SID",
                table: "ConvertorKPI",
                column: "Convertor_Convertor_SID");

            migrationBuilder.AddForeignKey(
                name: "PlantKPI_Convertor_FK",
                table: "PlantKPI",
                column: "Convertor_Convertor_SID",
                principalTable: "Convertor",
                principalColumn: "Convertor_SID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}