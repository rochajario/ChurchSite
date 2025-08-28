using Church.Infrastructure.DependencyInjection;

namespace Church.Web
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services
                .AddServices(builder.Configuration)
                .AddDataLayer(
                    builder.Configuration,
                    typeof(Program).Assembly.FullName!)
                .AddDatabaseDeveloperPageExceptionFilter()
                .AddRazorPages();

            var application = builder.Build();

            if (application.Environment.IsDevelopment())
            {
                application.UseMigrationsEndPoint();
            }
            else
            {
                application
                    .UseExceptionHandler("/Error")
                    .UseHsts();
            }

            application
                .UseHttpsRedirection()
                .UseStaticFiles()
                .UseRouting()
                .UseAuthentication()
                .UseAuthorization();

            application.MapRazorPages();
            application.Run();
        }
    }
}
