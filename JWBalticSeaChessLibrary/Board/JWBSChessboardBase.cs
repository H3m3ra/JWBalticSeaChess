using JWBalticSeaChessLibrary.Game;
using JWBalticSeaChessLibrary.Piece;
using JWBalticSeaChessLibrary.Player;
using System.Text;

namespace JWBalticSeaChessLibrary.Board
{
    public abstract class JWBSChessboardBase : IJWBSChessboard
    {
        public virtual int Width { get; set; }
        public virtual int Height { get; set; }
        public virtual IList<string> States { get; set; } = new List<string>();
        public virtual IList<JWBSMove> Moves { get; set; } = new List<JWBSMove>();

        // methods
        public abstract void Move(JWBSMove move);

        // set-methods
        public abstract void SetPieceAt(Tuple<int, int> position, IJWBSPiece move);

        // to-methods
        public string ToBoardStateString()
        {
            StringBuilder buffer = new StringBuilder();
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    buffer.Append(IJWBSPiece.ToStringFromPiece(GetPieceAt(new Tuple<int, int>(j, i))));
                }
            }
            return buffer.ToString();
        }

        // get-methods
        public virtual IList<Tuple<int, int>> GetAllowedMoves(IJWBSPiece piece, IList<Tuple<int, int>> possibleMoves)
        {
            IList<Tuple<int, int>> results = new List<Tuple<int, int>>(possibleMoves.Count);
            foreach (Tuple<int, int> possibleMove in possibleMoves)
            {
                if (
                    possibleMove.Item1 >= 0 && possibleMove.Item1 < Width &&
                    possibleMove.Item2 >= 0 && possibleMove.Item2 < Height &&
                    (GetPieceAt(possibleMove) == null || GetPieceAt(possibleMove).PlayerType != piece.PlayerType)
                )
                {
                    bool uniqueState;
                    {
                        JWBSArrayBoard board = new JWBSArrayBoard();
                        board.Pieces = new IJWBSPiece[Height][];
                        for (int i = 0; i < Height; i++)
                        {
                            board.Pieces[i] = new IJWBSPiece[Width];
                            for (int j = 0; j < Width; j++)
                            {
                                board.Pieces[i][j] = GetPieceAt(new Tuple<int, int>(j, i));
                            }
                        }
                        //board.Move(new JWBSMove(possibleMove));
                        uniqueState = !States.Contains(board.ToBoardStateString());
                    }

                    if (uniqueState)
                    {

                    }

                    results.Add(possibleMove);
                }
            }
            return results;
        }

        public IList<IJWBSPiece> GetPiecesOf(JWBSPlayerType playerType)
        {
            IList<IJWBSPiece> pieces = new List<IJWBSPiece>();
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    IJWBSPiece piece = GetPieceAt(new Tuple<int, int>(j, i));
                    if (piece.PlayerType == playerType)
                    {
                        pieces.Add(piece);
                    }
                }
            }
            return pieces;
        }

        public abstract IJWBSPiece GetPieceAt(Tuple<int, int> position);
    }
}