using Company.A1.BLL.Interfaces;
using Company.A1.BLL.Repositories;
using Company.A1.DAL.Data.Contexts;
using Company.A1.DAL.Models;
using Company.A1.PL.Authentication;
using Company.A1.PL.Mapping;
using Company.A1.PL.Services;
using Company.A1.PL.Settings;
using Company.G01.BLL;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MailSettings = Company.A1.PL.Authentication.MailSettings;
using TwilioSettings = Company.A1.PL.Authentication.TwilioSettings;

namespace Company.A1.PL
{

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            //builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>(); // Allows DI for DepartmentRepository
            //builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>(); // Allows DI for DepartmentRepository

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddDbContext<CompanyDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
            }); // Allows DI for DbContext
            builder.Services.AddScoped<IScopedService, ScopedService>(); // per request
            builder.Services.AddScoped<ITransientService, TransientService>(); // per operation
            builder.Services.AddScoped<ISingletonServices, SingletonServices>(); // per application
            builder.Services.AddScoped<IMailService, MailService>();
            builder.Services.AddScoped<ITwilioService, TwilioService>();

            builder.Services.AddAutoMapper(m => m.AddProfile(new EmployeeProfile()));
            builder.Services.AddAutoMapper(m => m.AddProfile(new DepartmentProfile()));
            builder.Services.AddIdentity<AppUser, IdentityRole>()
                            .AddEntityFrameworkStores<CompanyDbContext>()
                            .AddDefaultTokenProviders();
            builder.Services.ConfigureApplicationCookie(
                config => {
                    config.LoginPath = "/Account/SignIn";
                });

            builder.Services.Configure<MailSettings>(builder.Configuration.GetSection(nameof(MailSettings)));
            builder.Services.Configure<TwilioSettings>(builder.Configuration.GetSection(nameof(TwilioSettings)));
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
              .AddCookie()
                                         .AddGoogle(o =>
                                         {
                                             o.ClientId = builder.Configuration["Authentication:Google:ClientId"];
                                             o.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
                                             o.CallbackPath = "/signin-google"; 
                                         })
                                         .AddFacebook(facebookOptions =>
                                         {
                                             facebookOptions.AppId = builder.Configuration["Authentication:Facebook:AppId"];
                                             facebookOptions.AppSecret = builder.Configuration["Authentication:Facebook:AppSecret"];
                                             facebookOptions.CallbackPath = "/signin-facebook";
                                         });

            // Dependency Injection: Allow clr to create objects of this class instead of the class itself handles it
            // Services LifeTimes

            //builder.Services.AddScoped(); // create object life time per request - Unreachable object after the request -- best for repositories
            //builder.Services.AddTransient(); // create object life time per operation - every use creates an object
            //builder.Services.AddSingleton(); // create object life time per app

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");


            app.Run();
        }
    }
}