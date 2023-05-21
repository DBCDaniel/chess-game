using UnityEngine;
using UnityEngine;

public partial class GameManager : MonoBehaviour
{
    // Singleton instance
    private static GameManager instance;

    // Public getter for the instance
    public static GameManager Instance
    {
        get { return instance; }
    }

    [SerializeField, RequiredReference]
    private ChessboardGenerator chessboardGenerator;

    // Track whose turn it is
    public bool IsWhiteTurn { get; private set; }

    private Player whitePlayer;
    private Player blackPlayer;

    private void Awake()
    {
        // Initialize the resource manager
        ResourceManager.Instance.Initialize();
    }

    private void Start()
    {
        // Generate the chessboard and initialize players
        GenerateBoard();
        GenerateArmies();

        // Set white's turn to start the game
        IsWhiteTurn = true;
    }

    private void GenerateBoard()
    {
        // Use the materials from the resource manager
        Material whiteMaterial = ResourceManager.Instance.WhiteColorMaterial;
        Material blackMaterial = ResourceManager.Instance.BlackColorMaterial;

        chessboardGenerator.GenerateBoard(ref whiteMaterial, ref blackMaterial);
    }
    private void GenerateArmies()
    {
        whitePlayer = new Player(PlayerColor.White);
        blackPlayer = new Player(PlayerColor.Black);

        GenerateArmy(whitePlayer);
        GenerateArmy(blackPlayer);
    }

    private void GenerateArmy(Player player)
    {
        // Generate and position the pawns
        int pawnRank = (player.Color == PlayerColor.White) ? 1 : 6;
        Material pawnMaterial = (player.Color == PlayerColor.White) ? ResourceManager.Instance.WhiteColorMaterial : ResourceManager.Instance.BlackColorMaterial;

        GameObject pawnPrefab = ResourceManager.Instance.GetPrefabForType(PieceType.Pawn);

        for (int file = 0; file < 8; file++)
        {
            string fileName = chessboardGenerator.GetFileNameFromIndex(file);
            Vector3 pawnPosition = chessboardGenerator.GetPositionByFileRank(fileName, pawnRank);
            Vector3 finalPawnPosition = new Vector3(pawnPosition.x, pawnPrefab.transform.position.y, pawnPosition.z);

            GameObject pawnObject = Instantiate(pawnPrefab, finalPawnPosition, Quaternion.identity);
            ChessPiece pawn = pawnObject.GetComponent<ChessPiece>();
            pawn.GetComponent<Renderer>().material = pawnMaterial;
            // Add the pawn to the player's army
            player.AddPiece(pawn);
        }
    }


}

public partial class GameManager
{
    // Add the necessary methods to determine the validity of positions and check for pieces

    public bool IsValidPosition(Vector2 position)
    {
        // Add your logic to check if the position is within the valid chessboard range
        // Return true if it is a valid position, false otherwise
        return false;
    }

    public bool IsEmptyPosition(Vector2 position)
    {
        // Add your logic to check if the position is empty (no piece at that position)
        // Return true if the position is empty, false otherwise
        return false;
    }

    public bool IsOpponentPiece(Vector2 position)
    {
        // Add your logic to check if the piece at the given position belongs to the opponent
        // Return true if it is the opponent's piece, false otherwise
        return false;
    }

    public bool IsValidEnPassantTarget(Vector2 position)
    {
        // Implement the logic to check if the given position is a valid en passant target
        // Return true if it is a valid en passant target, false otherwise
        return false;
    }
}
