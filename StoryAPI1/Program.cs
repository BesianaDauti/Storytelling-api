using Microsoft.EntityFrameworkCore;
using StoryAPI1.Data;
using StoryAPI1.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<StoryDbContext>(options =>
    options.UseInMemoryDatabase("StoriesDb")); 

builder.Services.AddHttpClient();
builder.Services.AddScoped<TextToSpeechService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseCors("AllowLocalhost");
app.UseAuthorization();
app.MapControllers();
app.Run();
