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
using Tmds.DBus.Protocol;
using System.Security.Cryptography.X509Certificates;

using Avalonia.Automation.Peers;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.Input;
using Avalonia.LogicalTree;
using Avalonia.VisualTree;
using System.Data;

//have one function where we load everything, and dont reload every elemnt on screen just the ones that changed
class GUI
{
    public Window win;
    private List<List<Rectangle>> mapTiles = new List<List<Rectangle>>(); //[row][col]

    private TextBlock currentTurnPlayerText;

    public GUI()
    {
        win = new Window
        {
            Title = "TerritoryGame",
            Height = 500, 
            Width = 500, 
        };
    }



    public void DisplayLoadedGameState(GameState gameState, GameController gameController)
    {
        int mapSizeRow = gameState.GetMapSizeRow();
        int mapSizeCol = gameState.GetMapSizeCol();

        

        var stack = new StackPanel 
        {
            Orientation = Orientation.Vertical,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            Margin = new Thickness(20),
        };


        Button skipButton = new Button
        {
            ClickMode = ClickMode.Press,
            Width = 80,
            Height = 30,
            Content = "Skip"
        
        };
        skipButton.Click += gameController.OnSkipButtonPressed;


        Button loadButton = new Button
        {
            ClickMode = ClickMode.Press,
            Width = 80,
            Height = 30,
            Content = "Load"
        };
        loadButton.Click += gameController.OnLoadButtonPressed;

        Button saveButton = new Button
        {
            ClickMode = ClickMode.Press,
            Width = 80,
            Height = 30,
            Content = "Save"
        };
        saveButton.Click += gameController.OnSaveButtonPressed;




        

        currentTurnPlayerText = new TextBlock
        {
            FontSize = 25,
        };
        UpdateCurrentTurnPlayerText(gameState);
        
        //Declare the map
        Grid mapGrid = new Grid
        {
            ShowGridLines = true
        };

        
        //Set rows and cols count
        for(int row = 0; row < mapSizeRow; row++)
            mapGrid.RowDefinitions.Add(new RowDefinition(GridLength.Star));
        

        for(int col = 0; col < mapSizeCol; col++)
            mapGrid.ColumnDefinitions.Add(new ColumnDefinition(GridLength.Star));
        

        for(int row = 0; row < mapSizeRow; row++)
        {
            for(int col = 0; col < mapSizeCol; col++)
            {
                var tileColor = gameState.GetMapTile(row,col).GetColor();

                Button tileButton = new Button
                {
                    ClickMode = ClickMode.Press,
                    Width = 50,
                    Height = 50,
                    Background = tileColor,
                    CornerRadius = new CornerRadius(0),
                    //somehow set the hover color 
                };

                MapTile mapTile = gameState.GetMapTile(row,col); //has to be stored before passing the value to TileButtonPressed
                tileButton.Click += (sender, e) =>
                {
                    gameController.OnTileButtonPressed(mapTile, sender, e);
                };

                Grid.SetRow(tileButton, row);
                Grid.SetColumn(tileButton, col);

                mapGrid.Children.Add(tileButton);

                gameState.GetMapTile(row,col).SetTileVisual(tileButton);
            }
        }

        stack.Children.Add(currentTurnPlayerText);
        stack.Children.Add(mapGrid);

        stack.Children.Add(skipButton);
        stack.Children.Add(loadButton);
        stack.Children.Add(saveButton);
        win.Content = stack;
        win.Show();
    }


    public void UpdateGameVisualsAfterTilePressed(MapTile pressedTile, GameState gameState)
    {
        //update -> currentTurnPlayerText
        pressedTile.GetTileVisual().Background = pressedTile.GetColor();
        UpdateCurrentTurnPlayerText(gameState);
        

    }

    public void UpdateGameVisualsAfterTilePressed(GameState gameState)
    {
        UpdateCurrentTurnPlayerText(gameState);
    }

    public void UpdateCurrentTurnPlayerText(GameState gameState)
    {
        currentTurnPlayerText.Text = "Player" + gameState.GetCurrenTurnPlayerId() + " to move";
        currentTurnPlayerText.Background = gameState.GetCurrentTurnPlayerColor();
    }
}





