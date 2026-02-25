//this will store the current game state, which then a new one can be loaded 
//will store:
//map
//number of players
//current player
//is the game still ongoing



using System.Dynamic;
using Avalonia.Controls.Shapes;
using Avalonia.Media;
using Avalonia.Metadata;
using System.IO;
using System.Drawing;

class GameState
{

    private GameColors gameColors = new GameColors();
    private int mapSizeRow;
    private int mapSizeCol;  

    private int playerIdCount; //number of players in current game
    private int currentTurnPlayerId; //player who is on turn to play

    private List<PlayerInfo> playerInfos = new List<PlayerInfo>();

    private List<List<MapTile>> mapTiles = new List<List<MapTile>>(); //[row][col]

    public int GetMapSizeRow()
    {
        return mapSizeRow;
    }

    public int GetMapSizeCol()
    {
        return mapSizeCol;
    }

    public void AdvanceCurrentTurnPlayerId()
    {
        currentTurnPlayerId++;
        if(currentTurnPlayerId >= playerIdCount)
        {
            currentTurnPlayerId = 0;
        }
    }

    public int GetCurrenTurnPlayerId()
    {
        return currentTurnPlayerId;
    }

    public IBrush GetCurrentTurnPlayerColor()
    {
        return gameColors.GetPlayerColorByPlayerId(currentTurnPlayerId);
    }

    public MapTile GetMapTile(int rowInput, int colInput)
    {
        if(rowInput < 0 || rowInput >= mapSizeRow) return null;
        if(colInput < 0 || colInput >= mapSizeCol) return null;

        return mapTiles[rowInput][colInput];
    }

    public int GetPlayerPointById(int playerId)
    {
        if(playerId < 0 || playerId >= playerIdCount) return -1;
        return playerInfos[playerId].pointCount;
    }

    public void AwardPointToPlayerById(int playerId)
    {
        playerInfos[playerId].pointCount++;
    }

    //add function advancing the roundState
    //maybe input the path to the text file that needs to be read
    //for now some default values are getting loaded in
    public void LoadGameState()
    {
        
        string filePath = AppContext.BaseDirectory + "save.txt"; //place the save.txt where your script run, for me TerritoryGame\bin\Debug\net10.0\save.txt
        string[] lines;

        //try, catch exception by AI
        try
        {
            lines = File.ReadAllLines(filePath);
        }
        catch(FileNotFoundException)
        {
            Console.WriteLine($"Error: File not found: {filePath}");
            return; // exit or handle as needed
        }
        
        //parsing written by AI
        string[] firstLine = lines[0].Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

        //loaded values from the txt file
        int loadedMapSizeRow = int.Parse(firstLine[0]);
        int loadedMapSizeCol = int.Parse(firstLine[1]);
        int loadedPlayerIdCount = int.Parse(firstLine[2]);
        int loadedCurrentTurnPlayerId = int.Parse(firstLine[3]);

        mapSizeRow = loadedMapSizeRow;
        mapSizeCol = loadedMapSizeCol;
        playerIdCount = loadedPlayerIdCount;
        currentTurnPlayerId = loadedCurrentTurnPlayerId;

        for(int row = 0; row < mapSizeRow; row++)
        {
            //parsing written by AI
            var tilesValues = lines[row + 1]  // +1 because line 0 is metadata
                .Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            var mapRowTiles = new List<MapTile>();

            for (int col = 0; col < mapSizeCol; col++)
            {
                int value = tilesValues[col]; // This is the saved value for row, col

                MapTile mapTile = new MapTile(value, row, col);

                mapRowTiles.Add(mapTile);
            }

            mapTiles.Add(mapRowTiles);
        }


        //manage player list
        playerInfos.Clear();
        for(int i = 0; i < playerIdCount; i++)
        {
            PlayerInfo newPlayerInfo = new PlayerInfo(i);
            playerInfos.Add(newPlayerInfo);
        }
        //assign points from loaded state
        for(int row = 0; row < mapSizeRow; row++) 
        {
            for (int col = 0; col < mapSizeCol; col++)
            {
                if(GetMapTile(row,col).GetTileType() == TileType.PlayerOwned)
                {
                    playerInfos[GetMapTile(row,col).GetOwnerId()].pointCount++;
                }
            }
        }


    }



}

class PlayerInfo
{
    public int id = -1;
    public int pointCount = 0;

    public PlayerInfo(int idInput)
    {
        id = idInput;
        pointCount = 0;
    }
}