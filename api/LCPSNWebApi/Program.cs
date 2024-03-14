using LCPSNWebApi.Context;
using LCPSNWebApi.Filters;
using LCPSNWebApi.Interfaces;
using LCPSNWebApi.Interfaces.Auth;
using LCPSNWebApi.RewriterRules;
using LCPSNWebApi.Services;
using LCPSNWebApi.Services.Auth;
using LCPSNWebApi.Hubs;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text.Json.Serialization;
using Serilog;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var logger = new LoggerConfiguration()
.ReadFrom.Configuration(builder.Configuration)
.Enrich.FromLogContext()
.CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

if (builder.Configuration["DBMode"]!.Contains("SQLite", StringComparison.InvariantCultureIgnoreCase))
{
    builder.Services.AddDbContext<DBContext, DBContextSQLite>();
}
else if (builder.Configuration["DBMode"]!.Contains("PostgreSQL", StringComparison.InvariantCultureIgnoreCase))
{
    builder.Services.AddDbContext<DBContext, DBContextPostgreSQL>();
}
else if (builder.Configuration["DBMode"]!.Contains("MySQL", StringComparison.InvariantCultureIgnoreCase))
{
    builder.Services.AddDbContext<DBContext, DBContextMySQL>();
}
else
{
    builder.Services.AddDbContext<DBContext>();
}

builder.Services.AddScoped<IUser, UserService>();
builder.Services.AddScoped<IFriend, FriendService>();
builder.Services.AddScoped<IPost, PostService>();
builder.Services.AddScoped<IComment, CommentService>();
builder.Services.AddScoped<IAttachment, AttachmentService>();
builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddAuthentication(opt => {
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options => {
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = Convert.ToBoolean(builder.Configuration["JWTConfig:ValidateAudience"]),
        ValidateAudience = Convert.ToBoolean(builder.Configuration["JWTConfig:ValidateAudience"]),
        ValidateLifetime = Convert.ToBoolean(builder.Configuration["JWTConfig:ValidateLifetime"]),
        ValidateIssuerSigningKey = Convert.ToBoolean(builder.Configuration["JWTConfig:ValidateIssuerSigningKey"]),
        ValidIssuer = builder.Configuration["JWTConfig:ValidIssuer"],
        ValidAudience = builder.Configuration["JWTConfig:ValidAudience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWTConfig:IssuerSigningKey"]!))
    };
});

builder.Services.AddControllersWithViews().AddJsonOptions(options => {
    options.JsonSerializerOptions.WriteIndented = true;
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddRouting(options =>
{
    options.LowercaseUrls = true;
});

builder.Services.AddRazorPages();
builder.Services.AddMvc();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "LCPSocialNetwork API",
        Description = "LCPSocialNetwork API",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Contact",
            Url = new Uri("https://example.com/contact")
        },
        License = new OpenApiLicense
        {
            Name = "License",
            Url = new Uri("https://example.com/license")
        }
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Scheme = "bearer",
        Description = "Please insert JWT token into field"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            new string[] { }
        }
    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    options.SchemaFilter<EnumSchemaFilter>();
    options.ParameterFilter<ParameterFilter>();
    options.UseInlineDefinitionsForEnums();
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsAllowAll", builder =>
    {
        builder.AllowAnyMethod()
               .AllowAnyOrigin()
               .AllowAnyHeader()
               .SetIsOriginAllowed(origin => true);
    });
});

builder.Services.AddSignalR();

builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "application/octet-stream" });
});

var app = builder.Build();

app.UseSwagger(options =>
{
    options.SerializeAsV2 = false;
});

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.EnablePersistAuthorization();
});

if (app.Environment.IsDevelopment())
{
    var isMigServiceScopeEnabled = false;

    if(isMigServiceScopeEnabled)
    {
        using (var serviceScope = app.Services.CreateScope())
        {
            var dbContext = serviceScope.ServiceProvider.GetRequiredService<DBContext>();
            await dbContext.Database.MigrateAsync();
            // or dbContext.Database.EnsureCreatedAsync();
        }
    }

    app.UseWebAssemblyDebugging();
}
else
{
    app.UseHsts();
    app.UseResponseCompression();
}

app.UseRewriter(new RewriteOptions().Add(new RedirectLowerCaseRule()));
app.UseCors("CorsAllowAll");
app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, "Uploads")),
    RequestPath = "/Uploads"
});

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
app.MapControllers();
app.MapHub<ChatHub>("/chathub");
app.MapHub<DataSendHub>("/datasendhub");

app.Run();