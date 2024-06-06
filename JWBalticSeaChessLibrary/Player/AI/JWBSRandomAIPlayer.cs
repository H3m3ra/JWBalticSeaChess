using JWBalticSeaChessLibrary.Game;
using JWBalticSeaChessLibrary.Piece;
using JWBalticSeaChessLibrary.Piece.Independent;

namespace JWBalticSeaChessLibrary.Player.AI
{
    public class JWBSRandomAIPlayer : JWBSPlayerBase, IJWBSAIPlayer
    {
        public TimeSpan MaxRespondTime { get; set; }

        public JWBSRandomAIPlayer(string name) : base(name)
        {
            Name = name;
        }

        // get-methods
        public JWBSMove GetMove()
        {
            IList<JWBSMove> allowedMoves = new List<JWBSMove>();
            for (int i = 0; i < CurrentGame.Board.Height; i++)
            {
                for (int j = 0; j < CurrentGame.Board.Width; j++)
                {
                    Tuple<int, int> sourcePosition = new Tuple<int, int>(j, i);
                    IJWBSPiece piece = CurrentGame.Board.GetPieceAt(sourcePosition);
                    if (piece != null && piece.PlayerType == CurrentPlayerType)
                    {
                        foreach(Tuple<int, int> targetPosition in CurrentGame.Board.GetAllowedMoves(
                            piece,
                            IJWBSIndependentPiece.GetPossiblePositions(piece.PieceType, piece.PlayerType, j, i)
                        ))
                        {
                            allowedMoves.Add(new JWBSMove(sourcePosition, targetPosition, JWBSMove.GetMoveName(CurrentGame.Board,
                                sourcePosition, piece,
                                targetPosition, CurrentGame.Board.GetPieceAt(targetPosition)
                            )));
                        }
                    }
                }
            }
            return allowedMoves[new Random().Next(0, allowedMoves.Count)];
        }
    }
}