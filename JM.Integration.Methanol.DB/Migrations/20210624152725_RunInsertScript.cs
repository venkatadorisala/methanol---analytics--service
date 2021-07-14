using Microsoft.EntityFrameworkCore.Migrations;
using System.IO;
using System.Reflection;

namespace JM.Integration.Methanol.DB.Migrations
{
    public partial class RunInsertScript : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //var resourceNames = this.GetType().Assembly.GetManifestResourceNames();
            //var sqlResourceName = resourceNames.SingleOrDefault(r => r.Contains("20210624152725_RunInsertScript.sql"));
            //migrationBuilder.Sql(sqlResourceName);

            var assembly = Assembly.GetExecutingAssembly();
            string resourceName = typeof(RunInsertScript).Namespace + ".20210624152725_RunInsertScript.sql";
            using Stream stream = assembly.GetManifestResourceStream(resourceName);
            using StreamReader reader = new StreamReader(stream);
            string sqlResult = reader.ReadToEnd();
            migrationBuilder.Sql(sqlResult);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}