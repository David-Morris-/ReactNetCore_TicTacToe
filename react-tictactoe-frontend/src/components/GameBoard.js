import React from 'react';

function GameBoard({ game, onCellClick, disabled }) {
  if (!game) {
    return (
      <div className="game-board">
        {Array(9).fill(null).map((_, index) => (
          <div key={index} className="game-cell loading"></div>
        ))}
      </div>
    );
  }

  return (
    <div className="game-board">
      {game.state.board.map((cell, index) => (
        <div
          key={index}
          className={`game-cell ${cell ? `taken ${cell}` : ''}`}
          onClick={() => !cell && !game.isGameOver && !disabled && onCellClick(index)}
        >
          {cell}
        </div>
      ))}
    </div>
  );
}

export default GameBoard;
