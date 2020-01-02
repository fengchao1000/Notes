using FC.Notes.EFLog;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MySqlConnector.Logging;
using System.Collections.Generic;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.MySQL; 
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.IdentityServer.EntityFrameworkCore;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace FC.Notes.EntityFrameworkCore
{
    [DependsOn(
        typeof(NotesDomainModule),
        typeof(AbpIdentityEntityFrameworkCoreModule),
        typeof(AbpIdentityServerEntityFrameworkCoreModule),
        typeof(AbpPermissionManagementEntityFrameworkCoreModule),
        typeof(AbpSettingManagementEntityFrameworkCoreModule),
        typeof(AbpEntityFrameworkCoreMySQLModule),
        typeof(BackgroundJobsEntityFrameworkCoreModule),
        typeof(AbpAuditLoggingEntityFrameworkCoreModule),
        typeof(AbpTenantManagementEntityFrameworkCoreModule),
        typeof(AbpFeatureManagementEntityFrameworkCoreModule)
        )]
    public class NotesEntityFrameworkCoreModule : AbpModule
    {
        //public static readonly LoggerFactory MyLoggerFactory
        //= new LoggerFactory(  new List<ILoggerProvider> { new EFLoggerProvider() });

        //    public static readonly LoggerFactory MyLoggerFactory
        //= new LoggerFactory(new[] { new Microsoft.Extensions.Logging.Console.ConsoleLoggerProvider((_, __) => true, true) });

        
       public static readonly LoggerFactory MyLoggerFactory
       = new LoggerFactory(new[] { new Microsoft.Extensions.Logging.Debug.DebugLoggerProvider((_, __) => true) });

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<NotesDbContext>(options =>
            {
                /* Remove "includeAllEntities: true" to create
                 * default repositories only for aggregate roots */
                options.AddDefaultRepositories(includeAllEntities: true);
            });

            Configure<AbpDbContextOptions>(options =>
            {
                /* The main point to change your DBMS.
                 * See also NotesMigrationsDbContextFactory for EF Core tooling. */
                options.Configure(context =>
                {
                    context.DbContextOptions.UseLoggerFactory(MyLoggerFactory);
                    //context.DbContextOptions.EnableDetailedErrors();
                    //context.DbContextOptions.EnableSensitiveDataLogging();

                    //...
                });

                options.UseMySQL(); 


            });
        }
    }
}
