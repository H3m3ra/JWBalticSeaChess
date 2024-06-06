using JWBalticSeaChessLibrary.Player;
using System.Text;

namespace JWBalticSeaChessLibrary.Piece
{
    /// <summary>
    /// Basic interface for any piece.
    /// </summary>
    public interface IJWBSPiece
    {
        /// <summary>
        /// Text representation with simple self explaining 
        /// </summary>
        public static readonly string[] PIECE_NAMES = new string[] { "v", "+", "*", "L" };
        /// <summary>
        /// Text representation with unique identifer for piece and player type.
        /// </summary>
        public static readonly string[][] PIECE_SYMBOLS = new string[][]
        {
            new string[] { "H", "G", "F", "L" },
            new string[] { "H", "g", "f", "l" },
        };

        public JWBSPieceType PieceType { get; set; }
        public JWBSPlayerType PlayerType { get; set; }

        // static-to-methods
        public static string ToStringFromPiece(IJWBSPiece piece)
        {
            if (piece == null)
            {
                return "-";
            }
            else if (piece is IJWBSPieceTower<IJWBSPiece, IJWBSPiece>)
            {
                StringBuilder buffer = new StringBuilder();
                foreach (IJWBSPiece subPiece in ((IJWBSPieceTower<IJWBSPiece, IJWBSPiece>)piece).Pieces)
                {
                    buffer.Append(ToStringFromPiece(subPiece));
                    buffer.Append(IJWBSPieceTower<IJWBSPiece, IJWBSPiece>.ON_TOP_SYMBOL);
                }
                int onTopSymbolLength = IJWBSPieceTower<IJWBSPiece, IJWBSPiece>.ON_TOP_SYMBOL.Length;
                buffer.Remove(buffer.Length - onTopSymbolLength, onTopSymbolLength);
                return buffer.ToString();
            }
            else
            {
                return PIECE_SYMBOLS[(int)piece.PlayerType][(int)piece.PieceType];
            }
        }
    }
}