using System.ComponentModel;
using Avalonia.Controls.Shapes;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.PropertyStore;

class MapTile
{
    
    private TileType type; //since the save.txt can contain only integers, 0,1,2... reserved for player, -1 empty, -2 mountain
    private int ownerId; //-1 if nobody, 0,1,2... reserved for playerIds
    private Button visual; //button that displays the color

    private int rowId;
    private int colId;
    
    private IBrush color;

    private GameColors gameColors = new GameColors();

    public MapTile(int tileTypeId, int tileRowId, int tileColId)
    {
        if(tileTypeId >= 0)
        {
            type = TileType.PlayerOwned;
            ownerId = tileTypeId;
            color = gameColors.GetPlayerColorByPlayerId(ownerId);
        }
        else if(tileTypeId == -1)
        {
            type = TileType.Empty;
            color = gameColors.GetEmptyTileColor();
            ownerId = -1;
        }
        else if(tileTypeId == -2)
        {
            type = TileType.Mountain;
            color = gameColors.GetMountainTileColor();
            ownerId = -1;
        }
        
        rowId = tileRowId;
        colId = tileColId; 
    }

    public void SetTileVisual(Button visualInput)
    {
        visual = visualInput;
    }

    public Button GetTileVisual()
    {
        return visual;
    }

    public IBrush GetColor()
    {
        return color; 
    }

    public int GetRowId()
    {
        return rowId;
    }

    public int GetColId()
    {
        return colId;
    }

    public int GetOwnerId()
    {
        return ownerId;
    }

    public TileType GetTileType()
    {
        return type;
    }

    public void UpdateTileAfterPressed(int nextOwnerId)
    {
        type = TileType.PlayerOwned;
        ownerId = nextOwnerId;
        color = gameColors.GetPlayerColorByPlayerId(ownerId);
    }

}

public enum TileType
{
    PlayerOwned,
    Empty, 
    Mountain
}