using JWBalticSeaChessLibrary.Player;

namespace JWBalticSeaChessLibrary.Piece.Independent
{
    public interface IJWBSIndependentPiece : IJWBSPiece
    {
        /// <summary>
        /// Possible relative moves for every type <see cref="JWBSPieceType"/>
        /// of player one from bottom to top.
        /// </summary>
        public static readonly Tuple<int, int>[][] POSSIBLE_MOVES_PLAYER_ONE = new Tuple<int, int>[][]
        {
            // Shell
            new Tuple<int, int>[]{
                new Tuple<int, int>(-1, -1),
                new Tuple<int, int>(1, -1)
            },
            // Seagull
            new Tuple<int, int>[]{
                new Tuple<int, int>(0, -1),
                new Tuple<int, int>(1, 0),
                new Tuple<int, int>(0, 1),
                new Tuple<int, int>(-1, 0),
            },
            // Starfish
            new Tuple<int, int>[]{
                new Tuple<int, int>(-1, -1),
                new Tuple<int, int>(0, -1),
                new Tuple<int, int>(1, -1),
                new Tuple<int, int>(-1, 1),
                new Tuple<int, int>(1, 1),
            },
            // Seal
            new Tuple<int, int>[]{
                new Tuple<int, int>(-2, -1),
                new Tuple<int, int>(-1, -2),
                new Tuple<int, int>(1, -2),
                new Tuple<int, int>(2, -1),
                new Tuple<int, int>(-2, 1),
                new Tuple<int, int>(-1, 2),
                new Tuple<int, int>(1, 2),
                new Tuple<int, int>(2, 1),
            }
        };
        /// <summary>
        /// Possible relative moves for every type <see cref="JWBSPieceType"/>
        /// of player two from top to bottom.
        /// Could be generated of <c>POSSIBLE_MOVES_PLAYER_ONE</c> by swap sign with f:
        ///     f(0) = 1, f(1) = -1 _>
        ///     f(x) = -(2*x-1)
        /// </summary>
        public static readonly Tuple<int, int>[][] POSSIBLE_MOVES_PLAYER_TWO = new Tuple<int, int>[][]
        {
            // Shell
            new Tuple<int, int>[]{
                new Tuple<int, int>(-1, 1),
                new Tuple<int, int>(1, 1)
            },
            // Seagull
            new Tuple<int, int>[]{
                new Tuple<int, int>(0, 1),
                new Tuple<int, int>(1, 0),
                new Tuple<int, int>(0, -1),
                new Tuple<int, int>(-1, 0),
            },
            // Starfish
            new Tuple<int, int>[]{
                new Tuple<int, int>(-1, 1),
                new Tuple<int, int>(0, 1),
                new Tuple<int, int>(1, 1),
                new Tuple<int, int>(-1, -1),
                new Tuple<int, int>(1, -1),
            },
            // Seal
            new Tuple<int, int>[]{
                new Tuple<int, int>(-2, 1),
                new Tuple<int, int>(-1, 2),
                new Tuple<int, int>(1, 2),
                new Tuple<int, int>(2, 1),
                new Tuple<int, int>(-2, -1),
                new Tuple<int, int>(-1, -2),
                new Tuple<int, int>(1, -2),
                new Tuple<int, int>(2, -1),
            }
        };
        public int X { get; set; }
        public int Y { get; set; }
        public Tuple<int, int> Position { get; set; }

        // static-methods
        public static Tuple<int, int>[] GetPossiblePositions(JWBSPieceType pieceType, JWBSPlayerType playerType, int x, int y)
        {
            Tuple<int, int>[] results = new Tuple<int, int>[GetPossiblePositionsCount(pieceType)];
            for (int i = 0; i < results.Length; i++)
            {
                results[i] = new Tuple<int, int>(
                    x + (playerType == JWBSPlayerType.ONE ? POSSIBLE_MOVES_PLAYER_ONE : POSSIBLE_MOVES_PLAYER_TWO)[(int)pieceType][i].Item1,
                    y + (playerType == JWBSPlayerType.ONE ? POSSIBLE_MOVES_PLAYER_ONE : POSSIBLE_MOVES_PLAYER_TWO)[(int)pieceType][i].Item2
                );
            }
            return results;
        }

        public static int GetPossiblePositionsCount(JWBSPieceType type)
        {
            return POSSIBLE_MOVES_PLAYER_ONE[(int)type].Length;
        }

        // get-methods
        public Tuple<int, int>[] GetPossiblePositions();
    }
}
