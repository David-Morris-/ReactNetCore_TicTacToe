using TicTacToeAPI.Models;
using TicTacToeAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<GameService>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("AllowAll");
}

app.UseHttpsRedirection();

// Get the game service
var gameService = app.Services.GetRequiredService<GameService>();

// Create a new game
app.MapPost("/games", (GameCreateRequest request) =>
{
    try
    {
        var game = gameService.CreateGame(request.Player1Id, request.Player2Id);
        return Results.Created($"/games/{game.Id}", MapToGameResponse(game));
    }
    catch (Exception ex)
    {
        return Results.BadRequest(new { error = ex.Message });
    }
})
.WithName("CreateGame")
.WithOpenApi();

// Get game state
app.MapGet("/games/{gameId}", (Guid gameId) =>
{
    try
    {
        var game = gameService.GetGame(gameId);
        if (game == null)
            return Results.NotFound(new { error = "Game not found" });

        return Results.Ok(MapToGameResponse(game));
    }
    catch (Exception ex)
    {
        return Results.BadRequest(new { error = ex.Message });
    }
})
.WithName("GetGame")
.WithOpenApi();

// Make a move
app.MapPost("/games/{gameId}/moves", (Guid gameId, MoveRequest move) =>
{
    try
    {
        var gameResponse = gameService.MakeMove(gameId, move);
        return Results.Ok(gameResponse);
    }
    catch (ArgumentException ex)
    {
        return Results.NotFound(new { error = ex.Message });
    }
    catch (InvalidOperationException ex)
    {
        return Results.BadRequest(new { error = ex.Message });
    }
    catch (Exception ex)
    {
        return Results.BadRequest(new { error = ex.Message });
    }
})
.WithName("MakeMove")
.WithOpenApi();

// Get all games (for debugging)
app.MapGet("/games", () =>
{
    var games = gameService.GetAllGames();
    return Results.Ok(games.Select(MapToGameResponse));
})
.WithName("GetAllGames")
.WithOpenApi();

app.Run();

GameResponse MapToGameResponse(Game game)
{
    var currentPlayerSymbol = game.CurrentPlayerId == game.Player1Id ? game.Player1Symbol : game.Player2Symbol;

    return new GameResponse
    {
        Id = game.Id,
        CurrentPlayerId = game.CurrentPlayerId,
        CurrentPlayerSymbol = currentPlayerSymbol,
        State = game.State,
        Winner = game.Winner,
        IsDraw = game.IsDraw,
        IsGameOver = !string.IsNullOrEmpty(game.Winner) || game.IsDraw
    };
}
