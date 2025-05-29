
using BackendToDoList.Infra;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BackendToDoList.Application.Repositories.TarefaRepositories;
using BackendToDoList.Application.Repositories.ItemTarefaRepositories;

namespace BackendToDoList.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<ITarefaRepository, TarefaRepository>();
            builder.Services.AddScoped<IITemTarefaRepository,  ItemTarefaRepository>();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyHeader()
                               .AllowAnyMethod();
                    });
            });

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    var config = builder.Configuration;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = config["Jwt:Issuer"],
                        ValidAudience = config["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]))
                    };
                });

            builder.Services.AddAuthorization();

            var connectionString = builder.Configuration.GetConnectionString("Default");
            builder.Services.AddDbContext<SistemaToDoListDbContext>(options =>
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
               .EnableSensitiveDataLogging()
               .EnableDetailedErrors()
               .LogTo(Console.WriteLine, LogLevel.Information));

            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI();

            //app.UseHttpsRedirection();
            app.UseCors("AllowAll");
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
