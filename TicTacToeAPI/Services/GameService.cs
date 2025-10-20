using TicTacToeAPI.Models;

namespace TicTacToeAPI.Services;

public class GameService
{
    private readonly Dictionary<Guid, Game> _games = new();

    public Game CreateGame(string player1Id, string player2Id)
    {
        var game = new Game
        {
            Player1Id = player1Id,
            Player2Id = player2Id,
            CurrentPlayerId = player1Id
        };

        _games[game.Id] = game;
        return game;
    }

    public Game? GetGame(Guid gameId)
    {
        _games.TryGetValue(gameId, out var game);
        return game;
    }

    public GameResponse MakeMove(Guid gameId, MoveRequest move)
    {
        var game = GetGame(gameId);
        if (game == null)
            throw new ArgumentException("Game not found");

        if (game.IsDraw || !string.IsNullOrEmpty(game.Winner))
            throw new InvalidOperationException("Game is already finished");

        if (game.CurrentPlayerId != move.PlayerId)
            throw new InvalidOperationException("Not your turn");

        if (move.Position < 0 || move.Position > 8)
            throw new ArgumentException("Invalid position");

        if (!string.IsNullOrEmpty(game.State.Board[move.Position]))
            throw new InvalidOperationException("Position already taken");

        // Determine player's symbol
        var playerSymbol = game.Player1Id == move.PlayerId ? game.Player1Symbol : game.Player2Symbol;

        // Make the move
        game.State.Board[move.Position] = playerSymbol;
        game.State.MoveCount++;

        // Check for winner
        var winner = CheckWinner(game.State.Board);
        if (!string.IsNullOrEmpty(winner))
        {
            game.Winner = move.PlayerId;
            game.FinishedAt = DateTime.UtcNow;
        }
        else if (game.State.MoveCount == 9)
        {
            game.IsDraw = true;
            game.FinishedAt = DateTime.UtcNow;
        }
        else
        {
            // Switch to next player
            game.CurrentPlayerId = game.CurrentPlayerId == game.Player1Id ? game.Player2Id : game.Player1Id;
        }

        return MapToGameResponse(game);
    }

    private string? CheckWinner(string[] board)
    {
        var winningCombinations = new[]
        {
            new[] { 0, 1, 2 },
            new[] { 3, 4, 5 },
            new[] { 6, 7, 8 },
            new[] { 0, 3, 6 },
            new[] { 1, 4, 7 },
            new[] { 2, 5, 8 },
            new[] { 0, 4, 8 },
            new[] { 2, 4, 6 }
        };

        foreach (var combination in winningCombinations)
        {
            if (!string.IsNullOrEmpty(board[combination[0]]) &&
                board[combination[0]] == board[combination[1]] &&
                board[combination[1]] == board[combination[2]])
            {
                return board[combination[0]];
            }
        }

        return null;
    }

    private GameResponse MapToGameResponse(Game game)
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

    public List<Game> GetAllGames()
    {
        return _games.Values.ToList();
    }
}
