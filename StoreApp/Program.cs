using System.Net;
using System.Text.Json.Serialization;
using StoreApp;
using StoreApp.Core;
using StoreApp.Features.Authentication;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;
using StoreApp.Features.Authentication.Services;
using StoreApp.Features.Notifications;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(
  options =>
  {
    options.Limits.MaxRequestBodySize = int.MaxValue;
    options.Listen(IPAddress.Parse("0.0.0.0"), 8888);
  }
);

builder.Services.AddCors(
  options =>
  {
    options.AddPolicy(
      "AllowAll",
      policy => policy.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
    );
  }
);

builder.Services.AddOpenApi();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddControllers(options => options.Filters.Add<CoreExceptionFilters>())
  .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();

builder.Services.AddSqlite<StoreDbContext>(builder.Configuration.GetConnectionString("DefaultConnection"));
builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));


builder.Services.AddSwaggerGen(
  options =>
  {
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Recipe API", Version = "v1" });

    // Define JWT security scheme
    options.AddSecurityDefinition(
      "Bearer",
      new OpenApiSecurityScheme
      {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\""
      }
    );

    // Add JWT requirement to all endpoints
    options.AddSecurityRequirement(
      new OpenApiSecurityRequirement
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
      }
    );
  }
);


builder.Services.RegisterAuthenticationFeature(builder.Configuration);
builder.Services.RegisterNotificationsFeature();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

var uploadsDir = Path.Combine(builder.Environment.ContentRootPath, "uploads");

if (!Directory.Exists(uploadsDir))
{
  Directory.CreateDirectory(uploadsDir);
}

app.UseStaticFiles(
  new StaticFileOptions
  {
    FileProvider = new PhysicalFileProvider(uploadsDir),
    RequestPath = "/uploads"
  }
);

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();


app.Run();