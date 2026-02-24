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
using Avalonia.Media;


class GameController
{
    public Window win;

    private int mapSizeRow = 6;
    private int mapSizeCol = 6;
    private List<List<Rectangle>> mapGridElements = new List<List<Rectangle>>(); //[row][col]

    public GameController()
    {
        win = new Window
        {
            Title = "TerritoryGame",
            Height = 500, 
            Width = 500, 
        };

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

            var mapRowElements = new List<Rectangle>();
            for(int col = 0; col < mapSizeCol; col++)
            {
                Rectangle rect = new Rectangle
                {
                    Width = 50,     //this is max width here i think
                    Height = 50,    //max height
                };
                rect.Fill = Brushes.Blue;
                Grid.SetRow(rect, row);
                Grid.SetColumn(rect, col);
                mapGrid.Children.Add(rect);
                mapRowElements.Add(rect);
            }
            mapGridElements.Add(mapRowElements);
        }

        mapGridElements[0][4].Fill = Brushes.Red;

        //grid.
        stack.Children.Add(mapGrid);
        win.Content = stack;
        win.Show();
    }

    public void Run()
    {
        while (true)
        {
            
        }
    }
}