namespace BooksLib.DomainApi.Common
{
    public class BookLibOptions
    {
        /// <summary>
        /// External API base URL
        /// </summary>
        public string BaseUrl { get; set; }

        /// <summary>
        /// Authorization Bearer token
        /// </summary>
        public string BearerToken { get; set; }

        /// <summary>
        /// Expiration time in minutes for orders' cache
        /// </summary>
        public int OrdersCacheExpirationTime { get; set; }
    }
}
