namespace MyCompany.Core.Models
{
    /// <summary>
    /// Bad request exception
    /// </summary>
    public class BadRequestException : BaseException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BadRequestException"/> class.
        /// </summary>
        /// <param name="errorInfo">The error info.</param>
        public BadRequestException(ErrorInfo errorInfo)
            : base(errorInfo)
        {
        }
    }
}
