using Microsoft.EntityFrameworkCore.Migrations;

namespace JM.Integration.Methanol.DB.Migrations
{
    public partial class USP_GetConverterPeakTempKPIBySectionId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			string procedure = @"CREATE OR ALTER PROCEDURE [dbo].[USP_GetConverterPeakTempKPIBySectionId]
(
		@sectionId VARCHAR(100)
)
AS
BEGIN
    SET NOCOUNT ON

    
		Declare @tempDatasetConverterAPI table(

				title  varchar(100),
				name varchar(100),
				url varchar(100),

				analysisLabel Varchar(100),
				analysisValue Varchar(100),
				analysisIncrement float,
				analysisDecrement float,
				analysisType Varchar(100),
				analysisSymbol Varchar(100),
				analysisId Varchar(100),

				peakLabel Varchar(100),
				peakValue Varchar(100),
				peakIncrement float,
				peakDecrement float,
				peakType Varchar(100),
				peakSymbol Varchar(100),
				peakId Varchar(100),

				targetMaxId Varchar(100),
				targetMaxLabel Varchar(100),
				targetMaxValue float,
				targetMaxIncrement float,
				targetMaxDecrement float,
				targetMaxType Varchar(100),
				targetMaxSymbol Varchar(100),


				safetyMaxId Varchar(100),
				safetyMaxLabel Varchar(100),
				safetyMaxValue float,
				safetyMaxIncrement float,
				safetyMaxDecrement float,
				safetyMaxType Varchar(100),
				safetyMaxSymbol Varchar(100)
			)

			Declare @tempDataPointConverterAPI table(

				y  float,
				label varchar(100)
			)

			INSERT INTO @tempDatasetConverterAPI
					SELECT 
					CKPI.KPI_Name AS title,
					C.CONVERTER_NAME AS name,
					CKPI.Converter_Converter_SID AS url,
      
					'Analysis' AS analysisLabel,
					CKPI.ANALYSIS AS analysisValue,
					0 AS analysisIncrement,
					0 AS analysisDecrement,
					'' AS analysisType,
					'' AS analysisSymbol,
					'analysis' AS analysisId,

					'Peak' AS peakLabel,
					CKPI.CONV_PEAK_TEMP AS peakValue,
					0 AS peakIncrement,
					0 AS peakDecrement,
					CASE WHEN CKPI.CONV_PEAK_TEMP_UNIT = 'C' THEN 'Celsius' ELSE '' END  AS peakType,
					CKPI.CONV_PEAK_TEMP_UNIT AS peakSymbol,
					'peak' AS peakId,

					'targetMax' AS targetMaxId,
					'Target Max' AS targetMaxLabel,
					IsNull(CKPI.CONV_PEAK_TEMP_TRGT_MAX,0) AS targetMaxValue,
					IsNull(CKPI.CONV_PEAK_TEMP_TRGT_MAX_INCR,0) AS targetMaxIncrement,
					ISNull(CKPI.CONV_PEAK_TEMP_TRGT_MAX_DECR,0) AS targetMaxDecrement,
					CASE WHEN CKPI.CONV_PEAK_TEMP_UNIT = 'C' THEN 'Celsius' ELSE '' END  AS  targetMaxType,
					CONV_PEAK_TEMP_UNIT AS targetMaxSymbol,
					

					'safetyMax' AS safetyMaxId, 
					'Safety Max' AS safetyMaxLabel,
					IsNull(CKPI.CONV_PEAK_TEMP_SFTY_MAX,0)   AS safetyMaxValue,
					IsNull(CKPI.CONV_PEAK_TEMP_SFTY_MAX_INCR,0) AS safetyMaxIncrement,
			 
					IsNull(CKPI.CONV_PEAK_TEMP_SFTY_MAX_DECR,0) AS safetyMaxDecrement,
					CASE WHEN CKPI.CONV_PEAK_TEMP_UNIT = 'C' THEN 'Celsius' ELSE '' END  AS  safetyMaxType,
					CONV_PEAK_TEMP_UNIT AS safetyMaxSymbol


	 
					FROM [dbo].[ConverterKPI] CKPI inner join [dbo].[Converter] C ON CKPI.[Converter_Converter_SID] = C.[Converter_SID] and c.Section_Section_SID =@sectionId where CKPI.KPI_Name ='Converter peak temparture' 


			


					Insert  INTO @tempDataPointConverterAPI  Select  Isnull(CONV_PEAK_TEMP,0) as y, [Converter_Name] as label from  [dbo].[ConverterKPI] inner join [dbo].[Converter] on [Converter_Converter_SID] = [Converter_SID] and Section_Section_SID =@sectionId where KPI_Name ='Converter peak temparture' 

	


			Declare @dataset  varchar(max)
			Declare @datapoint  varchar(max)

			set @dataset =  (select * from  @tempDatasetConverterAPI for json auto 	)
			set @datapoint =  (select * from  @tempDataPointConverterAPI for json auto )
			select @dataset as 'dataset', @datapoint as 'datapoint', 'Converter peak temperature' as title
END  GRANT EXECUTE ON [dbo].[USP_GetConverterPressureDropKPIBySectionId] TO Public";
			migrationBuilder.Sql(procedure);
		}

        protected override void Down(MigrationBuilder migrationBuilder)
        {
			string procedure = " Drop PROCEDURE [dbo].[USP_GetConverterPeakTempKPIBySectionId];";
			migrationBuilder.Sql(procedure);
		}
    }
}
