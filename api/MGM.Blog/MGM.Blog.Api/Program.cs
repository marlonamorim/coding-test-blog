using MGM.Blog.Api;
using MGM.Blog.Api.Auth;
using MGM.Blog.Api.Settings;
using MGM.Blog.AppServices.DependencyInjection;
using MGM.Blog.Domain.DependencyInjection;
using MGM.Blog.Infrastructure.Api.Filters;
using MGM.Blog.Infrastructure.DependencyInjection;
using MGM.Blog.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(opts =>
{
    opts.Conventions.Add(new RouteTokenTransformerConvention(new ToKebabParameterTransformer()));
    opts.Filters.Add<ExceptionFilter>();
});
builder.Services.AddErrorHandlerServices();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.TagActionsBy(api =>
    {
        if (api.GroupName != null)
            return [api.GroupName];

        if (api.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor)
            return [controllerActionDescriptor.ControllerName];

        throw new InvalidOperationException("Unable to determine tag for endpoint.");
    });

    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the bearer scheme. Example: \"Authorization: bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    opt.AddSecurityRequirement(new OpenApiSecurityRequirement {{
        new OpenApiSecurityScheme {
            Reference = new OpenApiReference {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            },
            Scheme = "oauth2",
            Name = "Bearer",
            In = ParameterLocation.Header
        },
        new List<string>()
    }});

    opt.DocInclusionPredicate((name, api) => true);
    opt.EnableAnnotations();
});

builder.Services.AddAuthorization(auth =>
{
    auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
        .RequireAuthenticatedUser().Build());
});

var tokenSettings = new TokenSettings();
new ConfigureFromConfigurationOptions<TokenSettings>(
        builder.Configuration.GetSection("TokenSettings")
    ).Configure(tokenSettings);
builder.Services.AddSingleton(tokenSettings);

SigningSettings signingSettings = new();
builder.Services.AddSingleton(signingSettings);
builder.Services.AddSingleton<IAccessManager, AccessManager>();

builder.Services
.AddAuthentication(auth =>
{
    auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(bo =>
{
    var tokenParams = bo.TokenValidationParameters;
    tokenParams.IssuerSigningKey = signingSettings.Key;
    tokenParams.ValidAudience = tokenSettings.Audience;
    tokenParams.ValidIssuer = tokenSettings.Issuer;
    tokenParams.ValidateIssuerSigningKey = true;
    tokenParams.ValidateLifetime = true;
    tokenParams.ClockSkew = TimeSpan.Zero;
});

builder.Services.AddDbContext<BlogDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseNpgsql(connectionString);
}, ServiceLifetime.Singleton);

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddRepositories(builder.Configuration);
builder.Services.AddDomainServices();
builder.Services.AddAppServices();

builder.Services.AddCors();

var app = builder.Build();
app.UseWebSockets();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseCors(builder => builder
     .AllowAnyOrigin()
     .AllowAnyMethod()
     .AllowAnyHeader());

await app.RunAsync();
