using JWBalticSeaChessLibrary.Game;
using JWBalticSeaChessLibrary.Piece;
using JWBalticSeaChessLibrary.Piece.Independent;

namespace JWBalticSeaChessLibrary.Board
{
    public class JWBSPositionsBoard<T> : JWBSChessboardBase where T: IJWBSPiece 
    {
        public IList<IJWBSIndependentPiece>[] PlayerPieces { get; set; }

        // methods
        public override void Move(JWBSMove move)
        {
            throw new NotImplementedException();
        }

        // set-methods
        public override void SetPieceAt(Tuple<int, int> position, IJWBSPiece piece)
        {
            IJWBSIndependentPiece independentPiece = (IJWBSIndependentPiece)piece;
            PlayerPieces[(int)piece.PlayerType].Add(independentPiece);
        }

        // get-methods
        public override IJWBSPiece GetPieceAt(Tuple<int, int> position)
        {
            foreach (IList<IJWBSIndependentPiece> pieces in PlayerPieces)
            {
                foreach (IJWBSIndependentPiece piece in pieces)
                {
                    if (piece.X == position.Item1 && piece.Y == position.Item2)
                    {
                        return piece;
                    }
                }
            }
            return null;
        }
    }
}