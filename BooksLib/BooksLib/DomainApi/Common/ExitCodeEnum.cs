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
        [Description("Http error")]
        HttpError = 500

    }
}
