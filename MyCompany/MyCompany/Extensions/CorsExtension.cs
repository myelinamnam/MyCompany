namespace MyCompany.Extensions
{
    /// <summary>
    /// Provides extensions for configuring Cross-Origin Resource Sharing (CORS) in the application.
    /// </summary>
    public static class CorsExtension
    {
        /// <summary>
        /// Adds Cross-Origin Resource Sharing (CORS) middleware to the application pipeline with a default policy.
        /// </summary>
        /// <param name="app">The <see cref="IApplicationBuilder"/> to add the CORS middleware to.</param>
        /// <returns>The modified <see cref="IApplicationBuilder"/> to allow chaining of method calls.</returns>
        public static IApplicationBuilder UseCorsWithDefaultPolicy(this IApplicationBuilder app)
        {
            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) // allow any origin
                .AllowCredentials()); // allow credentials
            return app;
        }
    }
}
