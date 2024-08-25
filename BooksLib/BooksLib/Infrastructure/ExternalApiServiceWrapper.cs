using BooksLib.DomainApi.Common;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Text.Json.Serialization;

namespace BooksLib.Infrastructure
{
    public class ExternalApiServiceWrapper
    {
        private readonly HttpClient _httpClient;
        private readonly BookLibOptions _options;

        public ExternalApiServiceWrapper(HttpClient httpClient, IOptions<BookLibOptions> options)
        {
            _httpClient = httpClient;
            _options = options.Value;
        }

        public async Task<HttpResponseMessage> GetAsync(string requestUri)
        {
            var response = await _httpClient.GetAsync(requestUri);
            return response;
        }

        public async Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent content)
        {
            var response = await _httpClient.PostAsync(requestUri, content);
            return response;
        }
    }
}
