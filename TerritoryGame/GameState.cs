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
using Microsoft.Win32.SafeHandles;
using Avalonia.Platform;
using System.Data.Common;

class GameState
{
    private GameColors gameColors = new GameColors();
    private int mapSizeRow;
    private int mapSizeCol;  

    private int playerIdCount; //number of players in current game
    private int currentTurnPlayerId; //player who is on turn to play

    private List<PlayerInfo> playerInfos = new List<PlayerInfo>();
    private List<List<MapTile>> mapTiles = new List<List<MapTile>>(); //[row][col]

    private bool gameOver = false;
    public bool IsGameOver() { return gameOver; }
    public void SetGameOver() { gameOver = true; }

    // tracks players who have no legal moves left and are out of the game
    private List<int> eliminatedPlayers = new List<int>();
    public void EliminateCurrentPlayer() { eliminatedPlayers.Add(currentTurnPlayerId); } // marks current player as eliminated
    public bool IsPlayerEliminated(int id) { return eliminatedPlayers.Contains(id); } // checks if a player is eliminated
    public int GetEliminatedCount() { return eliminatedPlayers.Count; } // how many players are eliminated

    public int GetMapSizeRow() {return mapSizeRow;}

    public int GetMapSizeCol() {return mapSizeCol;}

    public int GetPlayerIdCount() {return playerIdCount;}

    public void AdvanceCurrentTurnPlayerId()
    {
        currentTurnPlayerId++;
        if(currentTurnPlayerId >= playerIdCount)
        {
            currentTurnPlayerId = 0;
        }
    }

    public int GetCurrenTurnPlayerId() {return currentTurnPlayerId;}

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
        int loadedLastPlayerId = int.Parse(firstLine[2]); 

        mapSizeRow = loadedMapSizeRow;
        mapSizeCol = loadedMapSizeCol;
        playerIdCount = loadedLastPlayerId; // default, in case the save file has no players yet
        currentTurnPlayerId = 0; // default to 0 since the save file doesnt include the data about this
        gameOver = false;
        eliminatedPlayers.Clear();

        //parsing written by AI
        var tilesValues = lines[1]  // +1 because line 0 is metadata
            .Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToArray();


        for(int row = 0; row < mapSizeRow; row++)
        {
            var mapRowTiles = new List<MapTile>();
            for (int col = 0; col < mapSizeCol; col++)
            {
                int value = 0;
                try {
                    value = tilesValues[row * mapSizeCol + col]; // This is the saved value for row, col
                } catch
                {
                    Console.WriteLine("Invalid save.txt format");
                    return;
                }
            
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

    public void SaveGameState()
    {
        /*
            expected format:

            .txt
            4 3 2
            0 0 2 0 0 1 2 0 1 1 2 0
            
            where:
            height width playerCount
            pixel_values

            values:
            0 - empty
            n - playerID + 1
            
            the n could also indicate the set number of players in the save
        */
        string filePath = AppContext.BaseDirectory + "save.txt"; //place the save.txt where your script run, for me TerritoryGame\bin\Debug\net10.0\save.txt
        
        // The first line
        var save = $"{mapSizeRow} {mapSizeCol} {playerIdCount}\n";
        
        foreach(List<MapTile> tileRow in mapTiles)
        {
            foreach (MapTile tile in tileRow)
            {
                if(tile.GetTileType() == TileType.PlayerOwned) save += $"{tile.GetOwnerId()} ";
                if(tile.GetTileType() == TileType.Empty) save += $"{-1} ";
                if(tile.GetTileType() == TileType.Mountain) save += $"{-2} ";
                
            }
        }
        
        File.WriteAllText(filePath, save);
    }


    //used for map creation
    public void CreateGameState(int mapSizeRowInput, int mapSizeColInput, int playerIdCountInput)
    {
        mapSizeRow = mapSizeRowInput;
        mapSizeCol = mapSizeColInput;
        playerIdCount = playerIdCountInput;
        currentTurnPlayerId = 0;
        gameOver = false;
        eliminatedPlayers.Clear();

        mapTiles.Clear();
        for (int row = 0; row < mapSizeRow; row ++)
        {
            var mapRowTiles = new List<MapTile>();
            for (int col = 0; col < mapSizeCol; col++)
                mapRowTiles.Add(new MapTile(-1, row, col));
            mapTiles.Add(mapRowTiles);
        }
        
        playerInfos.Clear();
        for (int i = 0; i < playerIdCount; i++)
            playerInfos.Add(new PlayerInfo(i));
    }

    // goes through all the tiles, checks if the empty tiles have a neighbouring player tile
    // if not, player can't place tile, so no legal moves
    public bool CurrentPlayerHasLegalMoves()
    {
        // return if the player doesnt have any tile yet
        if (GetPlayerPointById(currentTurnPlayerId) == 0) return true;

        foreach (List<MapTile> tileRow in mapTiles)
        {
            foreach (MapTile tile in tileRow)
            {
                // skip invalid tiles
                if (tile.GetTileType() != TileType.Empty)
                    continue;

                

                for(int adjacentRow =-1; adjacentRow < 2; adjacentRow++)
                {
                    for(int adjacentCol = -1; adjacentCol < 2; adjacentCol++)
                    {
                        MapTile adjacentMapTile = GetMapTile(tile.GetRowId() + adjacentRow, tile.GetColId() + adjacentCol);
                        if(adjacentMapTile == null) continue; //we dont want to consider "tiles" outside of bounds
                            

                        if(adjacentMapTile.GetOwnerId() == GetCurrenTurnPlayerId()) 
                            return true;
                    }
                }
            }
        }

        return false;
    }
    
    public List<(int id, int points)> GetPlayerScores()
    {
        var scores = new List<(int id, int points)>();
        for (int i = 0; i < playerIdCount; i++)
            scores.Add((i, playerInfos[i].pointCount));
        scores.Sort((a, b) => b.points.CompareTo(a.points));
        return scores;
    }
    
    public Avalonia.Media.IBrush GetPlayerGameColor(int playerId)
    {
        return gameColors.GetPlayerColorByPlayerId(playerId);
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