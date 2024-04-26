namespace MyCompany.Core.Enums
{
    /// <summary>
    /// The error type.
    /// </summary>
    public enum ErrorTypes
    {
        /// <summary>
        /// The ok
        /// </summary>
        OK = 0,

        #region range generic validator

        /// <summary>
        /// The unhandled error
        /// </summary>
        UnhandledError = 1,

        /// <summary>
        /// The unauthorized error
        /// </summary>
        UnauthorizedError = 2,

        /// <summary>
        /// The invalid identifier
        /// </summary>
        InvalidIdentifier = 3,

        /// <summary>
        /// The identifier mismatch
        /// </summary>
        IdentifierMismatch = 4,

        #endregion
    }
}
