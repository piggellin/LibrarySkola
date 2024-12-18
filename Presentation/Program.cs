using Application;
using Application.Interfaces;
using FluentValidation;
using Infrastructure;
using Infrastructure.Repositories;
namespace Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the cotainer.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddApplication();
            builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
            builder.Services.AddScoped<IBookRepository, BookRepository>();
            builder.Services.AddInfrastructure(builder.Configuration.GetSection("ConnectionStrings").Value!);
            builder.Services.AddValidatorsFromAssembly(typeof(CreateAuthorCommandValidator).Assembly);


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseHttpsRedirection();


            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
