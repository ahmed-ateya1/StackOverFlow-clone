using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StackOverFlowClone.Core.Domain.IdentityEntites;
using StackOverFlowClone.Core.Domain.RepositoryContracts;
using StackOverFlowClone.Core.Services;
using StackOverFlowClone.Core.ServicesContracts;
using StackOverFlowClone.Infrastructure.Data;
using StackOverFlowClone.Infrastructure.Repositories;

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

            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<IAnswerRepository, AnswerRepository>();
            services.AddScoped<IVoteRepository, VoteRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IQuestionServices, QuestionServices>();
            services.AddScoped<IAnswerServices, AnswerServices>();
            services.AddScoped<ICategoryServices, CategoryServices>();
            services.AddScoped<IVoteServices, VoteServices>();
            return services;

        }
    }
}
