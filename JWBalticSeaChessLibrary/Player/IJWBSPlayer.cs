using JWBalticSeaChessLibrary.Game;
using JWBalticSeaChessLibrary.Piece;

namespace JWBalticSeaChessLibrary.Player
{
    public interface IJWBSPlayer
    {
        public string Name { get; set; }
        public IJWBSGame CurrentGame { get; set; }
        public JWBSPlayerType CurrentPlayerType { get; }
        public IList<IJWBSPieceTower<IJWBSPiece, IJWBSPiece>> CurrentPlayerPoints { get; }
    }
}