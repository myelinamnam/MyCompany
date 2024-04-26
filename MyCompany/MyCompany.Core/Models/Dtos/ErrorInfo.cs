using MyCompany.Core.Enums;
using System.Text.RegularExpressions;

namespace MyCompany.Core.Models
{

    /// <summary>
    /// The error info.
    /// </summary>
    public class ErrorInfo
    {
        private string _errorTitle;

        /// <summary>
        /// Gets or sets the error code.
        /// </summary>
        /// <value>
        /// The error code.
        /// </value>
        public ErrorTypes ErrorCode { get; set; } = ErrorTypes.OK;

        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        /// <value>
        /// The error message.
        /// </value>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Gets pretty error title.
        /// </summary>
        public string ErrorTitle
        {
            get
            {
                if (string.IsNullOrEmpty(_errorTitle))
                {
                    var messageArr = Regex.Split(ErrorCode.ToString().Substring(1), @"(?<!^)(?=[A-Z])");
                    _errorTitle = ErrorCode.ToString().ToUpper().First() + string.Join(" ", messageArr).ToLower();
                }
                return _errorTitle;
            }

            set
            {
                _errorTitle = value;
            }
        }
    }
}
