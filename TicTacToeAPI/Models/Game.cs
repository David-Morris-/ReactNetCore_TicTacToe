namespace TicTacToeAPI.Models;

public class Game
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Player1Id { get; set; } = string.Empty;
    public string Player2Id { get; set; } = string.Empty;
    public string Player1Symbol { get; set; } = "X";
    public string Player2Symbol { get; set; } = "O";
    public GameState State { get; set; } = new GameState();
    public string CurrentPlayerId { get; set; } = string.Empty;
    public string? Winner { get; set; }
    public bool IsDraw { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? FinishedAt { get; set; }
}

public class GameState
{
    public string[] Board { get; set; } = new string[9];
    public int MoveCount { get; set; } = 0;

    public GameState()
    {
        for (int i = 0; i < 9; i++)
        {
            Board[i] = string.Empty;
        }
    }
}

public class MoveRequest
{
    public int Position { get; set; }
    public string PlayerId { get; set; } = string.Empty;
}

public class GameResponse
{
    public Guid Id { get; set; }
    public string CurrentPlayerId { get; set; } = string.Empty;
    public string CurrentPlayerSymbol { get; set; } = string.Empty;
    public GameState State { get; set; } = new GameState();
    public string? Winner { get; set; }
    public bool IsDraw { get; set; }
    public bool IsGameOver { get; set; }
}

public class GameCreateRequest
{
    public string Player1Id { get; set; } = string.Empty;
    public string Player2Id { get; set; } = string.Empty;
}
