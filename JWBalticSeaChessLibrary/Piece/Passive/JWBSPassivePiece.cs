using JWBalticSeaChessLibrary.Piece.Independent;
using JWBalticSeaChessLibrary.Player;

namespace JWBalticSeaChessLibrary.Piece.Passive
{
    public class JWBSPassivePiece : IJWBSPiece
    {
        public JWBSPieceType PieceType { get; set; }
        public JWBSPlayerType PlayerType { get; set; }

        public JWBSPassivePiece(JWBSPieceType pieceType, JWBSPlayerType playerType)
        {
            PieceType = pieceType;
            PlayerType = playerType;
        }

        // get-methods
        public Tuple<int, int>[] GetPossiblePositions(int x, int y)
        {
            return IJWBSIndependentPiece.GetPossiblePositions(PieceType, PlayerType, x, y);
        }
    }
}