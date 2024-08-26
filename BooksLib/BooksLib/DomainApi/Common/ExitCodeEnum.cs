using System.ComponentModel;

namespace BooksLib.DomainApi.Common
{
    /// <summary>
    /// Enumeration class with assigned exit codes to specific behaviour in processing requests
    /// </summary>
    public enum ExitCodeEnum
    {
        [Description("No errors")]
        NoErrors = 0,
        [Description("Validation error")]
        ValidationError = 1,
        [Description("JSON Serialization/deserialization error")]
        SerializeDeserializeError = 2,
        [Description("Mapping error")]
        MappingError = 3,
        [Description("Unknown error")]
        UnknownError = 9,
        [Description("Bad request")]
        BadRequest = 400,
        [Description("Unauthorized")]
        Unauthorized = 401,
        [Description("Access forbidden")]
        Forbidden = 403,
        [Description("Resource not found")]
        NotFound = 404,
        [Description("Conflict with resource")]
        Conflict = 409,
        [Description("Internal server error")]
        InternalError = 500,
        [Description("Invalid response from upstream server")]
        BadGateway = 502,
        [Description("Service unavailable")]
        ServiceUnavailable = 503,
        [Description("Timeout")]
        GatewayTimeout = 504,
    }
}
