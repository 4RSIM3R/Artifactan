using System.Security.Claims;
using System.Text;
using Artifactan.Config;
using Artifactan.Dto;
using Artifactan.Entities;
using Artifactan.Jobs;
using Artifactan.Middlewares;
using Artifactan.Providers;
using Artifactan.Providers.Auth;
using Artifactan.Utils;
using Hangfire;
using Hangfire.Storage.SQLite;
using HangfireBasicAuthenticationFilter;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// add database options
builder.Services.Configure<DatabaseOptions>(builder.Configuration.GetSection("DatabaseOptions"));
builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection("JwtConfig"));
builder.Services.Configure<SendEmailConfig>(builder.Configuration.GetSection("SmtpConfig"));

// authentication

// builder.Services.AddAuthentication(options =>
//     {
//         options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//         options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
//         options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//     })
//     .AddJwtBearer(jwt =>
//     {
//         var key = Encoding.ASCII.GetBytes(builder.Configuration.GetSection("JwtConfig:Secret").Value ?? "Secret123!");
//         jwt.TokenValidationParameters = new TokenValidationParameters()
//         {
//             ValidateIssuerSigningKey = true,
//             IssuerSigningKey = new SymmetricSecurityKey(key),
//             ValidateIssuer = false, // for development
//             ValidateAudience = false, // for development
//             RequireExpirationTime = false, // for development
//             ValidateLifetime = false, // for development
//         };
//     });

// Add services to the container.
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddControllers(options => { options.Filters.Add<FilterExceptionProvider>(); }).AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpContextAccessor();

builder.Services.AddApplicationDbContext();
builder.Services.AddLocalUtils();
builder.Services.AddFluentValidation();

builder.Services.AddHangfire(config => config.UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseSQLiteStorage(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddHangfireServer();

// builder.Services.AddTransient<IArtifactJob, ArtifactJob>();

var app = builder.Build();

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

app.UseMiddleware<JwtMiddleware>();

var options = new DashboardOptions
{
    DashboardTitle = "Job Monitor",
    Authorization = new[]
    {
        new HangfireCustomBasicAuthenticationFilter()
        {
            Pass = "admin",
            User = "admin"
        }
    }
};

app.UseHangfireDashboard("/job", options);
app.MapHangfireDashboard(options);

// RecurringJob.AddOrUpdate<IArtifactJob>("upload_artifact", (x) => x.UploadArtifact(), "* * * * *");

app.Run("http://localhost:4000");