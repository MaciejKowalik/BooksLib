using AutoMapper.Internal;

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
        public const string BadRequestErrorMessage = "The server could not understand the request due to a syntax error";
        public const string UnauthorizedErrorMessage = "Authorization token is missing or invalid";
        public const string ForbiddenErrorMessage = "No permission to access this resource";
        public const string NotFoundErrorMessage = "The requested resource could not be found on the server";
        public const string ConflictErrorMessage = "The request could not be completed due to a conflict with the current state of the resource";
        public const string InternalErrorMessage = "Internal server error";
        public const string BadGatewayErrorMessage = "The server received an invalid response from the upstream server";
        public const string ServiceUnavailableErrorMessage = "The service is currently unavailable (e.g. overload/maintenance)";
        public const string TimeoutErrorMessage = "The server did not receice a timely response from the upstream server";
        public const string UnknownErrorMessage = "Unknown error occured during process";
    }
}
