namespace BooksLib.DomainApi.Common
{
    /// <summary>
    /// Static class providing methods for operating on response messages
    /// </summary>
    public static class MessagesProvider
    {
        /// <summary>
        /// Method getting specific response message to given exit code
        /// </summary>
        /// <param name="exitCode">Exit code from ExitCodeEnum</param>
        /// <returns></returns>
        public static string GetMessageForExitCode(ExitCodeEnum exitCode)
        {
            return exitCode switch
            {
                ExitCodeEnum.NoErrors => ResponseMessages.NoErrorsMessage,
                ExitCodeEnum.MappingError => ResponseMessages.MappingErrorMessage,
                ExitCodeEnum.ValidationError => ResponseMessages.ValidationErrorMessage,
                ExitCodeEnum.SerializeDeserializeError => ResponseMessages.ValidationErrorMessage,
                _ => ResponseMessages.UnknownErrorMessage
            };
        }
    }
}
