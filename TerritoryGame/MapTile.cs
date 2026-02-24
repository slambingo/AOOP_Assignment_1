using Avalonia.Controls.Shapes;

class MapTile
{
    //the data each grid element should have
    private TileType type; // 
    private int ownerId; //-1 if nobody, 0,1,2... reserved for playerIds
    private Rectangle visual; //rectangle that displays the color

    public MapTile(TileType typeInput, int ownerIdInput)
    {
        type = typeInput;
        ownerId = ownerIdInput;
    }

    public void SetTileVisual(Rectangle visualInput)
    {
        visual = visualInput;
    }

}

public enum TileType
{
    PlayerOwned, //playerOwned
    Empty, 
    Mountain
}