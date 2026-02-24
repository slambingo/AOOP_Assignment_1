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

    public void TileButtonPressed(MapTile pressedTile, object s, EventArgs e)
    {
        //update game state
        //update UI (color change)
        Console.WriteLine("Tile Button Pressed " + pressedTile.GetRowId() + ":" + pressedTile.GetColId());
    }
}