using coreworking_space_booking_backend.Data;
using coreworking_space_booking_backend.Helpers;
using coreworking_space_booking_backend.Helpers.Logger;
using coreworking_space_booking_backend.Services.ProductService;
using coreworking_space_booking_backend.Services.BookingService;
using coreworking_space_booking_backend.Services.CardDetailsService;
using coreworking_space_booking_backend.Services.FacilityService;
using coreworking_space_booking_backend.Services.PermissionService;
using coreworking_space_booking_backend.Services.TestService;
using coreworking_space_booking_backend.Services.UserService;
using coreworking_space_booking_backend.Services.RoleService;
using coreworking_space_booking_backend.Services.AdminService;
using coreworking_space_booking_backend.Services.LocationService;
using coreworking_space_booking_backend.Services.PaymentService;
using coreworking_space_booking_backend.Services.PricingService;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using PayMedia.DotNet.Utils.Logger.Configs;
using System.Text;
using System.Text.Json;
using coreworking_space_booking_backend.Helpers.ImageUpload;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    options.Configure(builder.Configuration.GetSection("Kestrel"));
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
});

string connectionString = builder.Configuration.GetConnectionString("MySqlConnection");
string version = builder.Configuration.GetConnectionString("MySqlVersion");
MySqlServerVersion serverVersion = new MySqlServerVersion(ServerVersion.Parse(version));

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, serverVersion));

var product = builder.Configuration["Product"];
var logVersion = builder.Configuration["Version"];
var vendor = builder.Configuration["Vendor"];
var logPath = Path.Combine(Environment.CurrentDirectory, "Logs");
Directory.CreateDirectory(logPath);

builder.Services.AddLogger<LogConfigurations>(new LoggerConfiguration
{
    AppVersion = logVersion,
    Product = product,
    Vendor = vendor,
    LogPath = logPath
});

builder.Services.AddScoped<IAppLogger, AppLogger>();

builder.Services.AddScoped<ITestService, TestService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<IPricingService, PricingService>();
builder.Services.AddScoped<ImageUploadHelper>();



var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
Directory.CreateDirectory(uploadFolder);
builder.Services.AddScoped<IFacilityService, FacilityService>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<ICardDetailsService, CardDetailsService>();


builder.Services.AddControllers(o =>
{
    o.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));
})
.AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
    options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.SnakeCaseLower;
});


var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.ASCII.GetBytes(jwtSettings["Key"]);

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
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IFacilityService, FacilityService>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IPermissionService, PermissionService>();


var app = builder.Build();

using (IServiceScope scope = app.Services.CreateScope())
{
    scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
