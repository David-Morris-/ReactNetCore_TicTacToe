import React from 'react';

function GameStatus({ game }) {
  if (!game) {
    return (
      <div className="game-status loading">
        Loading game...
      </div>
    );
  }

  if (game.isGameOver) {
    if (game.winner) {
      const winnerSymbol = game.winner === 'player1' ? 'X' : 'O';
      return (
        <div className="game-status winner">
          Player {winnerSymbol} wins! 🎉
        </div>
      );
    } else if (game.isDraw) {
      return (
        <div className="game-status draw">
          It's a draw! 🤝
        </div>
      );
    }
  }

  return (
    <div className="game-status current-player">
      Player {game.currentPlayerSymbol}'s turn
    </div>
  );
}

export default GameStatus;
