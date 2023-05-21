using UnityEngine;

public class PawnPiece : ChessPiece
{
    public bool isFirstMove = true;
    // Flag to track if the last move was a double move
    private bool doubleMoveLastTurn = false; 

    public override bool[,] PossibleMoves()
    {
        bool[,] moves = new bool[8, 8];

        // Determine the direction based on the color
        int direction = GameManager.Instance.IsWhiteTurn ? 1 : -1;

        // Forward movement
        BasicMovement(currentPosition + new Vector2(0, direction), ref moves);

        // Capture moves
        CaptureMovement(new Vector2(-1, direction), ref moves);
        CaptureMovement(new Vector2(1, direction), ref moves);

        // En passant capture
        EnPassantCapture(new Vector2(-1, direction), ref moves);
        EnPassantCapture(new Vector2(1, direction), ref moves);

        return moves;
    }

    private void BasicMovement(Vector2 targetPosition, ref bool[,] moves)
    {
        if (GameManager.Instance.IsValidPosition(targetPosition) && GameManager.Instance.IsEmptyPosition(targetPosition))
        {
            // Valid move
            moves[(int)targetPosition.x, (int)targetPosition.y] = true;

            if (isFirstMove)
            {
                Vector2 doubleMovePos = targetPosition + new Vector2(0, targetPosition.y - currentPosition.y);
                if (GameManager.Instance.IsValidPosition(doubleMovePos) && GameManager.Instance.IsEmptyPosition(doubleMovePos))
                {
                    // Valid double move on first move
                    moves[(int)doubleMovePos.x, (int)doubleMovePos.y] = true;
                }
            }
        }
    }

    private void CaptureMovement(Vector2 offset, ref bool[,] moves)
    {
        Vector2 capturePos = currentPosition + offset;
        if (GameManager.Instance.IsValidPosition(capturePos) && GameManager.Instance.IsOpponentPiece(capturePos))
        {
            // Valid capture move
            moves[(int)capturePos.x, (int)capturePos.y] = true;
        }
    }

    private void EnPassantCapture(Vector2 offset, ref bool[,] moves)
    {
        Vector2 targetPos = currentPosition + offset;
        Vector2 enPassantPos = currentPosition + new Vector2(offset.x, 0);

        if (GameManager.Instance.IsValidPosition(targetPos) && GameManager.Instance.IsEmptyPosition(targetPos))
        {
            if (GameManager.Instance.IsValidEnPassantTarget(enPassantPos))
            {
                // Valid en passant capture
                moves[(int)targetPos.x, (int)targetPos.y] = true;
            }
        }
    }

    public override void MoveTo(Vector2 targetPosition)
    {
        doubleMoveLastTurn = Mathf.Abs(targetPosition.y - currentPosition.y) == 2;
        base.MoveTo(targetPosition);
        isFirstMove = false;
    }

}
