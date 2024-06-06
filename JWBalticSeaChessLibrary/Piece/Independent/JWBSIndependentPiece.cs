using JWBalticSeaChessLibrary.Player;

namespace JWBalticSeaChessLibrary.Piece.Independent
{
    public class JWBSIndependentPiece : IJWBSIndependentPiece
    {
        public JWBSPieceType PieceType { get; set; }
        public JWBSPlayerType PlayerType { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public Tuple<int, int> Position { get { return new Tuple<int, int>(X, Y); } set { X = value.Item1; Y = value.Item2; } }

        public JWBSIndependentPiece(JWBSPieceType pieceType, JWBSPlayerType playerType, int x, int y)
        {
            PieceType = pieceType;
            PlayerType = playerType;
            X = x;
            Y = y;
        }

        // get-methods
        public Tuple<int, int>[] GetPossiblePositions()
        {
            return IJWBSIndependentPiece.GetPossiblePositions(PieceType, PlayerType, X, Y);
        }
    }
}