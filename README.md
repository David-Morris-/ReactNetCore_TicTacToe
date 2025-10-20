# React + .NET Core Tic Tac Toe Game

A full-stack Tic Tac Toe game built with React frontend and .NET Core backend.

## Project Structure

- `TicTacToeAPI/` - .NET Core Web API backend
- `tictactoe-frontend/` - Vanilla HTML/CSS/JavaScript frontend
- `react-tictactoe-frontend/` - React frontend application

## Features

- Real-time multiplayer gameplay
- Game state management
- Win detection
- Responsive design
- RESTful API communication

## Getting Started

### Running the Application

1. **Start the Backend API** (already running on http://localhost:5264):
   ```bash
   cd TicTacToeAPI
   dotnet run
   ```

2. **Choose your Frontend**:

   **Option 1: React Frontend (Recommended)**
   ```bash
   cd react-tictactoe-frontend
   npm install
   npm start
   ```
   - The React app will open at http://localhost:3000
   - The game will automatically connect to the API and start a new game

   **Option 2: Vanilla HTML/CSS/JavaScript Frontend**
   - Open `tictactoe-frontend/index.html` in your web browser
   - The game will automatically connect to the API and start a new game

3. **Play the Game**:
   - Click on any empty cell to make your move
   - Players alternate turns (X goes first)
   - First player to get 3 in a row wins!
   - Click "New Game" to start over

### API Endpoints

- `POST /games` - Create a new game
- `GET /games/{gameId}` - Get game state
- `POST /games/{gameId}/moves` - Make a move
- `GET /games` - Get all games (for debugging)

## Technologies Used

- **Backend**: .NET Core 8.0, ASP.NET Core Web API, Minimal API
- **Frontend Options**:
  - React 18.2.0 with modern hooks and components
  - Vanilla HTML/CSS/JavaScript (no framework needed)
- **Communication**: REST API with JSON
- **Architecture**: Full-stack with separated concerns

## React Frontend Features

The React frontend (`react-tictactoe-frontend/`) includes:

- **Modern React Architecture**: Uses functional components with hooks
- **Component-based Design**: Modular, reusable components
- **Responsive Layout**: Works on desktop and mobile devices
- **Error Handling**: Graceful error handling with retry functionality
- **Loading States**: Visual feedback during API calls
- **Clean Code Structure**: Well-organized file structure with separation of concerns
