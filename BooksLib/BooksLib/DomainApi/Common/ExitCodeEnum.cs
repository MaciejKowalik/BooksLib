using System.ComponentModel;

namespace BooksLib.DomainApi.Common
{
    public enum ExitCodeEnum
    {
        [Description("No errors")]
        NoErrors = 0,
        [Description("Http error")]
        HttpError = 500

    }
}
