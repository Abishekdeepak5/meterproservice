using MeterProService.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using MeterProService.Hubs;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using MeterProService.Components;
using SendingEmail;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.AddCors(options => {
    options.AddPolicy("customPolicy", x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c=>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
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



    #region Database Connectivity

    builder.Services.AddDbContext<MeterproDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

#endregion

// Add services to the container.

    builder.Services.AddControllers();
    builder.Services.AddControllers();
    builder.Services.AddSignalR();
    builder.Services.AddScoped<CabUpdates>();
#region authentication config



builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = "user",
        ValidIssuer = "user",
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("apwmdlliendaddnetknz=3mlkd652341")) // Use the same key as in generatetoken

    });



    #endregion

    var app = builder.Build();

    // Configure the HTTP request pipeline.


    app.UseSwagger();

    app.UseSwaggerUI(
        options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            options.RoutePrefix = String.Empty;
            options.DocumentTitle = "My Swagger";
            options.ConfigObject.AdditionalItems["presets"] = new[] { "Bearer" };
        });
    app.UseCors("customPolicy");

    app.UseHttpsRedirection();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.MapHub<ClientUpdateSignalR>("/LocationHub");

    app.Run();
