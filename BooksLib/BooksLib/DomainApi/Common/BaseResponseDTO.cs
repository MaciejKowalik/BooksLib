namespace BooksLib.DomainApi.Common
{
    /// <summary>
    /// Base class for response objects, returning method's exit code
    /// </summary>
    public class BaseResponseDTO
    {
        public ExitCodeEnum ExitCode { get; set; }

        public BaseResponseDTO()
        {
            ExitCode = ExitCodeEnum.NoErrors;
        }
    }
}
