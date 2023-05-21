using UnityEngine;
public class PieceFactory
{
    public ChessPiece CreatePiece(PieceType type)
    {
        GameObject prefab = ResourceManager.Instance.GetPrefabForType(type);
        if (prefab == null)
        {
            Debug.LogError("No prefab found for piece type: " + type);
            return null;
        }

        GameObject pieceObject = GameObject.Instantiate(prefab);
        ChessPiece piece = pieceObject.GetComponent<ChessPiece>();
        if (piece != null)
        {
            // Optionally perform any additional setup or initialization here
            return piece;
        }

        Debug.LogError("The prefab for piece type: " + type + " does not have a ChessPiece component.");
        GameObject.Destroy(pieceObject);
        return null;
    }
}
