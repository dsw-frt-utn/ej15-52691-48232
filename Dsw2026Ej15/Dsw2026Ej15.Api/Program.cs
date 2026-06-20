
namespace Dsw2026Ej15.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            //para el pto h.
            builder.Services.AddHealthChecks();
            //para el singleton
            builder.Services.AddSingleton<Dsw2026Ej15.Domain.Interfaces.IPersistence, Dsw2026Ej15.Data.PersistenceInMemory>();
            var app = builder.Build();
            //para poner el filtro intermedio en mi sistema
            app.UseMiddleware<Dsw2026Ej15.Api.Middleware.ExceptionHandlingMiddleware>();
            // para crear la ruta pedida en el pto h
            app.MapHealthChecks("/health-check"); 

            // Swagger
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
