using DataAccess;
using DataAccess.AccessingDbRent.Concrete;
using Microsoft.OpenApi.Models;
using MyApi.Controllers.SystemAuth;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Swashbuckle.AspNetCore.Filters;
using CloudinaryDotNet;
using Business.Concrete;
using MyApi.Method;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Swagger/OpenAPI configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<IUserService, userService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8
                .GetBytes(builder.Configuration.GetSection("HomeLand:Token").Value)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    }); 
builder.Services.AddSwaggerGen();

// CORS policy
builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*")
           .AllowAnyMethod()
           .AllowAnyHeader();
}));
builder.Services.AddSingleton<Cloudinary>(provider =>
{
    IConfiguration configuration = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
              .Build();

    string cloudName = configuration["Password:CloudName"];
    string apiKey = configuration["Password:ApiKey"];
    string apiSecret = configuration["Password:ApiSecret"];
    var account = new Account(cloudName, apiKey, apiSecret);
    return new Cloudinary(account);
});
builder.Services.AddScoped<LandImgOperation>();
builder.Services.AddScoped<LandOperation>();
builder.Services.AddScoped<LandCustomerOperation>();
builder.Services.AddSingleton<UploadImageAndGetPath>();
builder.Services.AddScoped<ObyektOperationImg>();
builder.Services.AddScoped<ObyektOperation>();
builder.Services.AddScoped<ObyektOperationCustomer>();
builder.Services.AddScoped<OfficeImgOperation>();
builder.Services.AddScoped<OfficeOperation>();
builder.Services.AddScoped<OfficeCustomerOperation>();
builder.Services.AddScoped<RentHomeOperationImg>();
builder.Services.AddScoped<RentHomeOperation>();
builder.Services.AddScoped<RentHomeOperationCustomer>();
builder.Services.AddScoped<SellOperationImg>();
builder.Services.AddScoped<SellOperation>();
builder.Services.AddScoped<SellOperationCustomer>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseCors("corsapp");

app.UseAuthentication();

app.UseAuthorization();


app.MapControllers();

// Seed Membership
await MiddlewareRegister.SeedMembershipAsync(app);


app.Run();
