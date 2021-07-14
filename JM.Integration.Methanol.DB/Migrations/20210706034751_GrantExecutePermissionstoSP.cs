using Microsoft.EntityFrameworkCore.Migrations;

namespace JM.Integration.Methanol.DB.Migrations
{
    public partial class GrantExecutePermissionstoSP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("GRANT EXECUTE ON [dbo].[usp_SyngasSplit] TO Public");
            migrationBuilder.Sql("GRANT EXECUTE ON [dbo].[USP_GetConverterPeakTempKPIBySectionId] TO Public");
            migrationBuilder.Sql("GRANT EXECUTE ON [dbo].[USP_GetConverterPressureDropKPIBySectionId] TO Public");
        
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
