using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StackOverFlowClone.Core.Domain.IdentityEntites;
using StackOverFlowClone.Infrastructure.Data;

namespace StackOverFlowClone.UI.StartupExtensions
{
    public static class ConfigureServiceExtension
    {
        public static IServiceCollection ServiceConfiguration(this IServiceCollection services , IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("connstr"));
            });

            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders()
                .AddUserStore<UserStore<ApplicationUser, ApplicationRole, AppDbContext, Guid>>()
                .AddRoleStore<RoleStore<ApplicationRole, AppDbContext, Guid>>();

            return services;

        }
    }
}
