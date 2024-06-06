using JWBalticSeaChessLibrary.Piece.Independent;
using JWBalticSeaChessLibrary.Player;

namespace JWBalticSeaChessLibrary.Piece.Passive
{
    public class JWBSPassivePieceTower : IJWBSPieceTower<JWBSPassivePiece, JWBSPassivePiece>
    {
        public JWBSPieceType PieceType { get { return TopPiece.PieceType; } set { TopPiece.PieceType = value; } }
        public JWBSPlayerType PlayerType { get { return TopPiece.PlayerType; } set { TopPiece.PlayerType = value; } }
        public JWBSPassivePiece TopPiece { get { return (JWBSPassivePiece)Pieces.Last(); } set { Pieces[Pieces.Count] = value; } }
        public IList<IJWBSPiece> Pieces { get; set; }
        public IList<JWBSPassivePiece> OtherPieces
        {
            get
            {
                IList<JWBSPassivePiece> result = new List<JWBSPassivePiece>(Pieces.Count - 1);
                for (int p = 0; p < Pieces.Count - 1; p++)
                {
                    result.Add((JWBSPassivePiece)Pieces[p]);
                }
                return result;
            }
            set
            {
                JWBSPassivePiece topPiece = TopPiece;
                Pieces = (IList<IJWBSPiece>)value;
                Pieces.Add(topPiece);
            }
        }

        public JWBSPassivePieceTower(IList<IJWBSPiece> pieces)
        {
            Pieces = pieces;
        }

        // static-to-methods
        public static JWBSPassivePieceTower ToTowerFromPiece(IJWBSPiece piece)
        {
            if (piece == null || piece is JWBSPassivePieceTower)
            {
                return (JWBSPassivePieceTower)piece;
            }
            else
            {
                return new JWBSPassivePieceTower(new List<IJWBSPiece>() { piece });
            }
        }
    }
}