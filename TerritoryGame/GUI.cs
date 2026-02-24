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

class GUI
{
    public Window win;
    private List<List<Rectangle>> mapTiles = new List<List<Rectangle>>(); //[row][col]



    public GUI()
    {
        win = new Window
        {
            Title = "TerritoryGame",
            Height = 500, 
            Width = 500, 
        };
    }

    public void DisplayMapGrid(GameState gameState)
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

        //Declare the map
        Grid mapGrid = new Grid
        {
            ShowGridLines = true,
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
                var tileColor = gameState.mapTiles[row][col].GetTileColor();
                Rectangle rect = new Rectangle
                {
                    Width = 50,     //this is max width here i think
                    Height = 50,    //max height
                };
                rect.Fill = tileColor;
                Grid.SetRow(rect, row);
                Grid.SetColumn(rect, col);

                mapGrid.Children.Add(rect);

                gameState.mapTiles[row][col].SetTileVisual(rect);
            }
        }

        //mapTiles[0][4].Fill = Brushes.Red;


        stack.Children.Add(mapGrid);
        win.Content = stack;
        win.Show();
    }

}