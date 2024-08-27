# Fantasy Premier League API
## Overview

The Fantasy Premier League API is a .NET-based web application designed to manage fantasy teams and players. It provides endpoints to perform CRUD (Create, Read, Update, Delete) operations for teams and players. This API supports functionalities such as retrieving all teams, getting a team by ID, creating new teams, updating existing teams, and deleting teams. It also supports similar operations for players.

## Features

- **Team Management:**
  - Retrieve all teams sorted by total points.
  - Get details of a specific team by ID.
  - Create a new team with a list of players.
  - Update an existing team with new player information.
  - Delete a team by ID.
  - Retrieve details of the team with the highest total points 

- **Player Management:**
  - Retrieve all players sorted by points.
  - Create new players with specific attributes.
  - Update player positions.
  - Delete players by ID.

## Models

### Player
- `Id` (int): Unique identifier for the player.
- `Name` (string): Name of the player.
- `Position` (string): Position of the player (e.g., GK, RW).
- `Points` (int): Points scored by the player.

### Team
- `Id` (int): Unique identifier for the team.
- `Name` (string): Name of the team.
- `TotalPoints` (int): Total points accumulated by the team based on its players.
- `Players` (List<Player>): List of players in the team.

## API Endpoints

### Teams

- **GET /api/Teams**
  - Retrieves all teams, including their total points and player details.

- **GET /api/Teams/{id}**
  - Retrieves details of a specific team by ID.

- **POST /api/Teams/create**
  - Creates a new team with the provided name and list of player IDs.

- **PUT /api/Teams/{id}**
  - Updates an existing team with new player information.

- **DELETE /api/Teams/{id}**
  - Deletes a team by ID.
- **GET /api/Teams/highestTeam**
  - Retrieves details of the team with the highest total points by ID.

### Players

- **GET /api/Players**
  - Retrieves all players, sorted by points.

- **POST /api/Players**
  - Creates a new player with the specified attributes.

- **PUT /api/Players/{id}**
  - Updates the position of a player by ID.

- **DELETE /api/Players/{id}**
  - Deletes a player by ID.
