using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace MyCompany.Api.Extensions
{
    /// <summary>
    /// Provides extensions for configuring MVC (Model-View-Controller) in the application.
    /// </summary>
    public static class MvcExtension
    {
        /// <summary>
        /// Adds MVC (Model-View-Controller) services with authorization to the specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the MVC services with authorization to.</param>
        /// <returns>The modified <see cref="IServiceCollection"/> to allow chaining of method calls.</returns>
        public static IServiceCollection AddMvcWithAuthorization(this IServiceCollection services)
        {
            services.AddMvc(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                                .RequireAuthenticatedUser()
                                .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            }).AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);
            return services;
        }
    }
}
