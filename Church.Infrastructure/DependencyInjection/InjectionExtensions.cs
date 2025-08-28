using Church.Domain.Configurations;
using Church.Infrastructure.Contracts.Repositories;
using Church.Infrastructure.Contracts.Services;
using Church.Infrastructure.Data;
using Church.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Church.Infrastructure.DependencyInjection
{
    public static class InjectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .AddAutoMapper(p => p.AddProfile<MapperProfile>())
                .Configure<EmailOptions>(configuration.GetSection("EmailOptions").Bind)
                .AddScoped<IEmailService, EmailService>()
                .AddScoped<IUserDataService, UserDataService>();
        }

        public static IServiceCollection AddDataLayer(this IServiceCollection services, IConfiguration configuration, string assemblyName)
        {
            // Add services to the container.
            var connectionString = configuration.GetConnectionString("UserDatabase") ?? throw new InvalidOperationException("Connection string 'UserDatabase' not found.");

            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlite(connectionString, options => options.MigrationsAssembly(assemblyName)));

            services.AddDefaultIdentity<IdentityUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
                options.User.RequireUniqueEmail = true;
                options.Password.RequireUppercase = false;
            })
            .AddEntityFrameworkStores<ApplicationContext>();

            services.AddScoped<IUserDataRepository, UserDataRepository>();

            return services;
        }
    }
}
