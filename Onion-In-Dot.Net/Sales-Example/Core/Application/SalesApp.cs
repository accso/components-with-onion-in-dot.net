using Accso.Ecommerce.Onion.Sales.Core.Application.Service;
using Accso.Ecommerce.Onion.Sales.Infrastructure.Messaging;
using Accso.Ecommerce.Onion.Sales.Infrastructure.Persistence;

namespace Accso.Ecommerce.Onion.Sales.Core.Application
{
    public class SalesApp
    {
        public static void Main(String[] args)
        {           
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            //Dependency injection configuration
            builder.Services.AddSingleton<StorageMock>();
            builder.Services.AddTransient<ICatalogRepository, CatalogRepository>();            
            builder.Services.AddTransient<ICatalogService, CatalogService>();
            builder.Services.AddTransient<ISalesMessaging, MessagingProviderMock>();
                      

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();

            app.MapControllers();

            app.Run();

        }
    }
}
