using Microsoft.EntityFrameworkCore.Migrations;

namespace JM.Integration.Methanol.DB.Migrations
{
    public partial class AlterProcSyngasSplit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string procedure = @"CREATE OR ALTER  PROCEDURE [dbo].[usp_SyngasSplit]
(
    -- Add the parameters for the stored procedure here
    @sectionId varchar(10)
    --<@Param2, sysname, @p2> <Datatype_For_Param2, , int> = <Default_Value_For_Param2, , 0>
)
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT
		c.Converter_SID as Converter_SID,
		c.Converter_Name as Converter_Name,
		p.KPI_Name as KPI_Name,
		p.SYNGAS_FLOW_PERCENT as SYNGAS_FLOW_PERCENT,
		p.SYNGAS_FLOW_PERCENT_UNIT,
		CASE WHEN p.SYNGAS_FLOW_PERCENT_UNIT = '%' THEN 'Percentage' ELSE '' END as PERCENT_LABEL,
		CASE WHEN p.SYNGAS_FLOW_PERCENT_UNIT = '%' THEN 'percentage' ELSE '' END as PERCENT_ID,
		CASE WHEN p.SYNGAS_FLOW_PERCENT_UNIT = '%' THEN '%' ELSE '' END as PERCENT_TYPE,
		CASE WHEN p.SYNGAS_FLOW_PERCENT_UNIT = '%' THEN '%' ELSE '' END as PERCENT_SYMBOL,
		0 as PERCENT_INCREMENT,						--INCREMENT (for calculated percentage)--
		0 as PERCENT_DECREMENT,						--DECREMENT (for calculated percentage)--
		p.SYNGAS_FLOW_ACT_VALUE as SYNGAS_FLOW_ACT_VALUE,
		p.SYNGAS_FLOW_ACT_VALUE_UNIT as SYNGAS_FLOW_ACT_VALUE_UNIT,
		CASE WHEN Upper(p.SYNGAS_FLOW_ACT_VALUE_UNIT) = UPPER('Nm3/h') THEN 'ActualNm3h' ELSE '' END as ACTUAL_ID,
		CASE WHEN Upper(p.SYNGAS_FLOW_ACT_VALUE_UNIT) = UPPER('Nm3/h') THEN '' END as ACTUAL_TYPE,
		CASE WHEN Upper(p.SYNGAS_FLOW_ACT_VALUE_UNIT) = UPPER('Nm3/h') THEN '' END as ACTUAL_SYMBOL,
		CASE WHEN Upper(p.SYNGAS_FLOW_ACT_VALUE_UNIT) = UPPER('Nm3/h') THEN 'Actual Nm3/h' ELSE '' END as ACTUAL_LABEL,
		0 as ACTUAL_INCREMENT,							--INCREMENT (for actual)--
		0 as ACTUAL_DECREMENT,							--DECREMENT (for actual)--
		p.SYNGAS_FLOW_TOTAL as  MAINVALUE,				--mainValue
		0 as MAXVALUE,									--maxValue
		'Nm3/h' as UNIT,								--unit (Nm3/h)
		'x1000' as INDICATOR,							--Indicator
		'Total Syngas' as KPI_TITLE						--KPI Title

		FROM PlantKPI p INNER JOIN Converter c
		ON p.Converter_Converter_SID = c.Converter_SID
		where c.Section_Section_SID = @sectionId;
END";
#pragma warning disable CA1062 // Validate arguments of public methods
            migrationBuilder.Sql(procedure);
#pragma warning restore CA1062 // Validate arguments of public methods
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string procedure = "Drop PROCEDURE [dbo].[usp_SyngasSplit]";
            migrationBuilder.Sql(procedure);
        }
    }
}