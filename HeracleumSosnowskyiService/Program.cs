var builder = WebApplication.CreateBuilder(args);

// Enable CORS
builder.Services.AddCors(option => option.AddPolicy(name: "myAllowSpecificOrigins", policy =>
    policy.WithOrigins("http://localhost:5173").AllowAnyMethod().AllowAnyHeader()
));

//// JSON Serializer
//builder.Services.AddControllersWithViews().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
//.AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());

//builder.Services.AddTransient<IStorageService, StorageService>();

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Установим глобально размер тела любого запроса в байтах - NULL.
builder.WebHost.ConfigureKestrel(options => options.Limits.MaxRequestBodySize = null);

//var connectingToDbString = builder.Configuration.GetConnectionString("Storage:ConnectingToMongoDB");
//// Определяем MongoClient как синглтон
//builder.Services.AddSingleton(new MongoClient(connectingToDbString).GetDatabase("test"));


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
