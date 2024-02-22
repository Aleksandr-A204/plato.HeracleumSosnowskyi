using HeracleumSosnowskyiService.Data.PostgreSQL;
using HeracleumSosnowskyiService.MongoDb.Configuration;
using HeracleumSosnowskyiService.Repositories;
using HeracleumSosnowskyiService.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Register services here
builder.Services.Configure<MongoDbConfiguration>(builder.Configuration.GetSection(nameof(MongoDbConfiguration)));

builder.Services.AddScoped<IFilesRepository, FilesRepository>();
builder.Services.AddScoped<ISatelliteDataRepository, SatelliteDataRepository>();
builder.Services.AddScoped<IDatasetsRepository, DatasetsRepository>();
//builder.Services.AddScoped<ICachingService, CachingService>();
builder.Services.AddScoped<IProcessService, ProcessService>();

builder.Services.AddDbContext<PostgreSQLDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddMemoryCache();
//builder.Services.TryAdd(ServiceDescriptor.Scoped<IMemoryCache, MemoryCache>());

// JSON Serializer
builder.Services.AddControllersWithViews().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
.AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());

builder.Services.AddControllers();

// Enable CORS http://localhost:5173
builder.Services.AddCors(option => option.AddPolicy(name: "myAllowSpecificOrigins", builder =>
    builder.WithOrigins("http://localhost:5173").AllowAnyMethod().AllowAnyHeader()));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(configur =>
{
    configur.SwaggerDoc("v1", new() { Title = "Plato.HeracleumSosnowskiy API", Version = "v1" });
});

//builder.Services.AddControllersWithViews().AddNewtonsoftJson();

// Установим глобально размер тела любого запроса в байтах - NULL.
builder.WebHost.ConfigureKestrel(options => options.Limits.MaxRequestBodySize = null);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(configur => configur.SwaggerEndpoint("/swagger/v1/swagger.json", "Plato.HeracleumSosnowskiy API v1"));
}

app.UseAuthorization();

app.MapControllers();

// Enable CROS
app.UseCors("myAllowSpecificOrigins");

app.Run();
