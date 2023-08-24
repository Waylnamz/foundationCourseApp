using efcoreApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Options;


        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        builder.Services.AddDbContext<DataContext>(options =>{
            var config = builder.Configuration;
            options.UseNpgsql(config.GetConnectionString("database"));
        });
        // migrationları oluşturuken kurs üzerinden izlediğim 

        // builder.Services.AddDbContext<DataContext>(options =>{
        //             var config = builder.Configuration;
        //             options.UseNpgsql(config.GetConnectionString("database"));
        //         });
        // db bağlama yolu birçok kez hata verdi 
        //         public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
        // {
        //     public DataContext CreateDbContext(string[] args)
        //     {
        //         IConfigurationRoot configuration = new ConfigurationBuilder()
        //             .SetBasePath(Directory.GetCurrentDirectory())
        //             .AddJsonFile("appsettings.json")
        //             .Build();

        //         var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
        //         optionsBuilder.UseSqlServer(configuration.GetConnectionString("YourConnectionString"));

        //         return new DataContext(optionsBuilder.Options);
        //     }
        // }
        // ile migrations oluşturdum, ancak bu metod ile runtime db accsess mümkün değilmiş, bu metod ile migration oluşturduktan sonra tekrar klasik bağlantı oluşturma yoyluna geçtim ve işe yaradı.


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



