
using AR_WebApi.Interfaces;
using AR_WebApi.Models;
using AR_WebApi.Services;
using AR_WebApi.SignalR;
using Microsoft.EntityFrameworkCore;

namespace AR_WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<LeaderBoardDbContext>(options =>
            {
                options.UseSqlite(builder.Configuration.GetConnectionString("SQLiteConnection"));
                options.UseLazyLoadingProxies();
            });


            builder.Services.AddTransient<UpdateHub>();

            builder.Services.AddTransient<ILeaderBoardService, LeaderBoardService>();

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen();

            builder.Services.AddSignalR();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.MapHub<UpdateHub>("/updates");

            using (var serviceScope = app.Services.CreateScope())
            using (var context = serviceScope.ServiceProvider.GetRequiredService<LeaderBoardDbContext>())
            {
                DBSSetupService.SetupDb(context);
            }

            app.Run();
        }
    }
}
