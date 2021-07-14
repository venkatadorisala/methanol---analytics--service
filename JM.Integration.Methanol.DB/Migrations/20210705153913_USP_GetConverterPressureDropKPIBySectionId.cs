using Microsoft.EntityFrameworkCore.Migrations;

namespace JM.Integration.Methanol.DB.Migrations
{
    public partial class USP_GetConverterPressureDropKPIBySectionId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

			string procedure = @"CREATE OR Alter PROCEDURE [dbo].[USP_GetConverterPressureDropKPIBySectionId]
			(
	   @sectionId VARCHAR(100)
)
AS
BEGIN

	SET NOCOUNT ON


		Declare @tempDatasetPressureDropConverterAPI table(

				title varchar(100),
				name varchar(100),
				url varchar(100),
				

				analysisId Varchar(100),
				analysisLabel Varchar(100),
				analysisValue Varchar(100),
				analysisIncrement float,
				analysisDecrement float,
				analysisType Varchar(100),
				analysisSymbol Varchar(100),
				

				normalised4weekavgId Varchar(100),
				normalised4weekavgLabel Varchar(100),
				normalised4weekavgValue Varchar(100),
				normalised4weekavgIncrement float,
				normalised4weekavgDecrement float,
				normalised4weekavgType Varchar(100),
				normalised4weekavgSymbol Varchar(100),
				

				normalisedLifetimeAvgId Varchar(100),
				normalisedLifetimeAvgLabel Varchar(100),
				normalisedLifetimeAvgValue float,
				normalisedLifetimeAvgIncrement float,
				normalisedLifetimeAvgDecrement float,
				normalisedLifetimeAvgType Varchar(100),
				normalisedLifetimeAvgSymbol Varchar(100),

				kpiTitle Varchar(100),
				maxValue  float,
				unit  Varchar(10),
				indicator Varchar(10)
			)

			Declare @tempDataPointPressureDropConverterAPI table(

				y  float,
				label varchar(100)
			)

			INSERT INTO @tempDatasetPressureDropConverterAPI
					SELECT
					CKPI.KPI_Name AS title,
					C.CONVERTER_NAME AS name,
					CKPI.Converter_Converter_SID AS url,
      
					'analysis' AS analysisId,
					'Analysis' AS analysisLabel,
					CKPI.ANALYSIS AS analysisValue,
					0 AS analysisIncrement,
					0 AS analysisDecrement,
					'' AS analysisType,
					'' AS analysisSymbol,



					'normalised4WeekAvg' AS normalised4weekavgId,
					'Normalised 4 week avg.' AS normalised4weekavgLabel,
					CKPI.CONV_PRESS_DROP_FW_AVG_NRM AS normalised4weekavgValue,
					CKPI.CONV_PRESS_DRP_FW_AVG_NRM_INCR AS normalised4weekavgIncrement,
					CKPI.CONV_PRESS_DRP_FW_AVG_NRM_DECR AS normalised4weekavgDecrement,
					CASE WHEN CKPI.CONV_PRESS_DROP_AVG_NRM_UNIT = 'millibar/year' THEN 'millibar/year' ELSE '' END AS normalised4weekavgType, --#check Mapping
					CKPI.CONV_PRESS_DROP_AVG_NRM_UNIT AS normalised4weekavgSymbol, --#check Mapping
				

					'normalisedLifetimeAvg' AS normalisedLifetimeAvgId,
					'Normalised Lifetime avg.' AS normalisedLifetimeAvgLabel,
					IsNull(CKPI.CONV_PRESS_DROP_LF_AVG_NRM, 0) AS normalisedLifetimeAvgValue,
					0 AS normalisedLifetimeAvgIncrement,
					0 AS normalisedLifetimeAvgDecrement,
					CASE WHEN CKPI.CONV_PRESS_DROP_AVG_NRM_UNIT = 'mbar/y' THEN 'mbar/y' ELSE '' END AS  normalisedLifetimeAvgType,-- #check for mapping
					CONV_PRESS_DROP_AVG_NRM_UNIT AS normalisedLifetimeAvgSymbol,
					 'Measured pressure drop comparison' as kpiTitle,
					 0 as maxValue,
					 CONV_PRESS_DROP_MSD_UNIT as unit,
					 '' as indicator







					FROM[dbo].[ConverterKPI] CKPI inner join[dbo].[Converter] C
				  ON CKPI.[Converter_Converter_SID] = C.[Converter_SID] and
					c.Section_Section_SID = @sectionId where CKPI.KPI_Name = 'Converter pressure drop'






					Insert INTO @tempDataPointPressureDropConverterAPI Select  Isnull(CONV_PRESS_DROP_MSD, 0) as y, [Converter_Name] as label from
				   [dbo].[ConverterKPI] inner join[dbo].[Converter] on[Converter_Converter_SID] = [Converter_SID] and Section_Section_SID = @sectionId
					where KPI_Name = 'Converter pressure drop'




			Declare @dataset  varchar(max)
			Declare @datapoint  varchar(max)

			set @dataset = (select * from @tempDatasetPressureDropConverterAPI for json auto  )
				set @datapoint = (select * from @tempDataPointPressureDropConverterAPI for json auto )
				select @dataset as 'dataset', @datapoint as 'datapoint', 'Converter pressure drop' as title
END   GRANT EXECUTE ON [dbo].[USP_GetConverterPressureDropKPIBySectionId] TO Public";
			migrationBuilder.Sql(procedure);


		}

        protected override void Down(MigrationBuilder migrationBuilder)
        {
			string procedure = " Drop PROCEDURE  [dbo].[USP_GetConverterPressureDropKPIBySectionId];";
			migrationBuilder.Sql(procedure);
		}
    }
}
