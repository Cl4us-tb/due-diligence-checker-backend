using DueDiligenceChecker.Shared.Infrastructure.Persistence.EFC.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

using DueDiligenceChecker.Shared.Domain.Repositories;
using DueDiligenceChecker.Shared.Infrastructure.Persistence.EFC.Repositories;
using DueDiligenceChecker.IAM.Application.OutboundServices;
using DueDiligenceChecker.IAM.Infrastructure.Hashing;
using DueDiligenceChecker.IAM.Infrastructure.Tokens;
using DueDiligenceChecker.IAM.Domain.Repositories;
using DueDiligenceChecker.IAM.Infrastructure.Persistence.EFC.Repositories;
using DueDiligenceChecker.IAM.Application.InboundServices;
using DueDiligenceChecker.IAM.Application.Internal.CommandServices;
using DueDiligenceChecker.IAM.Application.Internal.QueryServices;
using DueDiligenceChecker.Suppliers.Application.InboundServices;
using DueDiligenceChecker.Suppliers.Application.Internal.CommandServices;
using DueDiligenceChecker.Suppliers.Application.Internal.QueryServices;
using DueDiligenceChecker.Suppliers.Domain.Repositories;
using DueDiligenceChecker.Suppliers.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

using DueDiligenceChecker.Shared.Infrastructure.Swagger;
using DueDiligenceChecker.Shared.Infrastructure.Errors;
using DueDiligenceChecker.Screening.Application.InboundServices;
using DueDiligenceChecker.Screening.Application.Internal.QueryServices;
using DueDiligenceChecker.Screening.Application.OutboundServices;
using DueDiligenceChecker.Screening.Infrastructure.Scraping;
using DueDiligenceChecker.Screening.Interfaces.ACL;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(connectionString)
    .UseSnakeCaseNamingConvention();
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var configuredOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>();
if (configuredOrigins == null || configuredOrigins.Length == 0)
{
    var rawOrigins = builder.Configuration["Cors:AllowedOrigins"];
    configuredOrigins = string.IsNullOrWhiteSpace(rawOrigins)
        ? ["http://localhost:5173"]
        : rawOrigins
            .Split([',', ';'], StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Where(o => !string.IsNullOrWhiteSpace(o))
            .ToArray();
}

builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend", policy =>
    {
        policy.WithOrigins(configuredOrigins)
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var globalPermitLimit = builder.Configuration.GetValue<int?>("RateLimiting:Global:PermitLimit") ?? 20;
var globalWindowSeconds = builder.Configuration.GetValue<int?>("RateLimiting:Global:WindowSeconds") ?? 60;

builder.Services.AddRateLimiter(options =>
{
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(context =>
    {
        if (HttpMethods.IsOptions(context.Request.Method))
            return RateLimitPartition.GetNoLimiter("preflight");

        var ip = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";

        return RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: ip,
            factory: _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = globalPermitLimit,
                Window = TimeSpan.FromSeconds(globalWindowSeconds),
                QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                QueueLimit = 0
            });
    });
});

builder.Services.AddHttpClient<ISecopScraper, SecopScraper>();
builder.Services.AddScoped<IInterpolScraper, InterpolScraper>();
builder.Services.AddScoped<ISmvScraper, SmvScraper>();

builder.Services.AddScoped<ISecopQueryService, SecopQueryService>();
builder.Services.AddScoped<IInterpolQueryService, InterpolQueryService>();
builder.Services.AddScoped<ISmvQueryService, SmvQueryService>();

builder.Services.AddScoped<IScreeningContextFacade, ScreeningContextFacade>();


var jwtSecret = builder.Configuration["JwtSettings:Secret"];
if (string.IsNullOrEmpty(jwtSecret))
{
    throw new InvalidOperationException("JwtSettings:Secret no está configurado.");
}

var key = Encoding.ASCII.GetBytes(jwtSecret);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "DueDiligenceChecker API",
        Version = "v1",
        Description = "API Base con arquitectura DDD y Bounded Contexts"
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "Ingrese 'Bearer {token}' o solo el token en el campo."
        });
        
    options.OperationFilter<AuthorizeCheckOperationFilter>();
});


builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IHashingService, BcryptHashingService>();
builder.Services.AddScoped<ITokenService, JwtTokenService>();


builder.Services.AddScoped<IUserRepository, UserRepository>();


builder.Services.AddScoped<IUserCommandService, UserCommandService>();
builder.Services.AddScoped<IUserQueryService, UserQueryService>();

builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();
builder.Services.AddScoped<ISupplierScreeningRepository, SupplierScreeningRepository>();
builder.Services.AddScoped<ISupplierCommandService, SupplierCommandService>();
builder.Services.AddScoped<ISupplierQueryService, SupplierQueryService>();
builder.Services.AddScoped<ISupplierScreeningCommandService, SupplierScreeningCommandService>();
builder.Services.AddScoped<ISupplierScreeningQueryService, SupplierScreeningQueryService>();


var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    context.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("Frontend");
app.UseRateLimiter();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
