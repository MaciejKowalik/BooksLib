namespace BooksLib.DomainApi.Common
{
    public class MyApiOptions
    {
        /// <summary>
        /// External API base URL
        /// </summary>
        public string BaseUrl { get; set; }

        /// <summary>
        /// Authorization Bearer token
        /// </summary>
        public string BearerToken { get; set; }
    }
}
