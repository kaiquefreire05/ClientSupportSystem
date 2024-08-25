using ClientSupportSystem.Database;
using ClientSupportSystem.Repositories;
using ClientSupportSystem.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ClientSupportSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Database
            builder.Services.AddEntityFrameworkSqlServer()
                .AddDbContext<ApplicationDBContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("Database")));

            builder.Services.AddScoped<IFeedbackRepository, FeedbackRepository>();
            builder.Services.AddScoped<IKnowledgeRepository, KnowledgeRepository>();
            builder.Services.AddScoped<ITicketRepository, TicketRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();

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
                pattern: "{controller=Login}/{action=IndexLogin}/{id?}");

            app.Run();
        }
    }
}
