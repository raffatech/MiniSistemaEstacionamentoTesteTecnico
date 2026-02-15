using Microsoft.EntityFrameworkCore;
using MiniSistemaEstacionamentoAPI.Data;
using MiniSistemaEstacionamentoAPI.Models;
using MiniSistemaEstacionamentoAPI.Services;

namespace MiniSistemaEstacionamentoAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            // 1. Configurações Padrão
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // 2. Configuração do Banco de Dados
            string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<ParkingDbContext>(options =>
                options.UseSqlite(connectionString));

            // 3. Configuração de CORS (Essencial para o Angular)
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            // 4. INJEÇÃO DE DEPENDÊNCIA
            builder.Services.AddScoped<IVehicleInterface, VehicleService>();
            builder.Services.AddScoped<IParkingInterface, ParkingService>();
            builder.Services.AddScoped<ITaxServiceRepository, BrazilTaxService>();

            WebApplication app = builder.Build();

            // 5. Middlewares
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            // Ativando o CORS antes da autorização e mapeamento
            app.UseCors("CorsPolicy");

            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}