using Avalonia;
using Avalonia.Controls;

class Program
{
    public static void Main(string[] args)
    {
        AppBuilder.Configure<Application>()
                  .UsePlatformDetect()
                  .Start(AppMain, args);
    }

    public static void AppMain(Application app, string[] args)
    {
        app.Styles.Add(new Avalonia.Themes.Fluent.FluentTheme());
        app.RequestedThemeVariant = Avalonia.Styling.ThemeVariant.Light;

    
        GUI gui = new GUI();     
        GameController gameController = new GameController(gui);
        
        //gameController.Run();
        app.Run(gui.win);
    }
}