using BLL;
using DAL;

namespace byte_gaming
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
            builder.Services.AddSingleton<IProductRepository, ProductRepository>();
            builder.Services.AddSingleton<IProductService, ProductService>();

            builder.Services.AddSingleton<IContactRequestRepository, ContactRequestRepository>();
            builder.Services.AddSingleton<IContactRequestService, ContactRequestService>();

            builder.Services.AddAutoMapper(typeof(Program));

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowVercel",
                    builder => builder
                        .WithOrigins("https://byte-gaming.vercel.app/")
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("AllowVercel");

            app.UseHttpsRedirection();

            app.UseAuthorization();
    
            app.MapControllers();

            app.Run();
        }
    }
}
