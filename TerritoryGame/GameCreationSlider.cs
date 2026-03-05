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
using System.Security.Cryptography;
using System.Dynamic;

class GameCreationSlider
{
    public StackPanel stack;

    public Slider slider;


    public GameCreationSlider(string sliderName, int min, int max)
    {
        stack = new StackPanel
        {
            Orientation = Orientation.Horizontal,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            //Margin = new Thickness(5),
        };

        var sliderText =  new TextBlock
        {
            FontSize = 25,
            Text = sliderName,
            Margin = new Thickness(5),
            
        };

        slider = new Slider
        {
            Minimum = min,
            Maximum = max,
            Width = 100,
            Margin = new Thickness(5),
        };

        var sliderValue =  new TextBlock
        {
            FontSize = 25,
            Text = "**",
            Margin = new Thickness(5),
        };
        
        slider.ValueChanged += (sender, e) =>
        {
            var newValue = (int)slider.Value;
            sliderValue.Text = newValue.ToString();
        };

        stack.Children.Add(sliderText);
        stack.Children.Add(slider);
        stack.Children.Add(sliderValue);

    }

    public int GetSliderValue()
    {
        return (int)slider.Value;
    }

}