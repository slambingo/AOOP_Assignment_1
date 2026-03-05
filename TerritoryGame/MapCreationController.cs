using System.Dynamic;
using Avalonia.Markup.Xaml.MarkupExtensions;

class MapCreationController
{
    private GameState mapCreation = new GameState();
    private int defaultMapSizeRow = 2;
    private int defaultMapSizeCol = 2; 

    private int defaultPlayerCount = 2;

    private GUI gui;

    public MapCreationController(GUI guiInput)
    {
        //Console.WriteLine("OnSaveButtonPressed");
        mapCreation.CreateGameState(defaultMapSizeRow, defaultMapSizeCol, defaultPlayerCount);
        gui = guiInput;
    }

    public GameState GetMapCreation() {return mapCreation;}

    public void OnSaveButtonPressed(object s, EventArgs e)
    {
        mapCreation.SaveGameState();
        Console.WriteLine("OnSaveButtonPressed");
    }

    //update on slider or on button press
    public void UpdateMapCreation(int mapSizeRow, int mapSizeCol, int playerCount, object s, EventArgs e)
    {
        mapCreation.CreateGameState(mapSizeRow, mapSizeCol, playerCount);
        gui.UpdateMapCreationGrid(false);
    }

    public void OnTileButtonPressed(MapTile pressedTile, object s, EventArgs e)
    {
        pressedTile.ToggleEmptyOrMountainTile();
        gui.UpdateGameVisualsAfterTilePressed(pressedTile, mapCreation);
        
    }
}