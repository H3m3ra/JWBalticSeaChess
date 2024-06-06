using JWBalticSeaChessLibrary.Board;
using JWBalticSeaChessLibrary.Piece;
using System;

namespace JWBalticSeaChessLibrary.Game
{
    public class JWBSMove
    {
        public static readonly string COLUMN_NAMES = "abcdefghijklmnopqrst";

        public Tuple<int, int> SourcePosition { get; set; }
        public Tuple<int, int> TargetPosition { get; set; }
        public string Name { get; set; }

        public JWBSMove(Tuple<int, int> sourcePosition, Tuple<int, int> targetPosition, string name)
        {
            SourcePosition = sourcePosition;
            TargetPosition = targetPosition;
            Name = name;
        }

        // static-methods
        public static string GetMoveName(IJWBSChessboard board, Tuple<int, int> sourcePosition, IJWBSPiece sourcePiece, Tuple<int, int> targetPosition, IJWBSPiece targetPiece)
        {
            return (
                    IJWBSPiece.PIECE_NAMES[(int)sourcePiece.PieceType] + COLUMN_NAMES[targetPosition.Item1] + (board.Height-1-targetPosition.Item2)+
                    (targetPiece == null ? "" : "^"+(targetPiece is IJWBSPieceTower<IJWBSPiece, IJWBSPiece> ? "^" : ""))
                );
        }
    }
}