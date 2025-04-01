using ChickTrack.Base.Domain.Entities;
using ChickTrack.Data;
using ChickTrack.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Graph.Models.ExternalConnectors;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Scalar.AspNetCore;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ChickTrack", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
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
            new string[] {}
        }
    });
});
// Add services to the container.
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
                .AddJwtBearer(option =>
                {

                    option.SaveToken = true;
                    option.RequireHttpsMetadata = false;
                    option.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidAudience = builder.Configuration["JWT:ValidAudience"],
                        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
                    };
                });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("SuperAdminOnly", policy => policy.RequireRole("SuperAdmin"));
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin", "SuperAdmin")); 
    options.AddPolicy("User", policy => policy.RequireRole("User", "Admin", "SuperAdmin")); 
});



builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("https://chick-track.vercel.app")
           .AllowAnyMethod()
           .AllowAnyHeader()
           .AllowCredentials(); // Only needed if using authentication cookies
}));

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services
    .AddBaseRepositoryDependencies(builder.Configuration)
    .AddServiceDependencies(builder.Configuration)
    .AddDataDependencies(builder.Configuration);

var app = builder.Build();

//Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

    app.UseSwagger();
    app.UseSwaggerUI();

}
app.MapScalarApiReference(options =>
{
    options
        .WithTitle("ChickTrack")
        .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient)
        .WithOpenApiRoutePattern("/swagger/v1/swagger.json"); // Add Swagger JSON specification
});

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("corsapp");

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
