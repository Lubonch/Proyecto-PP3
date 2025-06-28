using Backend.Configs;
using log4net;
using log4net.Config;
using System.Reflection;




var builder = WebApplication.CreateBuilder(args);

log4net.GlobalContext.Properties["ProcessName"] = builder.Configuration["Logging:LogInstance"] ?? "DefaultInstance";

// Configure log4net
var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));



// Set global process name (can be changed per class if needed)
log4net.GlobalContext.Properties["ProcessName"] = builder.Configuration["Logging:LogInstance"] ?? "DefaultInstance";

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Add CORS policy BEFORE building the app
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy => policy.WithOrigins("http://127.0.0.1:5500")
                        .AllowAnyHeader()
                        .AllowAnyMethod());
});

CustomExtensions.AddInjectionServices(builder.Services);
CustomExtensions.AddInjectionRepositories(builder.Services);

var app = builder.Build();

//Use CORS BEFORE routing and authorization
app.UseCors("AllowFrontend");

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
