using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
using Avalonia.Platform;

namespace TerritoryGame;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        this.Width = 600; 
        this.Height = 500;
    }
}
