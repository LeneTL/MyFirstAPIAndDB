using FirstAPI.data;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Hosting;
using System.Numerics;

namespace FirstAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = "Data Source=app.db";
            builder.Services.AddSingleton(new Character(connectionString));

            // Add services to the container.
            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            var app = builder.Build();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            //---------------- (control * = ta vekk //)


            //curl -X POST "https://localhost:7128/health/heal?id=1&newHealth=100"
            //app.MapPost("/health/heal", (int id, int newHealth, Character player) =>
            //{
            //    var result = player.UpdateHealth(id, newHealth);
            //    return result;
            //});

            //curl -X POST "https://localhost:7128/health/heal?id=1"
            app.MapPost("/health/heal", (int id, Character player) =>
            {
                player.UpdateHealth(id, GetRandomNumber() * 2);
                var result = player.GetHealth(id);
                return result;
            });


            //curl -X POST "https://localhost:7128/health/damage?id=1"
            app.MapPost("/health/damage", (int id, Character player) =>
            {
                player.UpdateHealth(id, -GetRandomNumber());
                var result = player.GetHealth(id);
                return result;

            });

            //curl "https://localhost:7128/health?id=1"
            app.MapGet("/health", (int id, Character player) =>
            {
                var result = player.GetHealth(id);
                return result;
            });

            //curl "https://localhost:7128/name?id=1"
            app.MapGet("/name", (int id, Character player) =>
            {
                var result = player.GetName(id);
                return result;
            });

            app.Run();
        }

        public static int GetRandomNumber()
        {
            Random rand = new Random();
            int result = rand.Next(5,15);
            return result;
        }
    }
}
