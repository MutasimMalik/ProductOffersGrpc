using Microsoft.EntityFrameworkCore;
using OfferGrpcService.Data;
using OfferGrpcService.DomainServices;
using OfferGrpcService.Models;
using OfferGrpcService.Repositories;
using OfferGrpcService.Services;

namespace OfferGrpcService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<OfferContext>(options => options.UseInMemoryDatabase("DemoDatabase"));
            builder.Services.AddGrpc();
            builder.Services.AddScoped<IOfferRepository, OfferRepository>();
            builder.Services.AddScoped<IOfferService, OfferService>();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<OfferContext>();

                context.Offer.AddRange(
                    new Offer { Id = 1, ProductName = "Adidas Shoe", OfferDescription = "10% off"},
                    new Offer { Id = 2, ProductName = "Bata Shoe", OfferDescription = "20% off" },
                    new Offer { Id = 3, ProductName = "Apex Shoe", OfferDescription = "30% off" }
                );
                context.SaveChanges();
            }

            // Configure the HTTP request pipeline.

            app.MapGrpcService<ProtoOfferService>();
            app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

            app.Run();
        }
    }
}
