using JWBalticSeaChessLibrary.Board;
using JWBalticSeaChessLibrary.Piece;
using JWBalticSeaChessLibrary.Piece.Passive;
using JWBalticSeaChessLibrary.Player;

namespace JWBalticSeaChessLibrary.Game
{
    public class JWBSActiveGame : IJWBSGame
    {
        public JWBSGameState State { get { return state; } }
        public IJWBSPlayer[] Players { get { return players; } }
        public IJWBSPlayer CurrentPlayer { get { return Players[(int)CurrentPlayerType]; } }
        public IJWBSChessboard Board { get { return board; } }
        public IList<IJWBSPieceTower<IJWBSPiece, IJWBSPiece>>[] PlayerPoints { get; set; }
        public JWBSPlayerType CurrentPlayerType { get; set; }
        public IList<JWBSMove> Moves { get { return (Board == null ? null : Board.Moves); } }
        public int RoundNumber { get { return (Moves.Count == 0 ? 0 : (int)Math.Ceiling((double)Moves.Count/2)); } }
        public int MoveNumber { get { return Moves.Count; } }

        protected JWBSGameState state = JWBSGameState.INIT;
        protected IJWBSPlayer[] players = new IJWBSPlayer[2];
        protected IJWBSChessboard board;
        protected IList<string> boardStateHistory;

        public JWBSActiveGame()
        {

        }

        // methods
        public bool Prepare()
        {
            if(IsReady() && State == JWBSGameState.INIT)
            {
                board = JWBSArrayBoard.GetRandomBoard();
                PlayerPoints  = new IList<IJWBSPieceTower<IJWBSPiece, IJWBSPiece>>[2] { new List<IJWBSPieceTower<IJWBSPiece, IJWBSPiece>>(), new List<IJWBSPieceTower<IJWBSPiece, IJWBSPiece>>() };
                CurrentPlayerType = JWBSPlayerType.ONE;
                state = JWBSGameState.READY;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Start()
        {
            if (State == JWBSGameState.READY)
            {
                state = JWBSGameState.RUNNING;
                Board.States.Add(Board.ToBoardStateString());
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Move(JWBSMove move)
        {
            board.Move(move);

            // score point by tower
            if (Board.GetPieceAt(move.TargetPosition) is JWBSPassivePieceTower && ((JWBSPassivePieceTower)Board.GetPieceAt(move.TargetPosition)).Pieces.Count >= IJWBSGame.TOWER_HEIGHT_MAX)
            {
                PlayerPoints[(int)CurrentPlayerType].Add(null);
                Board.SetPieceAt(move.TargetPosition, null);
            }

            // scroe point by baseline
            if(Board.GetPieceAt(move.TargetPosition) != null && Board.GetPieceAt(move.TargetPosition).PieceType != JWBSPieceType.SEAL)
            {
                if (CurrentPlayerType == JWBSPlayerType.ONE)
                {
                    if (move.TargetPosition.Item2 == 0)
                    {
                        PlayerPoints[(int)CurrentPlayerType].Add(null);
                        Board.SetPieceAt(move.TargetPosition, null);
                    }
                }
                else
                {
                    if (move.TargetPosition.Item2 == Board.Height - 1)
                    {
                        PlayerPoints[(int)CurrentPlayerType].Add(null);
                        Board.SetPieceAt(move.TargetPosition, null);
                    }
                }
            }

            CurrentPlayerType = (JWBSPlayerType)((1 + (int)CurrentPlayerType) % 2);
            return true;
        }

        // add-methods
        public bool AddPlayer(IJWBSPlayer player, JWBSPlayerType? playerType = null)
        {
            if (playerType == null)
            {
                if (!HasPlayer(JWBSPlayerType.ONE))
                {
                    Players[(int)JWBSPlayerType.ONE] = player;
                    player.CurrentGame = this;
                    return true;
                }
                else if (!HasPlayer(JWBSPlayerType.TWO))
                {
                    Players[(int)JWBSPlayerType.TWO] = player;
                    player.CurrentGame = this;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                JWBSPlayerType currentPlayerType = playerType.Value;
                if (HasPlayer(currentPlayerType))
                {
                    return false;
                }
                else
                {
                    Players[(int)currentPlayerType] = player;
                    player.CurrentGame = this;
                    return true;
                }
            }
        }

        public bool AddMove(JWBSMove move)
        {
            if (Move(move))
            {
                Moves.Add(move);
                return true;
            }
            return false;
        }

        // remove-methods
        public JWBSPlayerType? RemovePlayer(IJWBSPlayer player)
        {
            if(State == JWBSGameState.RUNNING)
            {
                state = JWBSGameState.CANCELLED;
            }
            else if (State == JWBSGameState.FINISHED)
            {
                
            }

            if (Players[(int)JWBSPlayerType.ONE] == player)
            {
                Players[(int)JWBSPlayerType.ONE] = null;
                return JWBSPlayerType.ONE;
            }
            else if (Players[(int)JWBSPlayerType.TWO] == player)
            {
                Players[(int)JWBSPlayerType.TWO] = null;
                return JWBSPlayerType.TWO;
            }
            else
            {
                return null;
            }
        }

        // get-methods
        public bool HasPlayer(JWBSPlayerType playerType)
        {
            return (Players[(int)playerType] != null);
        }

        public bool IsReady()
        {
            return HasPlayer(JWBSPlayerType.ONE) && HasPlayer(JWBSPlayerType.TWO);
        }
    }
}