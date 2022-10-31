using SmartMobility.API.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


builder.Services.Configure<CarSharingMongoDatabaseSettings>(builder.Configuration.GetSection("MongoDatabaseSettings"));
builder.Services.AddSingleton<MongoDbContext>();
// Cors Define
builder.Services.AddCors(options =>
{
    options.AddPolicy("TesCors",
        builder =>
        {
            builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
        });
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("TesCors");


app.UseAuthorization();

app.MapControllers();

app.Run();
