using Esfamilo_Core.Ml;
using Esfamilo_Data.Context;
using Esfamilo_IOC;
using Esfamilo_Web.Data;
using Esfamilo_Web.Hubs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Esfamilo_Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDbContext<EsfamiloDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.Configure<IdentityOptions>(options =>
            {
                // Default Lockout settings.
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            });
            builder.Services.AddDependencyInjection();
            builder.Services.AddPredictionWordEnginePool();
            builder.Services.AddSingleton<WordPredictML>();
            builder.Services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");
            builder.Services.AddRazorPages();
            builder.Services.AddSignalR();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.MapRazorPages();
            app.MapHub<ManageLobbyHub>("/ManageLobbyHub");
            app.MapHub<LobbyHub>("/LobbyHub");
            app.MapHub<GameHub>("/GameHub");

            app.Run();
        }
    }
}