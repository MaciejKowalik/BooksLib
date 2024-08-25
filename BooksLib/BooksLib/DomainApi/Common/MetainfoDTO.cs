namespace BooksLib.DomainApi.Common
{
    /// <summary>
    /// Model for storing information about returned results
    /// </summary>
    public class MetainfoDTO
    {
        /// <summary>
        /// Number of results returned to user
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Total count of matching result in request
        /// </summary>
        public int TotalCount { get; set; }
    }
}
