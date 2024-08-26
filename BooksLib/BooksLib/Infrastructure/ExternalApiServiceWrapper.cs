using BooksLib.DomainApi.Common;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;

namespace BooksLib.Infrastructure
{
    /// <summary>
    /// Service wrapper implementing methods for HTTP requests, set authorization data, handle errors
    /// </summary>
    public class ExternalApiServiceWrapper : IExternalApiServiceWrapper
    {
        private readonly HttpClient _httpClient;
        private readonly BookLibOptions _options;

        public ExternalApiServiceWrapper(HttpClient httpClient, IOptions<BookLibOptions> options)
        {
            _httpClient = httpClient;
            _options = options.Value;
        }

        /// <summary>
        /// Method for getting data with HTTP request
        /// </summary>
        /// <param name="requestUri">External API endpoint hit</param>
        /// <returns>Exit code with http response</returns>
        public async Task<Tuple<ExitCodeEnum, HttpResponseMessage>> GetAsync(string requestUri)
        {
            SetBearerToken();
            var response = await _httpClient.GetAsync(requestUri);
            var exitCode = HandleErrors(response);
            return Tuple.Create(exitCode, response);
        }

        /// <summary>
        /// Method for adding data with HTTP request
        /// </summary>
        /// <param name="requestUri">External API endpoint git</param>
        /// <param name="content">Http request content</param>
        /// <returns>Exit code with http response</returns>
        public async Task<Tuple<ExitCodeEnum, HttpResponseMessage>> PostAsync(string requestUri, HttpContent content)
        {
            SetBearerToken();
            var response = await _httpClient.PostAsync(requestUri, content);
            var exitCode = HandleErrors(response);
            return Tuple.Create(exitCode, response);
        }

        /// <summary>
        /// Private method for setting the authorization token in header of HTTP requests
        /// </summary>
        private void SetBearerToken()
        {
            if (!string.IsNullOrEmpty(_options.BearerToken))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.BearerString, _options.BearerToken);
            }
        }

        /// <summary>
        /// Private method handling error responses
        /// </summary>
        /// <param name="response">HTTP response message</param>
        /// <returns>Exit code</returns>
        private ExitCodeEnum HandleErrors(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                var statusCode = (int)response.StatusCode;
                MapStatusCodeToExitCode(statusCode);
            }

            return ExitCodeEnum.NoErrors;
        }

        /// <summary>
        /// Private method for mapping HTTP status codes to project internal exit codes
        /// </summary>
        /// <param name="statusCode">HTTP status code</param>
        /// <returns>Exit code</returns>
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
