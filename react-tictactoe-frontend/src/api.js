const API_BASE = 'http://localhost:5264';

export class GameAPI {
  static async createGame(player1Id = 'player1', player2Id = 'player2') {
    try {
      const response = await fetch(`${API_BASE}/games`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({ player1Id, player2Id })
      });

      if (!response.ok) {
        throw new Error(`HTTP error! status: ${response.status}`);
      }

      return await response.json();
    } catch (error) {
      console.error('Error creating game:', error);
      throw error;
    }
  }

  static async getGame(gameId) {
    try {
      const response = await fetch(`${API_BASE}/games/${gameId}`);

      if (!response.ok) {
        throw new Error(`HTTP error! status: ${response.status}`);
      }

      return await response.json();
    } catch (error) {
      console.error('Error getting game:', error);
      throw error;
    }
  }

  static async makeMove(gameId, position, playerId) {
    try {
      const response = await fetch(`${API_BASE}/games/${gameId}/moves`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({ position, playerId })
      });

      if (!response.ok) {
        const error = await response.json();
        throw new Error(error.error || 'Failed to make move');
      }

      return await response.json();
    } catch (error) {
      console.error('Error making move:', error);
      throw error;
    }
  }

  static async getAllGames() {
    try {
      const response = await fetch(`${API_BASE}/games`);

      if (!response.ok) {
        throw new Error(`HTTP error! status: ${response.status}`);
      }

      return await response.json();
    } catch (error) {
      console.error('Error getting all games:', error);
      throw error;
    }
  }
}
