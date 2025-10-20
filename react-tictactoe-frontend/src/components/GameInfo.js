import React from 'react';

function GameInfo({ game }) {
  if (!game) {
    return (
      <div className="game-info">
        <p>Click on any cell to make your move!</p>
        <p>Game ID: Loading...</p>
      </div>
    );
  }

  return (
    <div className="game-info">
      <p>Click on any cell to make your move!</p>
      <p>Game ID: {game.id}</p>
    </div>
  );
}

export default GameInfo;
