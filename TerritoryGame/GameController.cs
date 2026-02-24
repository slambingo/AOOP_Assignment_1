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



class GameController
{
    public Window win;

    public GameController()
    {
        win = new Window
        {
            Title = "TerritoryGame",
            Height = 700, 
            Width = 600, 
        };

        var stack = new StackPanel {
            Orientation = Orientation.Vertical,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            Margin = new Thickness(20),
        };

        //var tabControl = new TabControl();

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