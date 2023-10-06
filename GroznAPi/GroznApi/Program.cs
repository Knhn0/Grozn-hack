using System.Reflection;
using System.Text;
using CodePackage.Cfgs;
using CodePackage.Yandex.Storage;
using GroznApi.Middleware;
using Helpers;
using Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Presistence;
using Repository;
using Repository.Abstractions;
using Service;
using Service.Abstactions;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.Configure<JwtIssuerOptions>(builder.Configuration.GetSection(nameof(JwtIssuerOptions)));


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(swagger =>
{
    swagger.OperationFilter<AddResponseHeadersFilter>();

    swagger.SwaggerDoc("v1",
        new OpenApiInfo
        {
            Title = ".NET Core web API + Dapper + Swagger",
            Version = Assembly.GetExecutingAssembly().GetName().Version?.ToString(),
            Contact = new()
            {
                //Email = "", // Don't think this would be a good idea...
                Name = "Bryn Lewis",
                Url = new Uri("https://blog.devMobile.co.nz")
            },
            License = new()
            {
                Name = "Apache License V2.0",
                Url = new Uri("http://www.apache.org/licenses/LICENSE-2.0"),
            }
        });

    swagger.AddSecurityDefinition("Bearer", //Name the security scheme
        new OpenApiSecurityScheme
        {
            Description = "JWT Authorization header using the Bearer scheme.",
            Type = SecuritySchemeType.Http, //We set the scheme type to http since we're using bearer authentication
            Scheme = "bearer", //The name of the HTTP Authorization scheme to be used in the Authorization header. In this case "bearer"
            BearerFormat = "JWT",
        });

    swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new List<string>()
        }
    });
});
builder.Services.AddAuthorization();


var jwtIssuerOptions = builder.Configuration.GetSection(nameof(JwtIssuerOptions)).Get<JwtIssuerOptions>();

var tokenValidationParameters = new TokenValidationParameters
{
    ValidateIssuer = true,
    ValidIssuer = jwtIssuerOptions.Issuer,

    ValidateAudience = true,
    ValidAudience = jwtIssuerOptions.Audience,

    ValidateIssuerSigningKey = true,
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtIssuerOptions.SecretKey)),

    RequireExpirationTime = false,
    ValidateLifetime = true,
    ClockSkew = TimeSpan.Zero
};

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(configureOptions =>
{
    configureOptions.ClaimsIssuer = jwtIssuerOptions.Issuer;
    configureOptions.TokenValidationParameters = tokenValidationParameters;
    configureOptions.SaveToken = true;

    configureOptions.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
            {
                context.Response.Headers.Add("Token-Expired", "true");
            }

            return Task.CompletedTask;
        }
    };
});


builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<IThemeService, ThemeService>();
builder.Services.AddScoped<ILessonService, LessonService>();
builder.Services.AddScoped<ITestService, TestService>();
builder.Services.AddScoped<GptService>();
builder.Services.AddScoped<IAuthorizationService, AuthorizationService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<IAccountService, AccountService>();

builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<IThemeRepository, ThemeRepository>();
builder.Services.AddScoped<ILessonRepository, LessonRepository>();
builder.Services.AddScoped<ITestRepository, TestRepository>();
builder.Services.AddScoped<IAuthorizationRepository, AuthorizationRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<ITestPercentRepository, TestPercentRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<ITeacherRepository, TeacherRepository>();
builder.Services.AddScoped<IAnswerRepository, AnswerRepository>();

builder.Services.AddSingleton<PasswordHasher>();

builder.Services.AddTransient<ExceptionHandlingMiddleware>();

builder.Services.AddDbContext<Context>();

builder.Services.RegisterYandexObjectStorage();
builder.Services.Configure<YandexObjectStorageOptions>(y =>
{
    y.UserKey = "YCNC6whQumS5VAxPblEU6v81FWbCcvU7Xmt0thUg";
    y.UserId = "YCAJEP73J9gjQVZoEgkMUuce3";
    y.ServiceUrl = "https://s3.yandexcloud.net";
    y.Bucket = "grozn-hack";
});

//build
var app = builder.Build();

try
{
    using (var scope = app.Services.CreateScope())
    using (var context = scope.ServiceProvider.GetService<Context>())
        context.Database.Migrate();
}
catch
{
    Console.WriteLine("Пошел нахуй");
}

//swagger
app.UseSwagger();
app.UseSwaggerUI();

//start
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseCors(cors =>
{
    cors.AllowAnyHeader();
    cors.AllowAnyOrigin();
});


app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.MapControllers();
app.Run();

// :3