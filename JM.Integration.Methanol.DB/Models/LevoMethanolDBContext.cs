using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace JM.Integration.Methanol.DB.Models
{
    public partial class LevoMethanolDBContext : DbContext
    {
        //public LevoMethanolDBContext()
        //{
        //}

        public LevoMethanolDBContext(DbContextOptions<LevoMethanolDBContext> options)
            : base(options)
        {
            if (Database != null)
                Database.GetDbConnection();
        }

        public virtual DbSet<Converter> Converters { get; set; }
        public virtual DbSet<ConverterKpi> ConverterKpis { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<DataMetric> DataMetrics { get; set; }
        public virtual DbSet<DataTemplate> DataTemplates { get; set; }
        public virtual DbSet<ErrorDetail> ErrorDetails { get; set; }
        public virtual DbSet<FittingMetric> FittingMetrics { get; set; }
        public virtual DbSet<MasterTemplate> MasterTemplates { get; set; }
        public virtual DbSet<ModelDefinition> ModelDefinitions { get; set; }
        public virtual DbSet<ModelPlantMapping> ModelPlantMappings { get; set; }
        public virtual DbSet<ModelVariable> ModelVariables { get; set; }
        public virtual DbSet<OptimisedMetric> OptimisedMetrics { get; set; }
        public virtual DbSet<Plant> Plants { get; set; }
        public virtual DbSet<PlantKpi> PlantKpis { get; set; }
        public virtual DbSet<PlantProvisionalUnit> PlantProvisionalUnits { get; set; }
        public virtual DbSet<ProcessDetail> ProcessDetails { get; set; }
        public virtual DbSet<Section> Sections { get; set; }
        public virtual DbSet<Site> Sites { get; set; }
        public virtual DbSet<UnitsofMeasurement> UnitsofMeasurements { get; set; }

        private class LEVOTSContextFactory : IDesignTimeDbContextFactory<LevoMethanolDBContext>
        {
            public LevoMethanolDBContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<LevoMethanolDBContext>();
                optionsBuilder.UseSqlServer(GetDBConnection());
                return new LevoMethanolDBContext(optionsBuilder.Options);
            }
        }

        private static SqlConnection GetDBConnection()
        {
            IConfiguration config = new ConfigurationBuilder()
           .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
           .AddJsonFile("local.settings.json")
           .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
           .Build();
            SqlConnection conn = new SqlConnection
            {
                ConnectionString = config.GetSection("Values").GetValue<string>("SqlConnectionString"),
                AccessToken = new AzureServiceTokenProvider().GetAccessTokenAsync("https://database.windows.net").Result
            };
            return conn;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Converter>(entity =>
            {
                entity.HasKey(e => e.ConverterSid)
                    .HasName("Converter_PK");

                entity.HasIndex(e => e.SectionSectionSid);

                entity.Property(e => e.ConverterSid).IsUnicode(false);

                entity.Property(e => e.CatalystName).IsUnicode(false);

                entity.Property(e => e.CatalystSupplier).IsUnicode(false);

                entity.Property(e => e.CatalystVolumeUnit).IsUnicode(false);

                entity.Property(e => e.ConverterName).IsUnicode(false);

                entity.Property(e => e.ConverterStatus).IsUnicode(false);

                entity.Property(e => e.ReactorAetTypeUnit).IsUnicode(false);

                entity.Property(e => e.ReactorDuty).IsUnicode(false);

                entity.Property(e => e.ReactorRefFlowRateUnit).IsUnicode(false);

                entity.Property(e => e.ReactorSafeAetTempUnit).IsUnicode(false);

                entity.Property(e => e.ReactorSafeMaxTempUnit).IsUnicode(false);

                entity.Property(e => e.ReactorTrgtMaxTempUnit).IsUnicode(false);

                entity.Property(e => e.ReactorType).IsUnicode(false);

                entity.Property(e => e.SectionSectionSid).IsUnicode(false);

                entity.HasOne(d => d.SectionSectionS)
                    .WithMany(p => p.Converters)
                    .HasForeignKey(d => d.SectionSectionSid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Converter_Section_FK");
            });

            modelBuilder.Entity<ConverterKpi>(entity =>
            {
                entity.HasKey(e => e.Sid)
                    .HasName("Converter_KPI_PK");

                entity.HasIndex(e => e.ConverterConverterSid);

                entity.Property(e => e.Sid).ValueGeneratedNever();

                entity.Property(e => e.Analysis).IsUnicode(false);

                entity.Property(e => e.ConvPeakTempUnit).IsUnicode(false);

                entity.Property(e => e.ConvPressDropAvgNrmUnit).IsUnicode(false);

                entity.Property(e => e.ConvPressDropMsdUnit).IsUnicode(false);

                entity.Property(e => e.ConverterConverterSid).IsUnicode(false);

                entity.Property(e => e.KpiName).IsUnicode(false);

                entity.Property(e => e.VariableName).IsUnicode(false);

                entity.HasOne(d => d.ConverterConverterS)
                    .WithMany(p => p.ConverterKpis)
                    .HasForeignKey(d => d.ConverterConverterSid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Converter_KPI_Convertor_FK");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.Sid)
                    .HasName("Customer_PK");

                entity.Property(e => e.Sid).ValueGeneratedNever();

                entity.Property(e => e.CustomerName).IsUnicode(false);
            });

            modelBuilder.Entity<DataMetric>(entity =>
            {
                entity.HasKey(e => new { e.Sid, e.PlantSid })
                    .HasName("Data_Metrics_PK");

                entity.HasIndex(e => e.MasterTemplateSid);

                entity.HasIndex(e => e.PlantSid);

                entity.Property(e => e.PlantSid).IsUnicode(false);

                entity.Property(e => e.RecordedUnit).IsUnicode(false);

                entity.Property(e => e.StandardUnit).IsUnicode(false);

                entity.Property(e => e.Variable).IsUnicode(false);

                entity.HasOne(d => d.MasterTemplateS)
                    .WithMany(p => p.DataMetrics)
                    .HasForeignKey(d => d.MasterTemplateSid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Data_Metrics_Master_Template_FK");

                entity.HasOne(d => d.PlantS)
                    .WithMany(p => p.DataMetrics)
                    .HasForeignKey(d => d.PlantSid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Data_Metrics_Plant_FK");
            });

            modelBuilder.Entity<DataTemplate>(entity =>
            {
                entity.HasKey(e => e.Sid)
                    .HasName("Data_Metrics_Templates_PK");

                entity.HasIndex(e => e.MasterTemplateSid);

                entity.HasIndex(e => e.UomSid);

                entity.Property(e => e.Sid).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).IsUnicode(false);

                entity.Property(e => e.ProvisionalUnit).IsUnicode(false);

                entity.Property(e => e.Section).IsUnicode(false);

                entity.Property(e => e.SubSection).IsUnicode(false);

                entity.Property(e => e.TemplateCode).IsUnicode(false);

                entity.Property(e => e.TemplateType).IsUnicode(false);

                entity.Property(e => e.TemplateVersion).IsUnicode(false);

                entity.Property(e => e.UpdatedBy).IsUnicode(false);

                entity.Property(e => e.Variable).IsUnicode(false);

                entity.HasOne(d => d.MasterTemplateS)
                    .WithMany(p => p.DataTemplates)
                    .HasForeignKey(d => d.MasterTemplateSid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Data_Template_Master_Template_FK");

                entity.HasOne(d => d.UomS)
                    .WithMany(p => p.DataTemplates)
                    .HasForeignKey(d => d.UomSid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Data_Template_Units_of_Measurement_FK");
            });

            modelBuilder.Entity<ErrorDetail>(entity =>
            {
                entity.HasKey(e => e.Sid)
                    .HasName("Error_Details_PK");

                entity.HasIndex(e => e.ProcessDetailsSid);

                entity.Property(e => e.Sid).ValueGeneratedNever();

                entity.Property(e => e.ErrorDesc).IsUnicode(false);

                entity.Property(e => e.ErrorRecStatus).IsUnicode(false);

                entity.Property(e => e.FileName).IsUnicode(false);

                entity.HasOne(d => d.ProcessDetailsS)
                    .WithMany(p => p.ErrorDetails)
                    .HasForeignKey(d => d.ProcessDetailsSid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Error_Details_Process_Details_FK");
            });

            modelBuilder.Entity<FittingMetric>(entity =>
            {
                entity.HasKey(e => e.Sid)
                    .HasName("Fitting_Metrics_PK");

                entity.HasIndex(e => e.MvSid);

                entity.HasIndex(e => e.PlantSid);

                entity.Property(e => e.Sid).ValueGeneratedNever();

                entity.Property(e => e.PlantSid).IsUnicode(false);

                entity.Property(e => e.ProvisionalUnit).IsUnicode(false);

                entity.Property(e => e.StandardUnit).IsUnicode(false);

                entity.HasOne(d => d.MvS)
                    .WithMany(p => p.FittingMetrics)
                    .HasForeignKey(d => d.MvSid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Fitting_Metrics_Model_Variables_FK");

                entity.HasOne(d => d.PlantS)
                    .WithMany(p => p.FittingMetrics)
                    .HasForeignKey(d => d.PlantSid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Fitting_Metrics_Plant_FK");
            });

            modelBuilder.Entity<MasterTemplate>(entity =>
            {
                entity.HasKey(e => e.Sid)
                    .HasName("Master_Template_PK");

                entity.HasIndex(e => e.CustomerSid);

                entity.Property(e => e.Sid).ValueGeneratedNever();

                entity.Property(e => e.Converter1TemplateCode)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Converter2TemplateCode)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Converter3TemplateCode)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Converter4TemplateCode)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.PlantPlantSid).IsUnicode(false);

                entity.Property(e => e.PlantTemplateCode)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.SectionTemplateCode)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.SiteSiteId).IsUnicode(false);

                entity.HasOne(d => d.CustomerS)
                    .WithMany(p => p.MasterTemplates)
                    .HasForeignKey(d => d.CustomerSid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Master_Template_Customer_FK");

                entity.HasOne(d => d.PlantPlantS)
                    .WithMany(p => p.MasterTemplates)
                    .HasForeignKey(d => d.PlantPlantSid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("MasterTemplate_Plant_FK");

                entity.HasOne(d => d.SiteSite)
                    .WithMany(p => p.MasterTemplates)
                    .HasForeignKey(d => d.SiteSiteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Master_Template_Site_FK");
            });

            modelBuilder.Entity<ModelDefinition>(entity =>
            {
                entity.HasKey(e => e.ModelSid)
                    .HasName("Bestfit_Model_Definition_PK");

                entity.Property(e => e.ModelSid).ValueGeneratedNever();

                entity.Property(e => e.ModelName).IsUnicode(false);
            });

            modelBuilder.Entity<ModelPlantMapping>(entity =>
            {
                entity.HasKey(e => e.Sid)
                    .HasName("Model_Plant_Mapping_PK");

                entity.HasIndex(e => e.DataTemplateSid);

                entity.HasIndex(e => e.ModelVariableSid);

                entity.Property(e => e.Sid).ValueGeneratedNever();

                entity.Property(e => e.MappingType).IsUnicode(false);

                entity.Property(e => e.Step).IsUnicode(false);

                entity.Property(e => e.Weight).IsUnicode(false);

                entity.HasOne(d => d.DataTemplateS)
                    .WithMany(p => p.ModelPlantMappings)
                    .HasForeignKey(d => d.DataTemplateSid)
                    .HasConstraintName("Model_Plant_Mapping_Data_Template_FK");

                entity.HasOne(d => d.ModelVariableS)
                    .WithMany(p => p.ModelPlantMappings)
                    .HasForeignKey(d => d.ModelVariableSid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Model_Plant_Mapping_Model_Variables_FK");
            });

            modelBuilder.Entity<ModelVariable>(entity =>
            {
                entity.HasKey(e => e.Sid)
                    .HasName("Model_Variables_PK");

                entity.HasComment("Contain BestFit model definition as the config data to enable automatic orchestration of the fitting process ");

                entity.HasIndex(e => e.MdmSid);

                entity.Property(e => e.Sid).ValueGeneratedNever();

                entity.Property(e => e.CreateBy).IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.ModelName).IsUnicode(false);

                entity.Property(e => e.ReadWrite).IsUnicode(false);

                entity.Property(e => e.Section).IsUnicode(false);

                entity.Property(e => e.Step).IsUnicode(false);

                entity.Property(e => e.Unit).IsUnicode(false);

                entity.Property(e => e.UpdatedBy).IsUnicode(false);

                entity.Property(e => e.VariableName).IsUnicode(false);

                entity.Property(e => e.Weight).IsUnicode(false);

                entity.HasOne(d => d.MdmS)
                    .WithMany(p => p.ModelVariables)
                    .HasForeignKey(d => d.MdmSid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Model_Variables_Model_Definition_FK");
            });

            modelBuilder.Entity<OptimisedMetric>(entity =>
            {
                entity.HasKey(e => e.Sid)
                    .HasName("Optimised_Metric_PK");

                entity.Property(e => e.Sid).ValueGeneratedNever();

                entity.Property(e => e.ProvisionalUnit).IsUnicode(false);

                entity.Property(e => e.StandardUnit).IsUnicode(false);
            });

            modelBuilder.Entity<Plant>(entity =>
            {
                entity.HasKey(e => e.PlantSid)
                    .HasName("Plant_PK");

                entity.Property(e => e.PlantSid).IsUnicode(false);

                entity.Property(e => e.MaxProdFlowUnit).IsUnicode(false);

                entity.Property(e => e.MaxSyngFlowUnit).IsUnicode(false);

                entity.Property(e => e.PlantCapacityUnit).IsUnicode(false);

                entity.Property(e => e.PlantName).IsUnicode(false);

                entity.Property(e => e.PlantStatus).IsUnicode(false);

                entity.Property(e => e.SiteSiteId).IsUnicode(false);
            });

            modelBuilder.Entity<PlantKpi>(entity =>
            {
                entity.HasKey(e => e.Sid)
                    .HasName("Plant_KPI_PK");

                entity.HasIndex(e => e.ConverterConverterSid);

                entity.Property(e => e.Sid).ValueGeneratedNever();

                entity.Property(e => e.ConvPeakTempAnalysis).IsUnicode(false);

                entity.Property(e => e.ConvPeakTempUnit).IsUnicode(false);

                entity.Property(e => e.ConverterConverterSid)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.KpiName).IsUnicode(false);

                entity.Property(e => e.SyngasFlowActValueUnit).IsUnicode(false);

                entity.Property(e => e.SyngasFlowPercentUnit).IsUnicode(false);

                entity.Property(e => e.SyngasFlowTotalUnit).IsUnicode(false);

                entity.Property(e => e.VariableName).IsUnicode(false);

                entity.HasOne(d => d.ConverterConverterS)
                    .WithMany(p => p.PlantKpis)
                    .HasForeignKey(d => d.ConverterConverterSid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PlantKPI_Convertor_FK");
            });

            modelBuilder.Entity<PlantProvisionalUnit>(entity =>
            {
                entity.HasKey(e => e.Sid)
                    .HasName("Plant_Provisonal_Units_PK");

                entity.Property(e => e.Sid).ValueGeneratedNever();

                entity.Property(e => e.PlantPlantSid).IsUnicode(false);

                entity.Property(e => e.ProvisionalUnitName).IsUnicode(false);

                entity.Property(e => e.ProvisionalUnitSymbol).IsUnicode(false);

                entity.HasOne(d => d.PlantPlantS)
                    .WithMany(p => p.PlantProvisionalUnits)
                    .HasForeignKey(d => d.PlantPlantSid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Plant_Provisonal_Units_Plant_FK");

                entity.HasOne(d => d.UomUnitS)
                    .WithMany(p => p.PlantProvisionalUnits)
                    .HasForeignKey(d => d.UomUnitSid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Plant_Provisonal_Units_Units_of_Measurement_FK");
            });

            modelBuilder.Entity<ProcessDetail>(entity =>
            {
                entity.HasKey(e => e.Sid)
                    .HasName("Process_Details_PK");

                entity.HasIndex(e => e.PlantPlantSid);

                entity.Property(e => e.Sid).ValueGeneratedNever();

                entity.Property(e => e.AvScanStatus).IsUnicode(false);

                entity.Property(e => e.HistoryRevFlag).IsUnicode(false);

                entity.Property(e => e.PlantPlantSid).IsUnicode(false);

                entity.Property(e => e.PreChkStatus).IsUnicode(false);

                entity.Property(e => e.ProcessStatus).IsUnicode(false);

                entity.Property(e => e.Report).IsUnicode(false);

                entity.Property(e => e.SiteId).IsUnicode(false);

                entity.Property(e => e.Summary).IsUnicode(false);

                entity.Property(e => e.UploadFileName).IsUnicode(false);

                entity.Property(e => e.UploadFileStatus).IsUnicode(false);

                entity.HasOne(d => d.PlantPlantS)
                    .WithMany(p => p.ProcessDetails)
                    .HasForeignKey(d => d.PlantPlantSid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Process_Details_Plant_FK");
            });

            modelBuilder.Entity<Section>(entity =>
            {
                entity.HasKey(e => e.SectionSid)
                    .HasName("Section_PK");

                entity.HasIndex(e => e.PlantPlantSid);

                entity.Property(e => e.SectionSid).IsUnicode(false);

                entity.Property(e => e.PlantPlantSid).IsUnicode(false);

                entity.Property(e => e.SectionName).IsUnicode(false);

                entity.HasOne(d => d.PlantPlantS)
                    .WithMany(p => p.Sections)
                    .HasForeignKey(d => d.PlantPlantSid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Section_Plant_FK");
            });

            modelBuilder.Entity<SectionKpi>(entity =>
            {
                entity.HasKey(e => e.Sid)
                    .HasName("SectionKPI_PK");

                entity.Property(e => e.Sid).ValueGeneratedNever();

                entity.Property(e => e.Analysis).IsUnicode(false);

                entity.Property(e => e.ConverterConverterSid).IsUnicode(false);

                entity.Property(e => e.KpiName).IsUnicode(false);

                entity.Property(e => e.MethanolProdActualUnit).IsUnicode(false);

                entity.Property(e => e.VariableName).IsUnicode(false);
            });

            modelBuilder.Entity<Site>(entity =>
            {
                entity.Property(e => e.SiteId).IsUnicode(false);

                entity.Property(e => e.CompanyName).IsUnicode(false);

                entity.Property(e => e.SiteLocationCountry).IsUnicode(false);

                entity.Property(e => e.SiteLocationState).IsUnicode(false);

                entity.Property(e => e.SiteName).IsUnicode(false);

                entity.Property(e => e.SiteSubsrTier).IsUnicode(false);
            });

            modelBuilder.Entity<UnitsofMeasurement>(entity =>
            {
                entity.HasKey(e => e.UnitSid)
                    .HasName("Units_of_Measurement_PK");

                entity.Property(e => e.UnitSid).ValueGeneratedNever();

                entity.Property(e => e.UnitFamily).IsUnicode(false);

                entity.Property(e => e.UnitName).IsUnicode(false);

                entity.Property(e => e.UnitSymbol).IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}