using DinkToPdf;
using DinkToPdf.Contracts;
using FET_MVCforTest.Data;
using FET_MVCforTest.Helper;
using FET_MVCforTest.Security;
using FET_MVCforTest.Security.Entities;
using Microsoft.AspNetCore.Identity;


//using FET_MVCforTest.Services;
using Microsoft.EntityFrameworkCore;

namespace FET_MVCforTest
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
			var credentialPath = Path.Combine(Directory.GetCurrentDirectory(), "App_Data", "credentials.json");
			Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", "D:\\All D Files\\route\\backend Asb.net\\FET GraduationProject Test\\FET_MVC\\FET_MVCforTest\\wwwroot\\App_Data\\credentials.json");

			var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("default"));
            });

			builder.Services.AddDbContext<AuthIdentityDbContext>(options =>
			{
				options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnectionString"));
			});
		


			builder.Services.AddIdentity<ApplicationUser, IdentityRole>(option =>
			{
				//option.Password.RequireLowercase = true;
				//option.Password.RequireUppercase = true;
				//option.Password.RequiredLength = 5;
				//option.Password.RequireNonAlphanumeric = true;

				//option.User.RequireUniqueEmail = true;

				//option.Lockout.MaxFailedAccessAttempts = 5;
				//option.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromHours(3);


			})
			  .AddEntityFrameworkStores<AuthIdentityDbContext>()
			  .AddDefaultTokenProviders();


			builder.Services.ConfigureApplicationCookie(option =>
			{
				option.LoginPath = "/Account/SignIn";
				option.AccessDeniedPath = "/home/Error";

			});
			builder.Services.AddScoped<IDataSeeding, DataSeeding>();

			builder.Services.AddAutoMapper(typeof(MappingProfile));
			//	builder.Services.AddSingleton(new GeminiService(builder.Configuration["Gemini:ApiKey"]));


			//
			// Add at the top
		

			// Add to your services
			builder.Services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
			
			var app = builder.Build();

			// identity seed
			using var scope = app.Services.CreateScope();
			var services = scope.ServiceProvider;
			var _DataSeeding = services.GetRequiredService<IDataSeeding>();			
			await _DataSeeding.SeedIdentiyDataAsync();


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
                pattern: "{controller=Home}/{action=Welcome}/{id?}");

            app.Run();
        }
    }
}
