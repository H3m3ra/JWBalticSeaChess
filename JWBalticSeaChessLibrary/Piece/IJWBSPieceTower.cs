namespace JWBalticSeaChessLibrary.Piece
{
    /// <summary>
    /// Basic interface for any piece tower.
    /// </summary>
    /// <typeparam name="T">Explicit type of top piece.</typeparam>
    /// <typeparam name="O">Explicit type of bottom pieces.</typeparam>
    public interface IJWBSPieceTower<T, O> : IJWBSPiece where T : IJWBSPiece where O : IJWBSPiece
    {
        public static readonly string ON_TOP_SYMBOL = "^";

        public T TopPiece { get; set; }
        public IList<IJWBSPiece> Pieces { get; set; }
        public IList<O> OtherPieces { get; set; }
    }
}