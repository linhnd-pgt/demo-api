using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Reflection;
using Service;
using Service.Repositories.Base;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;

// write Log in cmd
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

try
{
    Log.Information("Starting web host");


    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.


    builder.Services.AddControllers();



    // DB
    builder.Services.ConfigureSqlContext(builder.Configuration);


    // Services
    builder.Services.ConfigureCustomServices();

    // JWT
    builder.Services.AddAuthentication(opt => {
        opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this is my secret key for authentication"))
            };
        });

    builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy("AdminPolicy", policy => policy.RequireRole("ROLE_ADMIN"));
        options.AddPolicy("LibrarianPolicy", policy => policy.RequireRole("ROLE_LIBRARIAN"));
        options.AddPolicy("UserPolicy", policy => policy.RequireRole("ROLE_USER"));
    });

    builder.Host.UseSerilog((ctx, lc) => lc
        .WriteTo.Console());

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();

    builder.Services.AddSwaggerGen(swagger =>
    {
        swagger.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "Library Management System API",
            Version = "v1",
            Description = "APIs for developing Library Managment System",
            TermsOfService = new Uri("https://example.com/terms"),
            Contact = new OpenApiContact
            {
                Name = "Nguyen Duy Linh",
                Email = "linhnd1899@gmail.com",
                Url = new Uri("https://example.com/terms")
            },
            License = new OpenApiLicense
            {
                Name = "Rook BookStore API LICS",
                Url = new Uri("https://example.com/license")
            },
        });

        swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Please enter 'Bearer' followed by a space and your JWT token",
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
        });

        // Require JWT authentication for all operations
        swagger.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });

        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

        // config swagger to read xml comments to generate
        swagger.IncludeXmlComments(xmlPath);
    });




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

    app.Run();
}
catch(Exception ex)
{
    Log.Fatal(ex, "Host terminated");
}
finally
{
    Log.CloseAndFlush();
}


