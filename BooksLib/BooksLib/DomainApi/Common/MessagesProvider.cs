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
                ExitCodeEnum.BadRequest => ResponseMessages.BadRequestErrorMessage,
                ExitCodeEnum.Unauthorized => ResponseMessages.UnauthorizedErrorMessage,
                ExitCodeEnum.Forbidden => ResponseMessages.ForbiddenErrorMessage,
                ExitCodeEnum.NotFound => ResponseMessages.NotFoundErrorMessage,
                ExitCodeEnum.Conflict => ResponseMessages.ConflictErrorMessage,
                ExitCodeEnum.InternalError => ResponseMessages.InternalErrorMessage,
                ExitCodeEnum.BadGateway => ResponseMessages.BadGatewayErrorMessage,
                ExitCodeEnum.ServiceUnavailable => ResponseMessages.ServiceUnavailableErrorMessage,
                ExitCodeEnum.GatewayTimeout => ResponseMessages.TimeoutErrorMessage,
                ExitCodeEnum.UnknownError => ResponseMessages.UnknownErrorMessage,
                _ => ResponseMessages.UnknownErrorMessage
            };
        }
    }
}
