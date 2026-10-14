using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using tareaSem9.Adapters.Outbound.Persistence;
using tareaSem9.Adapters.Outbound.Security;
using tareaSem9.Core.Application.UseCases;
using tareaSem9.Core.Ports.Inbound;
using tareaSem9.Core.Ports.Outbound;

var builder = WebApplication.CreateBuilder(args);

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// 1. Adaptador de Salida de Persistencia: PostgreSQL con Entity Framework Core
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection")));

// 2. Registro de Puertos de Salida (Driven / Outbound Ports -> Driven Adapters)
builder.Services.AddScoped<IUnitOfWorkPort, UnitOfWorkAdapter>();
builder.Services.AddScoped<ITokenServicePort, JwtTokenServiceAdapter>();
builder.Services.AddScoped<IPasswordHasherPort, Sha256PasswordHasherAdapter>();

// 3. Registro de Puertos de Entrada (Driving / Inbound Ports -> Core Use Cases)
builder.Services.AddScoped<IAuthUseCase, AuthUseCase>();
builder.Services.AddScoped<IEstudianteUseCase, EstudianteUseCase>();
builder.Services.AddScoped<ICursoUseCase, CursoUseCase>();
builder.Services.AddScoped<IMatriculaUseCase, MatriculaUseCase>();
builder.Services.AddScoped<IEvaluacioneUseCase, EvaluacioneUseCase>();
builder.Services.AddScoped<IAsistenciaUseCase, AsistenciaUseCase>();
builder.Services.AddScoped<IMateriaUseCase, MateriaUseCase>();
builder.Services.AddScoped<IProfesoreUseCase, ProfesoreUseCase>();

// 4. Configurar Autenticación JWT
var secretKey = builder.Configuration["Jwt:SecretKey"] ?? "SuperSecretKeyForJwtTokenAuth123456!";
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"] ?? "tareaSem9",
            ValidAudience = builder.Configuration["Jwt:Audience"] ?? "tareaSem9",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
        };
    });

// 5. Configurar Autorización y Políticas
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Administrador", policy => policy.RequireRole("Admin"));
});

// 6. Adaptadores de Entrada (Controladores API REST) con manejo de ciclos JSON
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });

// 7. Configuración de Swagger / OpenAPI con soporte para Token Bearer JWT
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var securityScheme = new Microsoft.OpenApi.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.ParameterLocation.Header,
        Description = "Ingresa tu token JWT obtenido del login en formato Bearer"
    };

    c.AddSecurityDefinition("Bearer", securityScheme);
    c.AddSecurityRequirement(doc => new Microsoft.OpenApi.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.OpenApiSecuritySchemeReference("Bearer", doc),
            new List<string>()
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
