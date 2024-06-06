using System;
using System.Text;
using JWBalticSeaChessLibrary.Game;
using JWBalticSeaChessLibrary.Piece;
using JWBalticSeaChessLibrary.Piece.Passive;
using JWBalticSeaChessLibrary.Player;

namespace JWBalticSeaChessLibrary.Board
{
    public class JWBSArrayBoard : JWBSChessboardBase
    {
        public static int SIZE = 8;
        public static int TOWER_LIMIT = 3;
        private static Dictionary<char, Tuple<JWBSPieceType, JWBSPlayerType>> PIECE_SOURCE_MAPPING = new Dictionary<char, Tuple<JWBSPieceType, JWBSPlayerType>>()
        {
            {'H', new Tuple<JWBSPieceType, JWBSPlayerType>(JWBSPieceType.SHELL, JWBSPlayerType.TWO) },
            {'h', new Tuple<JWBSPieceType, JWBSPlayerType>(JWBSPieceType.SHELL, JWBSPlayerType.ONE) },
            {'G', new Tuple<JWBSPieceType, JWBSPlayerType>(JWBSPieceType.SEAGULL, JWBSPlayerType.TWO) },
            {'g', new Tuple<JWBSPieceType, JWBSPlayerType>(JWBSPieceType.SEAGULL, JWBSPlayerType.ONE) },
            {'F', new Tuple<JWBSPieceType, JWBSPlayerType>(JWBSPieceType.STARFISH, JWBSPlayerType.TWO) },
            {'f', new Tuple<JWBSPieceType, JWBSPlayerType>(JWBSPieceType.STARFISH, JWBSPlayerType.ONE) },
            {'L', new Tuple<JWBSPieceType, JWBSPlayerType>(JWBSPieceType.SEAL, JWBSPlayerType.TWO) },
            {'l', new Tuple<JWBSPieceType, JWBSPlayerType>(JWBSPieceType.SEAL, JWBSPlayerType.ONE) }
        };

        public IJWBSPiece[][] Pieces { get; set; }
        public override int Width { get { return Pieces[0].Length; } set { } }
        public override int Height { get { return Pieces.Length; } set { } }

        public JWBSArrayBoard()
        {

        }

        // static-to-methods
        public static JWBSArrayBoard ToChessFromString(string source)
        {
            JWBSArrayBoard chess = new JWBSArrayBoard();

            IList<string> lines = source.Split("\n").ToList();
            //chess.PlayerPoints[0] = (from s in lines.First().Split(";") select (JWBSPassivePieceTower)ToPieceFromString(s)).ToList();
            lines.RemoveAt(0);
            //chess.PlayerPoints[1] = (from s in lines.First().Split(";") select (JWBSPassivePieceTower)ToPieceFromString(s)).ToList();
            lines.RemoveAt(0);
            chess.Pieces = (from l in lines select (from s in l.Split(";") select ToPieceFromString(s)).ToArray()).ToArray();
            return chess;
        }

        public static IJWBSPiece ToPieceFromString(string source)
        {
            if (source.Length > 1)
            {
                JWBSPassivePieceTower result = new JWBSPassivePieceTower(new List<IJWBSPiece>(4));
                foreach (char subSource in source)
                {
                    result.Pieces.Add(ToPieceFromString(subSource.ToString()));
                }
                return result;
            }
            else if (source == null || source.Length == 0 || !PIECE_SOURCE_MAPPING.ContainsKey(source[0]))
            {
                return null;
            }
            else
            {
                return new JWBSPassivePiece(PIECE_SOURCE_MAPPING[source[0]].Item1, PIECE_SOURCE_MAPPING[source[0]].Item2);
            }
        }

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
                return IJWBSPiece.PIECE_SYMBOLS[(int)piece.PlayerType][(int)piece.PieceType];
            }
        }

        // get-methods
        public static JWBSArrayBoard GetRandomBoard()
        {
            JWBSArrayBoard board = new JWBSArrayBoard();
            board.Pieces = new IJWBSPiece[8][];
            for (int i = 0; i < board.Height; i++)
            {
                board.Pieces[i] = new IJWBSPiece[8];
            }
            IList<JWBSPieceType> possiblePieceTypes = new List<JWBSPieceType>()
            {
                JWBSPieceType.SHELL,
                JWBSPieceType.SHELL,
                JWBSPieceType.SEAGULL,
                JWBSPieceType.SEAGULL,
                JWBSPieceType.STARFISH,
                JWBSPieceType.STARFISH,
                JWBSPieceType.SEAL,
                JWBSPieceType.SEAL
            };
            Random random = new Random();
            for(int j = 0; j < board.Width; j++)
            {
                int index = random.Next(0, possiblePieceTypes.Count());
                board.Pieces[board.Height - 1][j] = new JWBSPassivePiece(possiblePieceTypes[index], JWBSPlayerType.ONE);
                board.Pieces[0][board.Width - 1-j] = new JWBSPassivePiece(possiblePieceTypes[index], JWBSPlayerType.TWO);
                possiblePieceTypes.RemoveAt(index);
            }
            return board;
        }

        // methods
        public override void Move(JWBSMove move)
        {
            if (GetPieceAt(move.TargetPosition) == null)
            {
                SetPieceAt(move.TargetPosition, GetPieceAt(move.SourcePosition));
            }
            else
            {
                JWBSPassivePieceTower currentTower = JWBSPassivePieceTower.ToTowerFromPiece(GetPieceAt(move.TargetPosition));
                foreach (IJWBSPiece subPiece in JWBSPassivePieceTower.ToTowerFromPiece(GetPieceAt(move.SourcePosition)).Pieces)
                {
                    currentTower.Pieces.Add(subPiece);
                }
                SetPieceAt(move.TargetPosition, currentTower);
            }
            SetPieceAt(move.SourcePosition, null);

            States.Add(ToBoardStateString());
        }

        // set-methods
        public override void SetPieceAt(Tuple<int, int> position, IJWBSPiece piece)
        {
            Pieces[position.Item2][position.Item1] = piece;
        }

        // to-methods
        public override string ToString()
        {
            StringBuilder buffer = new StringBuilder();
            /*for (int f = 0; f < PlayerPoints[0].Count; f++)
            {
                if (f > 0)
                {
                    buffer.Append(';');
                }
                buffer.Append(ToStringFromPiece(PlayerPoints[0][f]));
            }
            buffer.Append('\n');
            for (int s = 0; s < PlayerPoints[1].Count; s++)
            {
                if (s > 0)
                {
                    buffer.Append(';');
                }
                buffer.Append(ToStringFromPiece(PlayerPoints[1][s]));
            }*/
            for (int i = 0; i < Pieces.Length; i++)
            {
                buffer.Append('\n');
                for (int j = 0; j < Pieces.Length; j++)
                {
                    if (j > 0)
                    {
                        buffer.Append(';');
                    }
                    buffer.Append(ToStringFromPiece(Pieces[i][j]));
                }
            }
            return buffer.ToString();
        }

        // get-methods
        public IList<JWBSMove> GetNextMoves(TimeSpan evaluationTime)
        {
            DateTime start = DateTime.Now;

            IList<JWBSMove> resultMoves = new List<JWBSMove>();
            for (int i = 0; i < 10; i++)
            {

            }
            return null;
        }

        public override IJWBSPiece GetPieceAt(Tuple<int, int> position)
        {
            return (position == null ? null : Pieces[position.Item2][position.Item1]);
        }
    }
}