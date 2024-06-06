using JWBalticSeaChessLibrary.Piece.Passive;
using JWBalticSeaChessLibrary.Player;

namespace JWBalticSeaChessLibrary.Piece.Independent
{
    public class JWBSIndependentPieceTower : IJWBSIndependentPieceTower<JWBSIndependentPiece, JWBSPassivePiece>
    {
        public JWBSPieceType PieceType { get { return TopPiece.PieceType; } set { TopPiece.PieceType = value; } }
        public JWBSPlayerType PlayerType { get { return TopPiece.PlayerType; } set { TopPiece.PlayerType = value; } }
        public int X { get { return TopPiece.X; } set { TopPiece.X = value; } }
        public int Y { get { return TopPiece.Y; } set { TopPiece.Y = value; } }
        public Tuple<int, int> Position { get { return TopPiece.Position; } set { TopPiece.Position = value; } }
        public JWBSIndependentPiece TopPiece { get { return (JWBSIndependentPiece)Pieces.Last(); } set { Pieces[Pieces.Count] = value; } }
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
                JWBSIndependentPiece topPiece = TopPiece;
                Pieces = (IList<IJWBSPiece>)value;
                Pieces.Add(topPiece);
            }
        }

        public JWBSIndependentPieceTower(IList<IJWBSPiece> pieces)
        {
            Pieces = pieces;
        }

        // static-to-methods
        public static JWBSIndependentPieceTower ToTowerFromPiece(IJWBSPiece piece)
        {
            if (piece == null || piece is JWBSIndependentPieceTower)
            {
                return (JWBSIndependentPieceTower)piece;
            }
            else
            {
                return new JWBSIndependentPieceTower(new List<IJWBSPiece>() { piece });
            }
        }

        // get-methods
        public Tuple<int, int>[] GetPossiblePositions()
        {
            return TopPiece.GetPossiblePositions();
        }
    }
}