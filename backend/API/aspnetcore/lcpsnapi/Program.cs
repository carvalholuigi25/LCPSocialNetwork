using lcpsnapi.Context;
using lcpsnapi.Interfaces;
using lcpsnapi.Middlewares;
using lcpsnapi.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NuGet.Configuration;
using Serilog;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;

#nullable disable

var builder = WebApplication.CreateBuilder(args);
var logger = new LoggerConfiguration()
  .ReadFrom.Configuration(builder.Configuration)
  .Enrich.FromLogContext()
  .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

builder.Services.AddRazorPages();

builder.Services.AddSqlServer<MyDBContext>(builder.Configuration.GetConnectionString("MyDBConn"));

builder.Services.AddTransient<IUsers, UsersRepository>();
builder.Services.AddTransient<IUsersToken, UsersTokenRepository>();
builder.Services.AddTransient<IPosts, PostsRepository>();
builder.Services.AddTransient<IMedia, MediaRepository>();
builder.Services.AddTransient<IAttachments, AttachmentsRepository>();
builder.Services.AddTransient<IAchievements, AchievementsRepository>();
builder.Services.AddTransient<ITodo, TodoRepository>();
builder.Services.AddTransient<IGames, GamesRepository>();
builder.Services.AddTransient<ITVSeries, TVSeriesRepository>();
builder.Services.AddTransient<IMovies, MoviesRepository>();
builder.Services.AddTransient<IAnimes, AnimesRepository>();
builder.Services.AddTransient<IMangas, MangasRepository>();
builder.Services.AddTransient<IComicBooks, ComicBooksRepository>();

var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]);
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("PolicyGuestOnly", policy => policy.RequireRole("guest"));
    options.AddPolicy("PolicyUserOnly", policy => policy.RequireRole("editor", "user"));
    options.AddPolicy("PolicyModOnly", policy => policy.RequireRole("moderator"));
    options.AddPolicy("PolicyAdminOnly", policy => policy.RequireRole("superadmin", "admin"));
    options.AddPolicy("PolicyAllRoles", policy => policy.RequireRole("superadmin", "admin", "moderator", "editor", "user", "guest"));
});

builder.Services.AddControllers().AddJsonOptions(opts =>
{
    var enumConverter = new JsonStringEnumConverter();
    opts.JsonSerializerOptions.Converters.Add(enumConverter);
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo 
    { 
        Title = "LCP Social Network API", 
        Version = "v1",
        Description = "LCP Social Network API v1",
        TermsOfService = new Uri("https://localhost:7099/terms"),
        Contact = new OpenApiContact
        {
            Name = "Luigi Carvalho",
            Email = "luiscarvalho239@gmail.com",
            Url = new Uri("https://www.instagram.com/luiscarvalhodev96")
        },
        License = new OpenApiLicense
        {
            Name = "LCP SN API License",
            Url = new Uri("https://localhost:7099/license")
        }
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. <br />
        Enter 'Bearer' [space] and then your token in the text input below.
        <br /> Example: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement() {
    {
        new OpenApiSecurityScheme
        {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "api",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

app.UseMiddleware(typeof(ExceptionHandlingMiddleware));
app.UseStaticFiles();

if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "LCP Social Network API v1");
        c.InjectStylesheet("/swagger-ui/custom.css");
    });
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapRazorPages();
app.Run();
