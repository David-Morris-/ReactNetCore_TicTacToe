import React from 'react';

function NewGameButton({ onNewGame, disabled }) {
  return (
    <button
      className="new-game-btn"
      onClick={onNewGame}
      disabled={disabled}
    >
      New Game
    </button>
  );
}

export default NewGameButton;
