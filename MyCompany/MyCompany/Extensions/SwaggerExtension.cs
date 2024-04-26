using Microsoft.OpenApi.Models;
using System.Reflection;

namespace MyCompany.Extensions
{
    /// <summary>
    /// Provides extensions for integrating Swagger documentation into the application.
    /// </summary>
    public static class SwaggerExtension
    {
        /// <summary>
        /// Adds Swagger services to the specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <remarks>
        /// Please note that the information provided in Swagger documentation is for reference purposes only and may not always reflect real-time data or system behavior. Users should refer to the actual system implementation for the most accurate information.
        /// </remarks>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the Swagger services to.</param>
        /// <returns>The modified <see cref="IServiceCollection"/> to allow chaining of method calls.</returns>
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "MyCompany API",
                    Description = "MyCompany API Swagger Surface",
                    Contact = new OpenApiContact
                    {
                        Name = "MyCompany Contact",
                        Email = "contactus@mycompany.com",
                        Url = new Uri("https://narrasoft.com/")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "MyCompany License",
                        Url = new Uri("https://narrasoft.com/privacy-policy/#:~:text=We%20are%20the%20sole%20owners,rent%20this%20information%20to%20anyone.")
                    },
                    TermsOfService = new Uri("https://narrasoft.com/how-we-work/"),

                });
                c.AddSecurityDefinition("basic", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "basic",
                    In = ParameterLocation.Header,
                    Description = "Basic Authorization header using the Bearer scheme."
                });
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "basic"
                                }
                            },
                            Array.Empty<string>()
                    }
                });
            });
            return services;
        }


        /// <summary>
        /// Adds Swagger UI middleware to the application pipeline.
        /// </summary>
        /// <param name="app">The <see cref="IApplicationBuilder"/> to add the Swagger UI middleware to.</param>
        /// <returns>The modified <see cref="IApplicationBuilder"/> to allow chaining of method calls.</returns>
        public static IApplicationBuilder UseSwaggerWithUI(this IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BookingServiceApp.Api v1"));
            return app;
        }
    }
}
