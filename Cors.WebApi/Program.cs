using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options => options.LoginPath = "/auth");
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin();
        policy.WithMethods("GET");
        policy.AllowAnyHeader();
    });

    options.AddPolicy("NoHeaders", policy =>
    {
        policy.AllowAnyOrigin();
        policy.AllowAnyMethod();
        policy.WithHeaders("Content-Type");
    });

    options.AddPolicy("ExposeHeader", policy =>
    {
        policy.AllowAnyHeader();
        policy.AllowAnyOrigin();
        policy.AllowAnyMethod();
        policy.WithExposedHeaders("X-Custom-Header");
    });

    options.AddPolicy("CustomHeader", policy =>
    {
        policy.AllowAnyOrigin();
        policy.AllowAnyMethod();
        policy.WithHeaders("X-Custom-Header");
    });

    options.AddPolicy("WithCredentials", policy =>
    {
        policy.WithOrigins("http://localhost:5001", "http://localhost:5002");
        policy.AllowAnyMethod();
        policy.AllowAnyHeader();
        policy.AllowCredentials();
    });
});

var app = builder.Build();


app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
