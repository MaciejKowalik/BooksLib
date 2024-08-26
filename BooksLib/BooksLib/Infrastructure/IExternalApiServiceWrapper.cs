using BooksLib.DomainApi.Common;

namespace BooksLib.Infrastructure
{
    public interface IExternalApiServiceWrapper
    {
        Task<Tuple<ExitCodeEnum, HttpResponseMessage>> GetAsync(string requestUri);
        Task<Tuple<ExitCodeEnum, HttpResponseMessage>> PostAsync(string requestUri, HttpContent content);
    }
}
