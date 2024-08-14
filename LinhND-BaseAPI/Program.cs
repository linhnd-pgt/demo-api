using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Reflection;
using Service;
using Service.Repositories.Base;

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
            }
        });

 /*       var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

        // config swagger to read xml comments to generate
        swagger.IncludeXmlComments(xmlPath);*/
    });




    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

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


