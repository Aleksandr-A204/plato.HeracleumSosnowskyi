using HeracleumSosnowskyiService.Configuration;
using HeracleumSosnowskyiService.Data;
using HeracleumSosnowskyiService.Interfaces;
using HeracleumSosnowskyiService.Storage;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

//// JSON Serializer
//builder.Services.AddControllersWithViews().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
//.AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());

// Register services here
builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection(nameof(MongoDbSettings)));
builder.Services.AddSingleton<MongoDBContext>(serviceProvider =>
{
    var settings = serviceProvider.GetRequiredService<IOptions<MongoDbSettings>>().Value;
    return new MongoDBContext(settings.ConnectionString, settings.DatabaseName);
});

// Определяем FileCacheRepository и FilesRepository как скопед
builder.Services.AddSingleton<FileInfoRepository>();
builder.Services.AddSingleton<IFilesRepository, FileStreamRepository>();

builder.Services.AddControllers();

// Enable CORS
builder.Services.AddCors(option => option.AddPolicy(name: "myAllowSpecificOrigins", policy =>
    policy.WithOrigins("http://localhost:5173").AllowAnyMethod().AllowAnyHeader()));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Установим глобально размер тела любого запроса в байтах - NULL.
builder.WebHost.ConfigureKestrel(options => options.Limits.MaxRequestBodySize = null);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

// Enable CROS
app.UseCors("myAllowSpecificOrigins");

app.Run();
