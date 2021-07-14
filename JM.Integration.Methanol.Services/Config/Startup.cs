// Copyright (c) Johnson Matthey Organization 2021. All rights reserved.

using JM.Integration.Methanol.Services.Config;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Startup))]

namespace JM.Integration.Methanol.Services.Config
{
    using Azure.Storage.Blobs;
    using JM.Integration.Common;
    using JM.Integration.Common.Interfaces;
    using JM.Integration.Common.Services;
    using JM.Integration.Methanol.Common;
    using JM.Integration.Methanol.Services;
    using JM.Integration.Methanol.Services.Interface;
    using JM.Integration.Methanol.Services.Mapper;
    using JM.Integration.Methanol.DB.Models;
    using JM.Integration.Methanol.Services.Services;
    using Microsoft.ApplicationInsights.Extensibility;
    using Microsoft.Azure.Functions.Extensions.DependencyInjection;
    using Microsoft.Azure.Services.AppAuthentication;
    using Microsoft.Data.SqlClient;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Azure;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using nClam;
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;

    /// <summary>
    /// FunctionStartup.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                                        .SetBasePath(Directory.GetCurrentDirectory())
                                         .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                                         .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                                        .AddEnvironmentVariables()
                                        .Build();

            MethanolFunctionConfiguration functionConfiguration = new MethanolFunctionConfiguration();
            config.Bind(functionConfiguration);
            builder.Services.AddSingleton<IFunctionConfiguration>(functionConfiguration);
            functionConfiguration.AzureStorageConnectionString = Environment.GetEnvironmentVariable("StorageAccountConnectionString");
            functionConfiguration.Gen2DataLakeConnectionString = Environment.GetEnvironmentVariable("Gen2DataLakeConnectionString");
            builder.Services.AddAzureClients(cfg =>
            {
                cfg.AddBlobServiceClient(functionConfiguration.Gen2DataLakeConnectionString).WithVersion(BlobClientOptions.ServiceVersion.V2019_02_02).WithName("Gen2DataLakeConnectionString");
                cfg.AddBlobServiceClient(functionConfiguration.AzureStorageConnectionString).WithVersion(BlobClientOptions.ServiceVersion.V2019_02_02);
               
            });


            //var blobConnection = Environment.GetEnvironmentVariable("Gen2WebJobsStorage");
            //builder.Services.AddSingleton(x => new BlobServiceClient(blobConnection));

            builder.Services.AddSingleton<IBlobService, MethanolBlobService>();
            builder.Services.AddTransient<IStorageQueue, MethanolStorageQueue>();
            builder.Services.AddTransient<IScanFileAsync, MethanolScanFile>();
            builder.Services.AddTransient<IProcessDetailService, ProcessDetailService>();
            builder.Services.AddSingleton<IBaseDataAccess, BaseDataAccess>();
            builder.Services.AddTransient<ISyngasSplitService, SyngasSplitService>();
            builder.Services.AddSingleton<ITelemetryInitializer, TelemetryInitializer>();
            builder.Services.AddSingleton<IActivityTagger, ActivityTagger>();
            builder.Services.AddTransient<IReqValidator, ReqValidator>();

            builder.Services.AddTransient<IRequestProcessor, ConverterPeakTempRequestProcessor>();
            builder.Services.AddTransient<IPeakTempTransform, PeakTempTransform>();
            builder.Services.AddTransient<IPlantService, PlantService>();
            builder.Services.AddTransient<IFileExtensionValidationService, FileExtensionValidationService>();
            builder.Services.AddTransient<IPlantService, PlantService>();
            builder.Services.AddTransient<IProcessDetailService, ProcessDetailService>();
            builder.Services.AddTransient<IExcelFileContentExtractorService, ExcelFileContentExtractorService>();
            builder.Services.AddTransient<IKPIResponseTransform, PressureDropTempTramsform>();
            builder.Services.AddTransient<IRequestProcessor, ConverterPressureDropRequestProcessor>();

            builder.Services.AddTransient<IRequestProcessor, MethanolProductionPerConverterProcessor>();
            builder.Services.AddTransient<IMethanolPerConverterTransform, MethanolProductionConverterTransform>();

            builder.Services.AddTransient<IRequestProcessor, ReactorConverterPeakTempRequestProcessor>();
            builder.Services.AddTransient<IReactorPeakTempTransform, ReactorPeakTempTransform>();

            var clamAvServer = Environment.GetEnvironmentVariable("clamAvServer");
            var clamAvServerPort = int.Parse(Environment.GetEnvironmentVariable("clamAvServerPort"));
            var cclient = new ClamClient(clamAvServer, clamAvServerPort);
            builder.Services.AddSingleton<IClamClient>(cavclient => cclient);
            builder.Services.AddDbContextPool<LevoMethanolDBContext>(options => options.UseSqlServer(GetDBConnection(), x => x.MigrationsAssembly("JM.Integration.Methanol.DB")));
        }

        private static SqlConnection GetDBConnection()
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Environment.GetEnvironmentVariable("SqlConnectionString");
            conn.AccessToken = new AzureServiceTokenProvider().GetAccessTokenAsync("https://database.windows.net").Result;
            return conn;
        }
    }
}