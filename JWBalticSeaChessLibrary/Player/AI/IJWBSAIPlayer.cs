using JWBalticSeaChessLibrary.Game;

namespace JWBalticSeaChessLibrary.Player.AI
{
    public interface IJWBSAIPlayer : IJWBSPlayer
    {
        public TimeSpan MaxRespondTime { get; set; }

        // get-methods
        public JWBSMove GetMove();
    }
}