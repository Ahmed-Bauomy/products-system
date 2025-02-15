using ProductSystem.Infrastructure;
using ProductSystem.Application;
using ProductSystem.Adapters;
namespace ProductSystem.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddInfrastructureServices(builder.Configuration);
            builder.Services.AddApplicationServices();
            builder.Services.AddAdapterServices();

            var policy = "allowAll";
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: policy, config =>
                {
                    config.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors(policy);
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
