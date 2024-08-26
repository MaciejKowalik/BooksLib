namespace BooksLib.DomainApi.Common
{
    /// <summary>
    /// Class to store all messages, used in exceptions and responses from methods
    /// </summary>
    public static class ResponseMessages
    {
        public const string NoErrorsMessage = "No errors occured, operation successful";
        public const string ValidationErrorMessage = "Validation errors occured: ";
        public const string MappingErrorMessage = "Error occured while mapping values: ";
        public const string SerializationDeserializationError = "Error occured while serialization/deserialization process of data";
        public const string UnknownErrorMessage = "Unknown error occured during process";
    }
}
