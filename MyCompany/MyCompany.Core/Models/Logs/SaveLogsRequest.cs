namespace MyCompany.Core.Models
{
    /// <summary>
    /// Represents a request object for saving logs.
    /// </summary>
    public class SaveLogsRequest
    {
        /// <summary>
        /// Gets or sets the endpoint associated with the request.
        /// </summary>
        public string Endpoint { get; set; }

        /// <summary>
        /// Gets or sets the request data.
        /// </summary>
        public string Request { get; set; }

        /// <summary>
        /// Gets or sets the response data.
        /// </summary>
        public string Response { get; set; }
    }

}
