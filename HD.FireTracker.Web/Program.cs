using Microsoft.Extensions.DependencyInjection;
using Hangfire;
using Hangfire.SqlServer;
using Hangfire.AspNetCore;

using Microsoft.Extensions.Configuration;
using AutoMapper;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using HD.FireTracker.Common.Classes.CustomConfig;
using HD.FireTracker.Common.Interfaces.Logging;
using HD.FireTracker.Web.AppCode.DefaultImplementation;
using HD.FireTracker.Web.AppCode.RecurringJobCommon;
using HD.FireTracker.Web.AppCode.MyRecurringJobProjects;
using HD.FireTracker.Data.Common.IRepositories.FireTrackerDB.ModelRepo;
using HD.FireTracker.DB.FireTrackerDB.Repository.ModelRepos;
using HD.FireTracker.Data.Common.IRepositories.Base;
using HD.FireTracker.DB.FireTrackerDB.Repository.Base;
using HD.FireTracker.Data.Service.Interfaces.IServices.Repository.FireTrackerDB;
using HD.FireTracker.Data.Service.Services.Repository.FireTrackerDB;
using HD.FireTracker.Data.Common.Specifications.Base;
using HD.FireTracker.Common.Extensions;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HD.FireTracker.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connString = builder.Configuration.GetConnectionString(HD.FireTracker.Common.Consts.ConstNames.HangfireConnection);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            ///// Data Base Configuration
            builder.Services.AddScoped<HD.FireTracker.DB.FireTrackerDB.FireTrackerDbContext>();
            builder.Services.AddDbContext<HD.FireTracker.DB.FireTrackerDB.FireTrackerDbContext>(options =>
            {
                options.UseSqlServer(
                    connString
                    );
            });

            // Add custom appsettings configurations
            //builder.Services.Configure<FireTrackerRecurringJobSettings>(builder.Configuration.GetSection("FireTrackerRecurringJobSettings"));

            //Add mapped interfaces

            builder.Services.AddScoped(typeof(IFireTrackerLogger), typeof(FireTrackerLogger));
            builder.Services.AddScoped(typeof(IRecurringProcessDetailProjectionRepository), typeof(RecurringProcessDetailProjectionRepository));
            builder.Services.AddScoped(typeof(IRecurringProcessDetailRepository), typeof(RecurringProcessDetailRepository));
            builder.Services.AddScoped(typeof(IRecurringProcessRepository), typeof(RecurringProcessRepository));
            builder.Services.AddScoped(typeof(IRecurringProcessSummaryRepository), typeof(RecurringProcessSummaryRepository));
            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            builder.Services.AddScoped(typeof(IRepositoryReadOnly<>), typeof(RepositoryReadOnly<>));
            builder.Services.AddScoped(typeof(IRecurringProcessDetailProjectionService), typeof(RecurringProcessDetailProjectionService));
            builder.Services.AddScoped(typeof(IRecurringProcessDetailService), typeof(RecurringProcessDetailService));
            builder.Services.AddScoped(typeof(IRecurringProcessService), typeof(RecurringProcessService));
            builder.Services.AddScoped(typeof(IRecurringProcessSummaryService), typeof(RecurringProcessSummaryService));
            builder.Services.AddScoped(typeof(ISpecification<>), typeof(BaseSpecification<>));
            builder.Services.AddScoped(typeof(IRecurringProcessLogCleanupService), typeof(RecurringProcessLogCleanupService));
            builder.Services.AddScoped(typeof(IRecurringJobConfigSettings), typeof(RecurringJobConfigSettings));
           

            //Add AutoMapper
            builder.Services.AddAutoMapper(typeof(HD.FireTracker.Data.Service.Mapper.MappingProfile).Assembly);
            

            //Add Hangfire services.
            builder.Services.AddHangfire(configuration => configuration
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseSerilogLogProvider()
            .UseSqlServerStorage
            (connString, new SqlServerStorageOptions
            { CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
              SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
              QueuePollInterval = TimeSpan.Zero,
              UseRecommendedIsolationLevel = true,
              DisableGlobalLocks = true
            })
            );
            JobStorage.Current = new SqlServerStorage(connString);

            // Add the processing server
            builder.Services.AddHangfireServer();

            #region "Region: Serilog"

            //Serilog
            var columnOptionsRecurringProcess = new ColumnOptions
            {
                AdditionalColumns = new List<SqlColumn>
                {
                    new SqlColumn{ ColumnName = "TaskManagerProcessId", PropertyName="TaskManagerProcessId", DataType=System.Data.SqlDbType.VarChar, DataLength=50, AllowNull=true}, 
                    new SqlColumn{ ColumnName = "RecurringJobName", PropertyName="RecurringJobName", DataType=System.Data.SqlDbType.VarChar, DataLength=256, AllowNull=true },
                    new SqlColumn{ ColumnName = "LogCleanupDate", PropertyName="LogCleanupDate", DataType=System.Data.SqlDbType.DateTime2, DataLength=256, AllowNull=true },
                }
            };

            var columnOptionsRecurringProcessDetail = new ColumnOptions
            {
                AdditionalColumns = new List<SqlColumn>
                {
                    new SqlColumn{ ColumnName = "TaskManagerProcessId", PropertyName="TaskManagerProcessId", DataType=System.Data.SqlDbType.VarChar, DataLength=50, AllowNull=true},
                    new SqlColumn{ ColumnName = "MessageType", PropertyName="MessageType", DataType=System.Data.SqlDbType.VarChar, DataLength=20, AllowNull=true },
                    new SqlColumn{ ColumnName = "FireTrackerMsg", PropertyName="FireTrackerMsg", DataType=System.Data.SqlDbType.VarChar, DataLength = -1, AllowNull=true },
                }
            };

            Log.Logger = new LoggerConfiguration()
                .WriteTo.Logger(mainLogger => mainLogger.Filter.ByExcluding(e => e.Properties.ContainsKey("RecurringProcess") || e.Properties.ContainsKey("RecurringProcessDetail"))
                .WriteTo.MSSqlServer(
                    connectionString: connString,
                    sinkOptions: new MSSqlServerSinkOptions { TableName = "HangFireEvents", AutoCreateSqlTable = false}
                 ))

                .WriteTo.Logger(mainLogger => mainLogger.Filter.ByIncludingOnly(e => e.Properties.ContainsKey("RecurringProcess"))
                .WriteTo.MSSqlServer(
                    connectionString: connString,
                    sinkOptions: new MSSqlServerSinkOptions { TableName = "RecurringProcess", AutoCreateSqlTable = false },
                    columnOptions: columnOptionsRecurringProcess
                 ))

                .WriteTo.Logger(mainLogger => mainLogger.Filter.ByIncludingOnly(e => e.Properties.ContainsKey("RecurringProcessDetail"))
                .WriteTo.MSSqlServer(
                    connectionString: connString,
                    sinkOptions: new MSSqlServerSinkOptions { TableName = "RecurringProcessDetail", AutoCreateSqlTable = false },
                    columnOptions: columnOptionsRecurringProcessDetail
                 ))
                .MinimumLevel.Information()
                .CreateLogger();

            #endregion

            
            
            RecurringJobConfigSettings recurringJobConfigSettings = new RecurringJobConfigSettings(builder.Configuration);
            //Schedule Recurring Jobs            
            foreach (RecurringJobProjectBase project in RecurringJobScheduler.GetRecurringJobList())
            {
                project.SetRecurringJobConfigurations(recurringJobConfigSettings);
                RecurringJobOptions rjo  = new RecurringJobOptions();
                rjo.TimeZone = TimeZoneInfo.Local;
               
                Hangfire.RecurringJob.AddOrUpdate(project.GetRecurringJobName(), project.RecurringJobDefaultQueue.GetNonNullValue("default"), project.GetTaskExpression(), project.CronSchedule, rjo);
            }

            //Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //Swagger
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseHangfireDashboard();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
