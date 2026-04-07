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

var builder = WebApplication.CreateBuilder(args);

// ConfiguraciÃ³n de Base de Datos SQL Server
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(connectionString)
    .UseSnakeCaseNamingConvention();
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Configurar autenticaciÃ³n JWT (provisional)
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
builder.Services.AddScoped<ISupplierCommandService, SupplierCommandService>();
builder.Services.AddScoped<ISupplierQueryService, SupplierQueryService>();


var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

// 4. Asegurar que la base de datos se cree/actualice al iniciar
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    context.Database.Migrate();
}

// 5. Configurar el pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

// UN SOLO app.Run() y nada mÃ¡s despuÃ©s de esto
app.Run();


