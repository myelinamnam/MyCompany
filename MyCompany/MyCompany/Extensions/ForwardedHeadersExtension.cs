using Microsoft.AspNetCore.HttpOverrides;

namespace MyCompany.Extensions
{
    /// <summary>
    /// Provides extensions for configuring forwarded headers in the application.
    /// </summary>
    public static class ForwardedHeadersExtension
    {
        /// <summary>
        /// Adds forwarded headers services to the specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the forwarded headers services to.</param>
        /// <returns>The modified <see cref="IServiceCollection"/> to allow chaining of method calls.</returns>

        public static IServiceCollection AddForwardedHeaders(this IServiceCollection services)
        {
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders =
                    ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });
            return services;
        }
    }
}
