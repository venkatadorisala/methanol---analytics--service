using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace JM.Integration.Methanol.DB.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    SID = table.Column<int>(nullable: false),
                    Customer_Name = table.Column<string>(unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Customer_PK", x => x.SID);
                });

            migrationBuilder.CreateTable(
                name: "ModelDefinition",
                columns: table => new
                {
                    Model_SID = table.Column<int>(nullable: false),
                    Model_Name = table.Column<string>(unicode: false, maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Bestfit_Model_Definition_PK", x => x.Model_SID);
                });

            migrationBuilder.CreateTable(
                name: "OptimisedMetric",
                columns: table => new
                {
                    SID = table.Column<int>(nullable: false),
                    Current_Value = table.Column<double>(nullable: true),
                    Measured_Value_SU = table.Column<double>(nullable: true),
                    Standard_Unit = table.Column<string>(unicode: false, maxLength: 10, nullable: true),
                    Fitted_Value_SU = table.Column<double>(nullable: true),
                    Provisional_Unit = table.Column<string>(unicode: false, maxLength: 10, nullable: true),
                    Fitted_Value_PU = table.Column<double>(nullable: true),
                    Step = table.Column<double>(nullable: true),
                    Weight = table.Column<double>(nullable: true),
                    SOS = table.Column<double>(nullable: true),
                    Standard_Error = table.Column<double>(nullable: true),
                    Change = table.Column<double>(nullable: true),
                    Change_ = table.Column<double>(name: "Change_%", nullable: true),
                    SOS_ = table.Column<double>(name: "SOS_%", nullable: true),
                    Fitting_Date = table.Column<DateTime>(type: "date", nullable: true),
                    Reading_Date = table.Column<DateTime>(type: "date", nullable: true),
                    Upload_Date = table.Column<DateTime>(type: "date", nullable: true),
                    Valid_From = table.Column<DateTime>(type: "datetime", nullable: true),
                    Valid_To = table.Column<DateTime>(type: "datetime", nullable: true),
                    Model_SID = table.Column<int>(nullable: false),
                    Model_Variable_SID = table.Column<int>(nullable: false),
                    Plant_SID = table.Column<int>(nullable: false),
                    Optimised_Value = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Optimised_Metric_PK", x => x.SID);
                });

            migrationBuilder.CreateTable(
                name: "Plant",
                columns: table => new
                {
                    Plant_SID = table.Column<string>(unicode: false, maxLength: 10, nullable: false),
                    Site_Site_Id = table.Column<string>(unicode: false, maxLength: 10, nullable: false),
                    Plant_Name = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Date_of_establish = table.Column<DateTime>(type: "datetime", nullable: true),
                    Plant_Capacity = table.Column<double>(nullable: true),
                    Plant_capacity_unit = table.Column<string>(unicode: false, maxLength: 30, nullable: true),
                    Plant_Status = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    Max_Prod_flow = table.Column<double>(nullable: true),
                    Max_Prod_flow_unit = table.Column<string>(unicode: false, maxLength: 30, nullable: true),
                    Max_Syngas_flow = table.Column<double>(nullable: true),
                    Max_Syng_flow_unit = table.Column<string>(unicode: false, maxLength: 30, nullable: true),
                    R_Ratio_Target = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Plant_PK", x => x.Plant_SID);
                });

            migrationBuilder.CreateTable(
                name: "Site",
                columns: table => new
                {
                    Site_Id = table.Column<string>(unicode: false, maxLength: 10, nullable: false),
                    Site_Name = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Date_of_establish = table.Column<DateTime>(type: "date", nullable: true),
                    Company_Name = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Site_location_state = table.Column<string>(unicode: false, maxLength: 30, nullable: true),
                    Site_location_country = table.Column<string>(unicode: false, maxLength: 30, nullable: true),
                    Site_Subsr_tier = table.Column<string>(unicode: false, maxLength: 30, nullable: true),
                    Customer_SID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Site", x => x.Site_Id);
                });

            migrationBuilder.CreateTable(
                name: "Test1",
                columns: table => new
                {
                    PersonID = table.Column<int>(nullable: true),
                    LastName = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    FirstName = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    Address = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    City = table.Column<string>(unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Test2",
                columns: table => new
                {
                    PlantSID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Test2", x => x.PlantSID);
                });

            migrationBuilder.CreateTable(
                name: "UnitsofMeasurement",
                columns: table => new
                {
                    UNIT_SID = table.Column<int>(nullable: false),
                    Unit_Family = table.Column<string>(unicode: false, maxLength: 30, nullable: true),
                    Unit_Name = table.Column<string>(unicode: false, maxLength: 30, nullable: true),
                    Unit_Symbol = table.Column<string>(unicode: false, maxLength: 15, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Units_of_Measurement_PK", x => x.UNIT_SID);
                });

            migrationBuilder.CreateTable(
                name: "ModelVariables",
                columns: table => new
                {
                    SID = table.Column<int>(nullable: false),
                    MDM_SID = table.Column<int>(nullable: false),
                    Variable_Name = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    Unit = table.Column<string>(unicode: false, maxLength: 30, nullable: true),
                    Current_Value = table.Column<double>(nullable: true),
                    Read_Write = table.Column<string>(unicode: false, maxLength: 5, nullable: true),
                    Model_Name = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Section = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Description = table.Column<string>(unicode: false, maxLength: 200, nullable: true),
                    Step = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Weight = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Create_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    Create_by = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Update_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    Updated_by = table.Column<string>(unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Model_Variables_PK", x => x.SID);
                    table.ForeignKey(
                        name: "Model_Variables_Model_Definition_FK",
                        column: x => x.MDM_SID,
                        principalTable: "ModelDefinition",
                        principalColumn: "Model_SID",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Contain BestFit model definition as the config data to enable automatic orchestration of the fitting process ");

            migrationBuilder.CreateTable(
                name: "MasterTEmplate",
                columns: table => new
                {
                    SID = table.Column<int>(nullable: false),
                    Customer_SID = table.Column<int>(nullable: false),
                    Plant_SID = table.Column<string>(unicode: false, maxLength: 10, nullable: false),
                    Site_Site_Id = table.Column<string>(unicode: false, maxLength: 10, nullable: false),
                    Plant_Template_Code = table.Column<string>(unicode: false, fixedLength: true, maxLength: 5, nullable: false),
                    Section_Template_Code = table.Column<string>(unicode: false, fixedLength: true, maxLength: 5, nullable: false),
                    Converter_1_Template_code = table.Column<string>(unicode: false, fixedLength: true, maxLength: 5, nullable: true),
                    Converter_2_Template_code = table.Column<string>(unicode: false, fixedLength: true, maxLength: 5, nullable: true),
                    Converter_3_Template_code = table.Column<string>(unicode: false, fixedLength: true, maxLength: 5, nullable: true),
                    Converter_4_Template_code = table.Column<string>(unicode: false, fixedLength: true, maxLength: 5, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Master_Template_PK", x => x.SID);
                    table.ForeignKey(
                        name: "MasterTemplate_Customer_FK",
                        column: x => x.Customer_SID,
                        principalTable: "Customer",
                        principalColumn: "SID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "Master_Template_Data_Template_FK",
                        column: x => x.Plant_SID,
                        principalTable: "Plant",
                        principalColumn: "Plant_SID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProcessDetails",
                columns: table => new
                {
                    SID = table.Column<int>(nullable: false),
                    Plant_Plant_SID = table.Column<string>(unicode: false, maxLength: 10, nullable: false),
                    Site_Id = table.Column<string>(unicode: false, maxLength: 10, nullable: true),
                    Upload_File_Name = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Upload_Date = table.Column<DateTime>(type: "datetime", nullable: true),
                    Upload_File_Status = table.Column<string>(unicode: false, maxLength: 30, nullable: true),
                    Reading_Date = table.Column<DateTime>(type: "date", nullable: true),
                    AV_Scan_Status = table.Column<string>(unicode: false, maxLength: 30, nullable: true),
                    Pre_Chk_Status = table.Column<string>(unicode: false, maxLength: 30, nullable: true),
                    History_Rev_Flag = table.Column<string>(unicode: false, maxLength: 5, nullable: true),
                    Process_Date = table.Column<DateTime>(type: "datetime", nullable: true),
                    Process_Status = table.Column<string>(unicode: false, maxLength: 30, nullable: true),
                    Summary = table.Column<string>(unicode: false, maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Process_Details_PK", x => x.SID);
                    table.ForeignKey(
                        name: "Process_Details_Plant_FK",
                        column: x => x.Plant_Plant_SID,
                        principalTable: "Plant",
                        principalColumn: "Plant_SID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Section",
                columns: table => new
                {
                    Section_SID = table.Column<string>(unicode: false, maxLength: 10, nullable: false),
                    Plant_Plant_SID = table.Column<string>(unicode: false, maxLength: 10, nullable: false),
                    Section_Name = table.Column<string>(unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Section_PK", x => x.Section_SID);
                    table.ForeignKey(
                        name: "Section_Plant_FK",
                        column: x => x.Plant_Plant_SID,
                        principalTable: "Plant",
                        principalColumn: "Plant_SID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PlantProvisonalUnits",
                columns: table => new
                {
                    Plant_Plant_SID = table.Column<string>(unicode: false, maxLength: 10, nullable: false),
                    Units_of_Measurement_UNIT_SID = table.Column<int>(nullable: false),
                    Provisional_Unit = table.Column<string>(unicode: false, maxLength: 30, nullable: true)
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

            migrationBuilder.CreateTable(
                name: "FittingMetrics",
                columns: table => new
                {
                    SID = table.Column<int>(nullable: false),
                    Current_Value = table.Column<double>(nullable: true),
                    Measured_Value_SU = table.Column<double>(nullable: true),
                    Standard_Unit = table.Column<string>(unicode: false, maxLength: 30, nullable: true),
                    Fitted_Value_SU = table.Column<double>(nullable: true),
                    Provisional_Unit = table.Column<string>(unicode: false, maxLength: 30, nullable: true),
                    Fitted_Value_PU = table.Column<double>(nullable: true),
                    Step = table.Column<double>(nullable: true),
                    Weight = table.Column<double>(nullable: true),
                    SOS = table.Column<double>(nullable: true),
                    Standard_Error = table.Column<double>(nullable: true),
                    Change = table.Column<double>(nullable: true),
                    Change_PCT = table.Column<double>(nullable: true),
                    SOS_PCT = table.Column<double>(nullable: true),
                    Fitting_Date = table.Column<DateTime>(type: "date", nullable: true),
                    Reading_Date = table.Column<DateTime>(type: "date", nullable: true),
                    Upload_Date = table.Column<DateTime>(type: "date", nullable: true),
                    Valid_From = table.Column<DateTime>(type: "datetime", nullable: true),
                    Valid_To = table.Column<DateTime>(type: "datetime", nullable: true),
                    MV_SID = table.Column<int>(nullable: false),
                    Plant_SID = table.Column<string>(unicode: false, maxLength: 10, nullable: false),
                    Optimised_Value = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Fitting_Metrics_PK", x => x.SID);
                    table.ForeignKey(
                        name: "Fitting_Metrics_Model_Variables_FK",
                        column: x => x.MV_SID,
                        principalTable: "ModelVariables",
                        principalColumn: "SID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "Fitting_Metrics_Plant_FK",
                        column: x => x.Plant_SID,
                        principalTable: "Plant",
                        principalColumn: "Plant_SID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DataMetrics",
                columns: table => new
                {
                    SID = table.Column<int>(nullable: false),
                    Plant_SID = table.Column<string>(unicode: false, maxLength: 10, nullable: false),
                    Master_Template_SID = table.Column<int>(nullable: false),
                    Variable = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Value_RU = table.Column<double>(nullable: true),
                    Recorded_Unit = table.Column<string>(unicode: false, maxLength: 30, nullable: true),
                    Value_SU = table.Column<double>(nullable: true),
                    Standard_Unit = table.Column<string>(unicode: false, maxLength: 30, nullable: true),
                    DOU = table.Column<DateTime>(type: "datetime", nullable: true),
                    Valid_From = table.Column<DateTime>(type: "datetime", nullable: true),
                    Valid_To = table.Column<DateTime>(type: "datetime", nullable: true),
                    DOR = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Data_Metrics_PK", x => new { x.SID, x.Plant_SID });
                    table.ForeignKey(
                        name: "Data_Metrics_Master_Template_FK",
                        column: x => x.Master_Template_SID,
                        principalTable: "MasterTEmplate",
                        principalColumn: "SID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "Data_Metrics_Plant_FK",
                        column: x => x.Plant_SID,
                        principalTable: "Plant",
                        principalColumn: "Plant_SID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DataTemplate",
                columns: table => new
                {
                    SID = table.Column<int>(nullable: false),
                    Template_Code = table.Column<string>(unicode: false, maxLength: 5, nullable: false),
                    Template_Type = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    Template_Version = table.Column<string>(unicode: false, maxLength: 5, nullable: true),
                    Section = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    Sub_Section = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Variable = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    Unit = table.Column<string>(unicode: false, maxLength: 30, nullable: true),
                    Column_no = table.Column<int>(nullable: false),
                    UOM_SID = table.Column<int>(nullable: false),
                    Range_From = table.Column<double>(nullable: true),
                    Range_To = table.Column<double>(nullable: true),
                    Mandatory = table.Column<bool>(nullable: true),
                    Create_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    Update_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    Created_By = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Updated_by = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Master_Template_SID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Data_Metrics_Templates_PK", x => x.SID);
                    table.ForeignKey(
                        name: "Data_Template_Master_Template_FK",
                        column: x => x.Master_Template_SID,
                        principalTable: "MasterTEmplate",
                        principalColumn: "SID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "Data_Template_Units_of_Measurement_FK",
                        column: x => x.UOM_SID,
                        principalTable: "UnitsofMeasurement",
                        principalColumn: "UNIT_SID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ErrorDetails",
                columns: table => new
                {
                    SID = table.Column<int>(nullable: false),
                    Process_Details_SID = table.Column<int>(nullable: false),
                    File_Name = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Rec_Number = table.Column<int>(nullable: true),
                    DOReading = table.Column<DateTime>(type: "datetime", nullable: true),
                    DOUpdate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Error_Desc = table.Column<string>(unicode: false, maxLength: 4000, nullable: true),
                    Error_Rec_Status = table.Column<string>(unicode: false, maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Error_Details_PK", x => x.SID);
                    table.ForeignKey(
                        name: "Error_Details_Process_Details_FK",
                        column: x => x.Process_Details_SID,
                        principalTable: "ProcessDetails",
                        principalColumn: "SID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Convertor",
                columns: table => new
                {
                    Convertor_SID = table.Column<string>(unicode: false, maxLength: 10, nullable: false),
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
                    Convertor_Status = table.Column<string>(unicode: false, maxLength: 20, nullable: true)
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
                name: "ModelPlantMapping",
                columns: table => new
                {
                    SID = table.Column<int>(nullable: false),
                    Model_Variable_SID = table.Column<int>(nullable: false),
                    Data_Template_SID = table.Column<int>(nullable: true),
                    Mapping_Type = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    Step = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Weight = table.Column<string>(unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Model_Plant_Mapping_PK", x => x.SID);
                    table.ForeignKey(
                        name: "Model_Plant_Mapping_Data_Template_FK",
                        column: x => x.Data_Template_SID,
                        principalTable: "DataTemplate",
                        principalColumn: "SID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "Model_Plant_Mapping_Model_Variables_FK",
                        column: x => x.Model_Variable_SID,
                        principalTable: "ModelVariables",
                        principalColumn: "SID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ConvertorKPI",
                columns: table => new
                {
                    SID = table.Column<int>(nullable: false),
                    Convertor_Convertor_SID = table.Column<string>(unicode: false, maxLength: 10, nullable: false),
                    KPI_Name = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    VARIABLE_NAME = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    CONV_PEAK_TEMP_ANALYSIS = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    CONV_PEAK_TEMP = table.Column<double>(nullable: true),
                    CONV_PEAK_TEMP_UNIT = table.Column<string>(unicode: false, maxLength: 30, nullable: true),
                    CONV_PEAK_TEMP_TRGT_MAX = table.Column<double>(nullable: true),
                    CONV_PEAK_TEMP_TRGT_MAX_INCR = table.Column<double>(nullable: true),
                    CONV_PEAK_TEMP_TRGT_MAX_DECR = table.Column<double>(nullable: true),
                    CONV_PEAK_TEMP_SFTY_MAX = table.Column<double>(nullable: true),
                    CONV_PEAK_TEMP_SFTY_MAX_INCR = table.Column<double>(nullable: true),
                    CONV_PEAK_TEMP_SFTY_MAX_DECR = table.Column<double>(nullable: true)
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

            migrationBuilder.CreateTable(
                name: "PlantKPI",
                columns: table => new
                {
                    SID = table.Column<int>(nullable: false),
                    Convertor_Convertor_SID = table.Column<string>(unicode: false, maxLength: 10, nullable: false),
                    KPI_Name = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    SYNGAS_FLOW_PERCENT = table.Column<double>(nullable: true),
                    SYNGAS_FLOW_PERCENT_UNIT = table.Column<string>(unicode: false, maxLength: 5, nullable: true),
                    SYNGAS_FLOW_ACT_VALUE = table.Column<double>(nullable: true),
                    SYNGAS_FLOW_ACT_VALUE_UNIT = table.Column<string>(unicode: false, maxLength: 30, nullable: true),
                    SYNGAS_FLOW_TOTAL = table.Column<double>(nullable: true),
                    SYNGAS_FLOW_TOTAL_UNIT = table.Column<string>(unicode: false, maxLength: 30, nullable: true),
                    VARIABLE_NAME = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    CONV_PEAK_TEMP_ANALYSIS = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    CONV_PEAK_TEMP = table.Column<double>(nullable: true),
                    CONV_PEAK_TEMP_UNIT = table.Column<string>(unicode: false, maxLength: 30, nullable: true),
                    CONV_PEAK_TEMP_TRGT_MAX = table.Column<double>(nullable: true),
                    CONV_PEAK_TEMP_TRGT_MAX_INCR = table.Column<double>(nullable: true),
                    CONV_PEAK_TEMP_TRGT_MAX_DECR = table.Column<double>(nullable: true),
                    CONV_PEAK_TEMP_SFTY_MAX = table.Column<double>(nullable: true),
                    CONV_PEAK_TEMP_SFTY_MAX_INCR = table.Column<double>(nullable: true),
                    CONV_PEAK_TEMP_SFTY_MAX_DECR = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Plant_KPI_PK", x => x.SID);
                    table.ForeignKey(
                        name: "PlantKPI_Convertor_FK",
                        column: x => x.Convertor_Convertor_SID,
                        principalTable: "Convertor",
                        principalColumn: "Convertor_SID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Convertor_Section_Section_SID",
                table: "Convertor",
                column: "Section_Section_SID");

            migrationBuilder.CreateIndex(
                name: "IX_ConvertorKPI_Convertor_Convertor_SID",
                table: "ConvertorKPI",
                column: "Convertor_Convertor_SID");

            migrationBuilder.CreateIndex(
                name: "IX_DataMetrics_Master_Template_SID",
                table: "DataMetrics",
                column: "Master_Template_SID");

            migrationBuilder.CreateIndex(
                name: "IX_DataMetrics_Plant_SID",
                table: "DataMetrics",
                column: "Plant_SID");

            migrationBuilder.CreateIndex(
                name: "IX_DataTemplate_Master_Template_SID",
                table: "DataTemplate",
                column: "Master_Template_SID");

            migrationBuilder.CreateIndex(
                name: "IX_DataTemplate_UOM_SID",
                table: "DataTemplate",
                column: "UOM_SID");

            migrationBuilder.CreateIndex(
                name: "IX_ErrorDetails_Process_Details_SID",
                table: "ErrorDetails",
                column: "Process_Details_SID");

            migrationBuilder.CreateIndex(
                name: "IX_FittingMetrics_MV_SID",
                table: "FittingMetrics",
                column: "MV_SID");

            migrationBuilder.CreateIndex(
                name: "IX_FittingMetrics_Plant_SID",
                table: "FittingMetrics",
                column: "Plant_SID");

            migrationBuilder.CreateIndex(
                name: "IX_MasterTEmplate_Customer_SID",
                table: "MasterTEmplate",
                column: "Customer_SID");

            migrationBuilder.CreateIndex(
                name: "Master_Template__IDX",
                table: "MasterTEmplate",
                column: "Plant_SID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ModelPlantMapping_Data_Template_SID",
                table: "ModelPlantMapping",
                column: "Data_Template_SID");

            migrationBuilder.CreateIndex(
                name: "IX_ModelPlantMapping_Model_Variable_SID",
                table: "ModelPlantMapping",
                column: "Model_Variable_SID");

            migrationBuilder.CreateIndex(
                name: "IX_ModelVariables_MDM_SID",
                table: "ModelVariables",
                column: "MDM_SID");

            migrationBuilder.CreateIndex(
                name: "IX_PlantKPI_Convertor_Convertor_SID",
                table: "PlantKPI",
                column: "Convertor_Convertor_SID");

            migrationBuilder.CreateIndex(
                name: "IX_PlantProvisonalUnits_Units_of_Measurement_UNIT_SID",
                table: "PlantProvisonalUnits",
                column: "Units_of_Measurement_UNIT_SID");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessDetails_Plant_Plant_SID",
                table: "ProcessDetails",
                column: "Plant_Plant_SID");

            migrationBuilder.CreateIndex(
                name: "IX_Section_Plant_Plant_SID",
                table: "Section",
                column: "Plant_Plant_SID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConvertorKPI");

            migrationBuilder.DropTable(
                name: "DataMetrics");

            migrationBuilder.DropTable(
                name: "ErrorDetails");

            migrationBuilder.DropTable(
                name: "FittingMetrics");

            migrationBuilder.DropTable(
                name: "ModelPlantMapping");

            migrationBuilder.DropTable(
                name: "OptimisedMetric");

            migrationBuilder.DropTable(
                name: "PlantKPI");

            migrationBuilder.DropTable(
                name: "PlantProvisonalUnits");

            migrationBuilder.DropTable(
                name: "Site");

            migrationBuilder.DropTable(
                name: "Test1");

            migrationBuilder.DropTable(
                name: "Test2");

            migrationBuilder.DropTable(
                name: "ProcessDetails");

            migrationBuilder.DropTable(
                name: "DataTemplate");

            migrationBuilder.DropTable(
                name: "ModelVariables");

            migrationBuilder.DropTable(
                name: "Convertor");

            migrationBuilder.DropTable(
                name: "MasterTEmplate");

            migrationBuilder.DropTable(
                name: "UnitsofMeasurement");

            migrationBuilder.DropTable(
                name: "ModelDefinition");

            migrationBuilder.DropTable(
                name: "Section");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "Plant");
        }
    }
}