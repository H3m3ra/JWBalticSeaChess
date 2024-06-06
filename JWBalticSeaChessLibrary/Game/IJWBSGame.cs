using JWBalticSeaChessLibrary.Board;
using JWBalticSeaChessLibrary.Piece;
using JWBalticSeaChessLibrary.Piece.Passive;
using JWBalticSeaChessLibrary.Player;

namespace JWBalticSeaChessLibrary.Game
{
    public interface IJWBSGame
    {
        public static readonly int TOWER_HEIGHT_MAX = 3;

        public JWBSGameState State { get; }
        public IJWBSPlayer[] Players { get; }
        public IJWBSPlayer CurrentPlayer { get; }
        public IJWBSChessboard Board { get; }
        public IList<IJWBSPieceTower<IJWBSPiece, IJWBSPiece>>[] PlayerPoints { get; set; }
        public JWBSPlayerType CurrentPlayerType { get; set; }
        public IList<JWBSMove> Moves { get; }

        // methods
        public bool Prepare();

        public bool Start();

        public bool Move(JWBSMove move);

        // add-methods
        public bool AddPlayer(IJWBSPlayer player, JWBSPlayerType? playerType = null);

        // set-methods


        // remove-methods
        public JWBSPlayerType? RemovePlayer(IJWBSPlayer player);
    }
}