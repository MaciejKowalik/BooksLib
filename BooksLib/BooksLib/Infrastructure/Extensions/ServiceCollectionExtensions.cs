using BooksLib.Domain.Abstraction;
using BooksLib.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BooksLib.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            services.AddHttpClient<ExternalApiServiceWrapper>(client =>
            {
                client.BaseAddress = new Uri("https://exampleuri.com");
            });

            services.AddTransient<ExternalApiServiceWrapper>();
            services.AddTransient<IBookService, BookService>();
            services.AddTransient<IOrderService, OrderService>();

            return services;
        }
    }
}
