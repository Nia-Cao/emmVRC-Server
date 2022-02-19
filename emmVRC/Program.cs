using emmVRC.Models;
using emmVRC.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


// Add services to the container.
builder.Services.Configure<dbSettings>(
    builder.Configuration.GetSection("database"));
builder.Services.AddSingleton<dbService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    //app.UseSwaggerUI();
}

//app.UseHttpsRedirection();


app.UseAuthorization();

app.MapControllers();

app.Run();
