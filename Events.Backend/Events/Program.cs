using Events.API.Middlewares;
using Events.Application;
using Events.Application.Attributes;
using Events.Application.Interfaces;
using Events.Application.Use_Cases.EventUseCases;
using Events.Application.Use_Cases.ImageUseCases;
using Events.Application.Use_Cases.ParticipantUseCases;
using Events.Application.Use_Cases.UserUseCases;
using Events.Application.Validation;
using Events.Core.Interfaces.Repositories;
using Events.Events.Data.Email;
using Events.Data;
using Events.Data.Notifications;
using Events.Data.Repositories;
using Events.Data.Security;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Настройка сервисов
builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidateModelAttribute>();
});

// Настройка CORS (по желанию)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// Настройка валидаторов
builder.Services.AddValidatorsFromAssemblyContaining<UserRequestDTOValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<EventRequestDTOValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<ParticipantRequestDTOValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<RegisterParticipantToEventRequestDTOValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<RegisterRequestDTOValidator>();

// Настройка репозиториев
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IParticipantRepository, ParticipantRepository>();
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IEventParticipantRepository, EventParticipantRepository>();
builder.Services.AddScoped<IImageRepository, ImageRepository>();
builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Настройка сервисов использования случаев
builder.Services.AddScoped<GetAllEventsUseCase>();
builder.Services.AddScoped<GetEventByIdUseCase>();
builder.Services.AddScoped<GetEventByNameUseCase>();
builder.Services.AddScoped<AddEventUseCase>();
builder.Services.AddScoped<UpdateEventUseCase>();
builder.Services.AddScoped<DeleteEventUseCase>();
builder.Services.AddScoped<GetEventsByCriteriaUseCase>();
builder.Services.AddScoped<GetPagedEventsUseCase>();
builder.Services.AddScoped<LoginUserUseCase>();
builder.Services.AddScoped<GetParticipantByIdUseCase>();
builder.Services.AddScoped<GetParticipantsByEventIdUseCase>();
builder.Services.AddScoped<RegisterParticipantToEventUseCase>();
builder.Services.AddScoped<RemoveParticipantFromEventUseCase>();
builder.Services.AddScoped<AddImageUseCase>();
builder.Services.AddScoped<GetUserByIdUseCase>();
builder.Services.AddScoped<GetUserByUsernameUseCase>();
builder.Services.AddScoped<RegisterUserUseCase>();
builder.Services.AddScoped<ValidateCredentialsUseCase>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IJwtService, JwtService>();

// Настройка AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Настройка Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Введите JWT токен: Bearer {your token}"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// Настройка базы данных
var connectionString = builder.Configuration["ConnectionStrings:DefaultConnection"];
builder.Services.AddDbContext<EventDbContext>(options =>
    options.UseNpgsql(connectionString));

// Настройка JWT
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
builder.Services.AddScoped<JwtService>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]))
    };
});

builder.WebHost.ConfigureKestrel(options =>
{
    // Настройка Kestrel для работы только с HTTP
    options.ListenAnyIP(80);  // Использовать только порт 80 для HTTP
});

// Настройка авторизации
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
    options.AddPolicy("UserPolicy", policy => policy.RequireRole("User", "Admin"));
});

// Построение приложения
var app = builder.Build();

//// Применение миграций при запуске
//using (var scope = app.Services.CreateScope())
//{
//    var db = scope.ServiceProvider.GetRequiredService<Events.Data.EventDbContext>();
//    db.Database.Migrate();
//}


// Использование CORS (если настроен)
app.UseCors("AllowAll");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();