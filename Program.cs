using MiniBackend.Repositories;
using MiniBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Builder;

var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options => 
    {
        options.AddPolicy(name: MyAllowSpecificOrigins,
            builder => 
            {
                builder.WithOrigins(
                    "http://localhost:8080",
                    "https://localhost:8080"
                ).AllowAnyHeader()
                .AllowAnyMethod();
            }
        );
    });

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Add SqlServer
builder.Services.AddDbContext<MiniContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BackendMinisContext")));
builder.Services.AddScoped<IMinisRepository, SqlServerDbMinisRepository>();

// Add front end
// builder.Services.AddSpaStaticFiles(configuration => configuration.RootPath = "wwwroot/dist");

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
}

app.UseCors(MyAllowSpecificOrigins);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseDefaultFiles();

// app.UseSpaStaticFiles();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
           Path.Combine(builder.Environment.ContentRootPath, "Images")),
    RequestPath = "/Images"
});

app.UseAuthorization();

app.MapControllers();

// app.UseSpa(configuration: builder =>
//     {
//         if (app.Environment.IsDevelopment())
//         {
//             builder.UseProxyToSpaDevelopmentServer("http://localhost:8080");
//         }
//     });

app.Run();
