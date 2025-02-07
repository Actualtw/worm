namespace SnakeGame
{
    /// <summary>
    /// Kuvaa tyhjää solua gridillä
    /// </summary>
    public class EmptyCell : ICellOccupier
    {
        public CellOccupierType Type => CellOccupierType.None;
    }
}
