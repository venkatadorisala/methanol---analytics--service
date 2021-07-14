using Microsoft.EntityFrameworkCore.Migrations;

namespace JM.Integration.Methanol.DB.Migrations
{
    public partial class USP_GetMethanolProductionKPIBySectionId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string procedure = @"
			CREATE OR ALTER PROCEDURE USP_GetMethanolProductionKPIBySectionId
			(
				@sectionId VARCHAR(100)
			)
			AS
			BEGIN		
					SET NOCOUNT ON;

					Declare @tempDatasetMethanolProduction table(

						title  varchar(100),
						name varchar(100),
						url varchar(100),

						percentLabel Varchar(100),
						percentValue Varchar(100),
						percentIncrement float,
						percentDecrement float,
						percentType Varchar(100),
						percentSymbol Varchar(100),
						percentId Varchar(100),

						actualLabel Varchar(100),
						actualValue Varchar(100),
						actualIncrement float,
						actualDecrement float,
						actualType Varchar(100),
						actualSymbol Varchar(100),
						actualId Varchar(100),

						mainValue float,
						maxValue float,
						unit Varchar(100),
						indicator Varchar(100)
					);

					Declare @tempDataPointMethanolProduction table(
						y  float,
						label varchar(100)
					);

					INSERT INTO @tempDatasetMethanolProduction
						SELECT 
						p.KPI_Name as title,
						c.Converter_Name as name,
						c.Converter_SID as url,

						'Percentage' as percentLabel,
						p.Methanol_Prod_actual_perc as percentValue,
						0 as percentIncrement,						
						0 as percentDecrement,
						'%' as percentType,
						'%' as percentSymbol,
						'percentage' as percentId,
		
						CASE WHEN Upper(p.Methanol_Prod_actual_unit) = UPPER('mtpd') THEN 'Actual mtpd' ELSE '' END as actualLabel,
						p.Methanol_Prod_actual as actualValue,
						0 as actualIncrement,							
						0 as actualDecrement,
						'' as actualType,
						'' as actualValue,
						'actualMtpd' as actualId,
											
						p.Methanol_Prod_Total_prod as  mainValue,			
						0 as maxValue,									
						'mtpd' as unit,								
						'x1000' as indicator							
		
					FROM SectionKPI p INNER JOIN Converter c
					ON p.Converter_Converter_SID = c.Converter_SID
					where c.Section_Section_SID = @sectionId 
					AND UPPER(p.KPI_Name) = UPPER('Methanol Production conveter');

					Insert  INTO @tempDataPointMethanolProduction  
						Select  Isnull(Methanol_Prod_actual,0) as y, 
								[Converter_Name] as label
						FROM  [dbo].[SectionKPI] inner join [dbo].[Converter] 
						ON [Converter_Converter_SID] = [Converter_SID] 
						AND Section_Section_SID = @sectionId 
						WHERE UPPER(KPI_Name) = UPPER('Methanol Production conveter');

					Declare @dataset  varchar(max);
					Declare @datapoint  varchar(max);
					set @dataset =  (select * from  @tempDatasetMethanolProduction for json auto);
					set @datapoint =  (select * from  @tempDataPointMethanolProduction for json auto);
					select @dataset as 'dataset', @datapoint as 'datapoint', 'Methanol Production' as title;

			END
			GO
/*Grant Statement */
			GRANT EXECUTE ON [dbo].[USP_GetMethanolProductionKPIBySectionId] TO Public 
			GO";

			migrationBuilder.Sql(procedure);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
			string procedure = " DROP PROCEDURE [dbo].[USP_GetMethanolProductionKPIBySectionId];";
			migrationBuilder.Sql(procedure);
		}
    }
}
