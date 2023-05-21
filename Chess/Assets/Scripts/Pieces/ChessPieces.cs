using System.Collections;
using UnityEngine;

public enum PieceType
{
    Pawn,
    Knight,
    Bishop,
    Rook,
    Queen,
    King
}

public abstract class ChessPiece : MonoBehaviour
{
    public PieceType pieceType;
    public Vector2 currentPosition;

    public abstract bool[,] PossibleMoves();

    public virtual void MoveTo(Vector2 targetPosition)
    {
        StartCoroutine(MoveAnimation(targetPosition, GameSettings.Instance.PieceMovementSpeed));
    }

    private IEnumerator MoveAnimation(Vector2 targetPosition, float speed)
    {
        Vector3 startPosition = transform.position;
        Vector3 targetPosition3D = new Vector3(targetPosition.x, 0, targetPosition.y);
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * speed;
            transform.position = Vector3.Lerp(startPosition, targetPosition3D, t);
            yield return null;
        }

        currentPosition = targetPosition;
    }
}
