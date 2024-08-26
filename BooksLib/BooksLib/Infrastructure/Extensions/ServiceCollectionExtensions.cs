using BooksLib.Domain.Abstraction;
using BooksLib.Domain.Services;
using BooksLib.DomainApi.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace BooksLib.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<BookLibOptions>(configuration.GetSection(Constants.ConfigurationOptionsString));

            services.AddHttpClient<ExternalApiServiceWrapper>(client =>
            {
                var options = configuration.GetSection(Constants.ConfigurationOptionsString).Get<BookLibOptions>();
                client.BaseAddress = new Uri(options.BaseUrl);
            });

            services.AddTransient<ExternalApiServiceWrapper>(provider =>
            {
                var client = provider.GetRequiredService<HttpClient>();
                var options = provider.GetRequiredService<IOptions<BookLibOptions>>();
                return new ExternalApiServiceWrapper(client, options);
            });

            services.AddTransient<IBookService, BookService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IExternalApiServiceWrapper, ExternalApiServiceWrapper>();

            return services;
        }
    }
}
