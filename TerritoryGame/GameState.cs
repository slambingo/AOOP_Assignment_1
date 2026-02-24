//this will store the current game state, which then a new one can be loaded 
//will store:
//map
//number of players
//current player
//is the game still ongoing



using System.Dynamic;
using Avalonia.Controls.Shapes;
using Avalonia.Media;

class GameState
{
    private int mapSizeRow;
    private int mapSizeCol;  

    private int playerIdCount; //number of players in current game
    private int currentTurnPlayerId; //player who is on turn to play

    private IBrush[] playerColors = [Brushes.Blue, Brushes.Red, Brushes.Green, Brushes.Purple, Brushes.Yellow];
    

    public List<List<MapTile>> mapTiles = new List<List<MapTile>>(); //[row][col]

    public int GetMapSizeRow()
    {
        return mapSizeRow;
    }

    public int GetMapSizeCol()
    {
        return mapSizeCol;
    }


    //add function advancing the roundState

    //maybe input the path to the text file that needs to be read
    //for now some default values are getting loaded in
    public void LoadGameState()
    {
        //loaded values from the txt file
        int loadedMapSizeRow = 6;
        int loadedMapSizeCol = 6;
        int loadedPlayerIdCount = 3;
        int loadedCurrentTurnPlayerId = 0;

        mapSizeRow = loadedMapSizeRow;
        mapSizeCol = loadedMapSizeCol;
        playerIdCount = loadedPlayerIdCount;
        currentTurnPlayerId = loadedCurrentTurnPlayerId;

        for(int row = 0; row < mapSizeRow; row++)
        {
            var mapRowTiles = new List<MapTile>();
            for(int col = 0; col < mapSizeCol; col++)
            {
                //loaded data will determine the maptile setup
                MapTile mapTile = new MapTile(TileType.Empty, -1);

                mapRowTiles.Add(mapTile);
            }
            mapTiles.Add(mapRowTiles);
        }
    }
}