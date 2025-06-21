using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ReciclaMais.API.Data;
using System.Text;
using System.Text.Json.Serialization;

namespace ReciclaMais.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Controllers + ciclos de referência
            builder.Services.AddControllers()
                .AddJsonOptions(s => s.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            // CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    policy => policy.AllowAnyOrigin()
                                    .AllowAnyMethod()
                                    .AllowAnyHeader());
            });

            // Banco de Dados SQL Server
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Autenticação JWT
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes("ReciclaMaisSuperSecureKey123456!@#$"))
                };
            });

            // Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Tratamento de erros
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/error"); // opcional: crie um endpoint de erro se quiser
            }

            // Swagger (em todos os ambientes)
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ReciclaMaisAPI V1");
                c.RoutePrefix = "swagger";
            });

            // HTTPS
            app.UseHttpsRedirection();

            // CORS
            app.UseCors("AllowAll");

            // Autenticação e autorização
            app.UseAuthentication();
            app.UseAuthorization();

            // Mapear controllers
            app.MapControllers();

            app.Run();
        }
    }
}
