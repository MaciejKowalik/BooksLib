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

        public async Task<Tuple<ExitCodeEnum, HttpResponseMessage>> GetAsync(string requestUri)
        {
            SetBearerToken();
            var response = await _httpClient.GetAsync(requestUri);
            var exitCode = HandleErrors(response);
            return Tuple.Create(exitCode, response);
        }

        public async Task<Tuple<ExitCodeEnum, HttpResponseMessage>> PostAsync(string requestUri, HttpContent content)
        {
            SetBearerToken();
            var response = await _httpClient.PostAsync(requestUri, content);
            var exitCode = HandleErrors(response);
            return Tuple.Create(exitCode, response);
        }

        private void SetBearerToken()
        {
            if (!string.IsNullOrEmpty(_options.BearerToken))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.BearerString, _options.BearerToken);
            }
        }

        private ExitCodeEnum HandleErrors(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                var statusCode = (int)response.StatusCode;
                MapStatusCodeToExitCode(statusCode);
            }

            return ExitCodeEnum.NoErrors;
        }

        private ExitCodeEnum MapStatusCodeToExitCode(int statusCode)
        {
            return statusCode switch
            {
                200 => ExitCodeEnum.NoErrors,
                201 => ExitCodeEnum.NoErrors,
                204 => ExitCodeEnum.NoErrors,
                400 => ExitCodeEnum.BadRequest,
                401 => ExitCodeEnum.Unauthorized,
                403 => ExitCodeEnum.Forbidden,
                404 => ExitCodeEnum.NotFound,
                409 => ExitCodeEnum.Conflict,
                500 => ExitCodeEnum.InternalError,
                502 => ExitCodeEnum.BadGateway,
                503 => ExitCodeEnum.ServiceUnavailable,
                504 => ExitCodeEnum.GatewayTimeout,
                _ => ExitCodeEnum.UnknownError
            };
        }
    }
}
