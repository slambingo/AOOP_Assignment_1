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
        MapCreationController mapCreationController = new MapCreationController(gui);   
        GameController gameController = new GameController(gui);
        gui.SetControllers(gameController, mapCreationController);
        
        app.Run(gui.win);
    }
}