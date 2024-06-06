namespace JWBalticSeaChessLibrary.Piece.Independent
{
    public interface IJWBSIndependentPieceTower<T, O> : IJWBSPieceTower<T, O>, IJWBSIndependentPiece where T : IJWBSIndependentPiece where O : IJWBSPiece
    {

    }
}