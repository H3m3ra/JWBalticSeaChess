using JWBalticSeaChessLibrary.Game;
using JWBalticSeaChessLibrary.Piece;

namespace JWBalticSeaChessLibrary.Player
{
    public abstract class JWBSPlayerBase : IJWBSPlayer
    {
        public string Name { get; set; }
        public IJWBSGame CurrentGame { get; set; }
        public JWBSPlayerType CurrentPlayerType
        {
            get
            {
                return (CurrentGame.Players[0] == this ? JWBSPlayerType.ONE : JWBSPlayerType.TWO);
            }
        }
        public IList<IJWBSPieceTower<IJWBSPiece, IJWBSPiece>> CurrentPlayerPoints
        {
            get
            {
                return CurrentGame.PlayerPoints[(int)CurrentPlayerType];
            }
        }

        public JWBSPlayerBase(string name)
        {
            Name = name;
        }
    }
}