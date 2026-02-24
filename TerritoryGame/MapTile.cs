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
    //the data each grid element should have
    private TileType type; // 
    private int ownerId; //-1 if nobody, 0,1,2... reserved for playerIds
    private Rectangle visual; //rectangle that displays the color

    
    private IBrush emptyTileColor = Brushes.DarkGray;
    private IBrush mountainTileColor = Brushes.Black;

    public MapTile(TileType typeInput, int ownerIdInput)
    {
        type = typeInput;
        ownerId = ownerIdInput;
    }

    public void SetTileVisual(Rectangle visualInput)
    {
        visual = visualInput;
    }

    public IBrush GetTileColor()
    {
        if(type == TileType.Empty)
        {
            return emptyTileColor;
        }
        else if(type == TileType.Mountain)
        {
            return mountainTileColor;
        }

        return emptyTileColor; //idk what to return here

        
    }

}

public enum TileType
{
    PlayerOwned, //playerOwned
    Empty, 
    Mountain
}