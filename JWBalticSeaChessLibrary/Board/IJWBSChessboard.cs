using JWBalticSeaChessLibrary.Game;
using JWBalticSeaChessLibrary.Piece;
using JWBalticSeaChessLibrary.Player;

namespace JWBalticSeaChessLibrary.Board
{
    public interface IJWBSChessboard
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public IList<string> States { get; set; }
        public IList<JWBSMove> Moves { get; set; }

        // methods
        public void Move(JWBSMove move);

        // set-methods
        public void SetPieceAt(Tuple<int, int> position, IJWBSPiece piece);

        // to-methods
        public string ToBoardStateString();

        // get-methods
        public IList<Tuple<int, int>> GetAllowedMoves(IJWBSPiece piece, IList<Tuple<int, int>> possibleMoves);

        public IList<IJWBSPiece> GetPiecesOf(JWBSPlayerType playerType);

        public IJWBSPiece GetPieceAt(Tuple<int, int> position);
    }
}