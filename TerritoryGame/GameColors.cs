
using Avalonia.Media;

class GameColors
{
    private IBrush[] playerColors = [Brushes.Blue, Brushes.Red, Brushes.Green, Brushes.Purple, Brushes.Yellow];
    private IBrush emptyTileColor = Brushes.DarkGray;
    private IBrush mountainTileColor = Brushes.Black;

    public IBrush GetEmptyTileColor()
    {   
        return emptyTileColor;
    }

    public IBrush GetMountainTileColor()
    {   
        return mountainTileColor;
    }


    public IBrush GetPlayerColorByPlayerId(int playerId)
    {
        return playerColors[playerId];
    }
}