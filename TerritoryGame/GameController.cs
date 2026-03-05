using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Globalization;

using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.PropertyStore;
using Avalonia.Controls.Shapes;
using Avalonia.Animation;



class GameController
{
    private GUI gui;
    private GameColors gameColors = new GameColors();
    private GameState gameState; 

    

    public GameController(GUI guiInput)
    {
        gui = guiInput;

        gameState = new GameState();
        gameState.LoadGameState(); //here the game state would be loaded

        gui.DisplayLoadedGameState(gameState, this);
    }
    
    private void CheckGameOver()
    {
        // if the current player has no valid tiles to claim, they are eliminated
        if (!gameState.CurrentPlayerHasLegalMoves())
        {
            gameState.EliminateCurrentPlayer();

            // game ends when all but one player are eliminated (or all for a draw)
            if (gameState.GetEliminatedCount() >= gameState.GetPlayerIdCount() - 1)
            {
                gameState.SetGameOver();
                gui.DisplayGameOver(gameState);
                Console.WriteLine("Game over");
                return;
            }

            // advance turn, skipping over any already-eliminated players
            do { gameState.AdvanceCurrentTurnPlayerId(); }
            while (gameState.IsPlayerEliminated(gameState.GetCurrenTurnPlayerId()));

            gui.UpdateGameVisualsAfterTilePressed(gameState);
        }
    }

    //I dont know if this function should be in gamestate or in here
    public void OnTileButtonPressed(MapTile pressedTile, object s, EventArgs e)
    {
        Console.WriteLine("Tile Button Pressed " + pressedTile.GetRowId() + ":" + pressedTile.GetColId());

        if(gameState.IsGameOver()) return;

        //if tile is empty disallow placing
        if(pressedTile.GetTileType() != TileType.Empty) return;
        
        if(gameState.GetPlayerPointById(gameState.GetCurrenTurnPlayerId()) != 0)
        {
            //CHECK If the clicked square has a adjacent color to it that is the same? corners count   
            bool hasAdjacentCurrentTurnPlayerOwnedTile = false;         
            for(int adjacentRow =-1; adjacentRow < 2; adjacentRow++)
            {
                for(int adjacentCol = -1; adjacentCol < 2; adjacentCol++)
                {
                    if(adjacentRow == 0 && adjacentCol == 0) continue; //we dont want to consider the clicked tile
  
                    MapTile adjacentMapTile = gameState.GetMapTile(pressedTile.GetRowId() + adjacentRow, pressedTile.GetColId() + adjacentCol);
                    if(adjacentMapTile == null) continue; //we dont want to consider "tiles" outside of bounds
                        

                    if(adjacentMapTile.GetOwnerId() == gameState.GetCurrenTurnPlayerId()) 
                        hasAdjacentCurrentTurnPlayerOwnedTile = true;
                }
            }

            //if there is no current turn player owned adjacent tile, disallow placing
            if(!hasAdjacentCurrentTurnPlayerOwnedTile) return; 
        }


        pressedTile.UpdateTileAfterPressed(gameState.GetCurrenTurnPlayerId());

        gameState.AwardPointToPlayerById(gameState.GetCurrenTurnPlayerId());
        gameState.AdvanceCurrentTurnPlayerId();

        gui.UpdateGameVisualsAfterTilePressed(pressedTile, gameState); 
        
        CheckGameOver();
    }

    public void OnLoadButtonPressed(object s, EventArgs e)
    {
        gameState = new GameState();
        gameState.LoadGameState(); 
        gui.DisplayLoadedGameState(gameState, this);
        Console.WriteLine("OnLoadButtonPressed");
    }
}