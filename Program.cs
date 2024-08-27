using Microsoft.Extensions.Configuration;
using testCLVD.Services;
using testCLVD.Settings;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Access configuration
        var configuration = builder.Configuration;

        // Add services to the container.
        builder.Services.Configure<AzureStorageSettings>(configuration.GetSection("AzureStorage"));
        builder.Services.AddSingleton<AzureStorageService>();

        // Add services for controllers and views
        builder.Services.AddControllersWithViews();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
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
