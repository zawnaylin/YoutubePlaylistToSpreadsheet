using YouTubePlaylistToSpreadsheetApi.Endpoints;
using YouTubePlaylistToSpreadsheetApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IErrorResponseFactory, ErrorResponseFactory>();
builder.Services.AddSingleton<IBodyParamUtils, BodyParamUtils>();
builder.Services.AddScoped<IYouTubeServiceProvider, YouTubeServiceProvider>();
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

app.MapGroup("/api")
    .MapYoutubePlaylistApi();

string ColorName(string color) => $"Color specified: {color}";

app.MapGet("/colorSelector/{color}", ColorName)
    .AddEndpointFilter(async (invocationContext, next) =>
    {
        var color = invocationContext.GetArgument<string>(0);
        if (color == "red")
        {
            return Results.Problem("Red not allowed.");
        }

        return await next(invocationContext);
    });

app.Run();
