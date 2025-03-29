using Company.A1.BLL.Interfaces;
using Company.A1.BLL.Repositories;
using Company.A1.DAL.Data.Contexts;
using Company.A1.PL.Mapping;
using Company.A1.PL.Services;
using Company.G01.BLL;
using Microsoft.EntityFrameworkCore;

namespace Company.A1.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container. 
            builder.Services.AddControllersWithViews(); //Register Built-in  MVC Services
            //builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();  //Allow DI for DepartmentRepository
            //builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>(); //Allow DI for EmployeeRepository
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddDbContext<CompanyDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            }, ServiceLifetime.Scoped);



            //builder.Services.AddAutoMapper(typeof(EmployeeProfile));
            builder.Services.AddAutoMapper(M => M.AddProfile(new EmployeeProfile()));

            // Life Time
             
            //builder.Services.AddScoped();      // Create Object Life Time Per Request UnReachable-Object

            // builder.Services.AddTransient();  // Create Object Life Time Per Operation

            //builder.Services.AddSingleton();  // Create Object Life Timer Per App

            builder.Services.AddScoped<IScopedService, ScopedService>();    //Per Request
            builder.Services.AddTransient<ITransientService, TransientService>(); //Per Operation
            builder.Services.AddSingleton<ISingletonServices, SingletonServices>(); // APP

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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
