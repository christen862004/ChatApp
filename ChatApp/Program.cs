using ChatApp.Hubs;
using ChatApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            
            builder.Services.AddSignalR(); //register need service to hub
            //already built in service need register
            builder.Services.AddDbContext<ITIContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("cs"))
            );
            builder.Services.AddCors(options => {
                options.AddDefaultPolicy(policy => {
                    policy.AllowAnyMethod()
                        .AllowAnyHeader()
                        .SetIsOriginAllowed(url => true)
                        .AllowCredentials();             
                });
            });
            /*
             .SetIsOriginAllowed(url => {
                        if (url == "domain")
                            return true;
                        return false;
                    });
             */

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();
            
            app.UseCors();// allow cross original (servies ) [allow | disallow]

            app.UseRouting();

            app.UseAuthorization();

            //mapping url ==>hub
            app.MapHub<ChatHub>("/MyChat");
           
            app.MapHub<ProductHub>("/ProductHub");

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
