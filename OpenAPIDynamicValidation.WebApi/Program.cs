using OpenAPIDynamicValidation.BAL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddScoped<ValidationService>();  // Register ValidationService
// Configure services
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Enable case-insensitive property matching
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;

        // Ensure fields are mapped irrespective of sequence
        // (This is the default behavior in .NET's JSON deserialization)
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();  // Enable Swagger UI

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
