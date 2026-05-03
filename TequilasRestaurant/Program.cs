using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TequilasRestaurant.Data;
using TequilasRestaurant.Models;
using TequilasRestaurant.Models.DbModels;
using TequilasRestaurant.Models.Services;

namespace TequilasRestaurant
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
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddControllersWithViews();

            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            builder.Services.AddScoped(typeof(IImageService), typeof(ImageService));
            builder.Services.AddScoped<IngredientService>();
            builder.Services.AddScoped<ProductService>();
            builder.Services.AddScoped<CategoryService>();
            builder.Services.AddScoped<ImageService>();
            builder.Services.AddScoped<OrderService>();

            builder.Services.AddMemoryCache();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSession();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Ingredient}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}
