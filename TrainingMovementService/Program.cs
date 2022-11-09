using Repository;
using Repository.Abstract;
using TrainingMovementService.Exception;

namespace TrainingMovementService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddSingleton<ITrainingRepository, TrainingRepository>();
            builder.Services.AddSingleton<IMovementRepository, MovementRepository>();
            builder.Services.AddControllers(opt=>opt.Filters.Add(typeof(ExceptionLogAttribute)));

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}