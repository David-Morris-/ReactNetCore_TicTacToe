import React, { useState, useEffect } from 'react';
import { GameAPI } from './api';
import GameBoard from './components/GameBoard';
import GameStatus from './components/GameStatus';
import NewGameButton from './components/NewGameButton';
import GameInfo from './components/GameInfo';
import './App.css';

function App() {
  const [game, setGame] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    startNewGame();
  }, []);

  const startNewGame = async () => {
    try {
      setLoading(true);
      setError(null);
      const newGame = await GameAPI.createGame();
      setGame(newGame);
    } catch (err) {
      setError('Failed to connect to the game server. Make sure the API is running on http://localhost:5264');
      console.error('Error starting new game:', err);
    } finally {
      setLoading(false);
    }
  };

  const handleCellClick = async (position) => {
    if (!game || game.isGameOver || loading) return;

    try {
      setLoading(true);
      const updatedGame = await GameAPI.makeMove(game.id, position, game.currentPlayerId);
      setGame(updatedGame);
    } catch (err) {
      alert(err.message);
      console.error('Error making move:', err);
    } finally {
      setLoading(false);
    }
  };

  if (error) {
    return (
      <div className="app">
        <div className="error-container">
          <h1>Tic Tac Toe</h1>
          <div className="error-message">{error}</div>
          <button onClick={startNewGame} className="retry-button">
            Retry Connection
          </button>
        </div>
      </div>
    );
  }

  return (
    <div className="app">
      <div className="game-container">
        <h1>React Tic Tac Toe</h1>

        <GameStatus game={game} />

        <GameBoard
          game={game}
          onCellClick={handleCellClick}
          disabled={loading}
        />

        <NewGameButton
          onNewGame={startNewGame}
          disabled={loading}
        />

        <GameInfo game={game} />
      </div>
    </div>
  );
}

export default App;
