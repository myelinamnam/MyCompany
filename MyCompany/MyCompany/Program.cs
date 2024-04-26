using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.HttpOverrides;
using MyCompany.Extensions;
using MyCompany.Infrastructure;
using MyCompany.Api.Handlers;
using MyCompany.Api.Services;
using MyCompany.Api.Extensions;
using Microsoft.AspNetCore.Diagnostics;
using MyCompany.Handler.SqlInterface;
using MyCompany.Handler.SqlHelper;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddForwardedHeaders();
builder.Services.AddHttpContextAccessor();
builder.Services.AddCors();
builder.Services.AddControllers();
builder.Services.AddAuthentication("BasicAuthentication")
        .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);
builder.Services.AddScoped<IApiCredentialService, ApiCredentialService>();
builder.Services.AddScoped<ISqlHelperRepository, SQLHelper>();
builder.Services.AddScoped<IDapperConnectionRepository, AppDatabaseConnection>();
builder.Services.AddMvcWithAuthorization();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddInfrastructure();

//======================================================================
// Basic Authentication
//======================================================================
builder.Services.AddSwagger();

var app = builder.Build();

app.UseSwaggerWithUI();
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

app.UseHttpsRedirection();
app.UseRouting();
app.UseCorsWithDefaultPolicy();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ExceptionHandleMiddleware>();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();