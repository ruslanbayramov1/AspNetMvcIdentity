using AspNetIdentity.Contexts;
using AspNetIdentity.Extensions;
using AspNetIdentity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AspNetIdentity
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // options for database connection, conntring comes from appsettings.Development.json
            string? connStr = builder.Configuration.GetConnectionString("MSSql");
            builder.Services.AddDbContext<AppDbContext>(opt => {
                opt.UseSqlServer(connStr);
            });

            // options can be writed for identity with opt => 
            builder.Services.AddIdentity<User, IdentityRole>(opt => {
                opt.Password.RequiredLength = 8;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
                opt.User.RequireUniqueEmail = true;
                opt.SignIn.RequireConfirmedAccount = false;
                opt.SignIn.RequireConfirmedEmail = false;
                opt.SignIn.RequireConfirmedPhoneNumber = false;
                opt.Lockout.MaxFailedAccessAttempts = 3;
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
            })
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            // 1. if user is not authenticated, it automatically redirects to login path
            // 2. if user is authenticated but still dont have acces to some features, AccessDeniedPath redirects to specified path
            builder.Services.ConfigureApplicationCookie(opt =>
            {
                opt.LoginPath = "/Account/Login";
                opt.AccessDeniedPath = "/Denied";
            });

            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseCustomUserDatas();

            app.MapControllerRoute(
                name: "login",
                pattern: "login", new
                {
                    Controller="Account",
                    Action="Login"
                });

            app.MapControllerRoute(
                name: "register",
                pattern: "register", new {
                    Controller="Account",
                    Action="Register"
                });

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
