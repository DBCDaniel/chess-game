using UnityEngine;
using System.Collections.Generic;

public class ChessboardGenerator : MonoBehaviour
{
    [System.Serializable]
    private class ChessboardSettings
    {
        [SerializeField]
        private float squareSize = 1.0f;
        [SerializeField]
        private float cubeHeight = 1.0f;
        [SerializeField]
        private Vector3 boardPosition = Vector3.zero;

        public float SquareSize { get => squareSize; }
        public float CubeHeight { get => cubeHeight; }
        public Vector3 BoardPosition { get => boardPosition; }
    }

    [SerializeField]
    private ChessboardSettings settings;

    private string[] letters = { "A", "B", "C", "D", "E", "F", "G", "H" };

    // Number of rows and columns in the chessboard
    private const int size = 8;

    private Dictionary<string, Vector3> tiles;

    // Instance of TileFactory
    private TileFactory tileFactory;

    // List to store created tile game objects
    private List<GameObject> createdTiles; 

    /// <summary>
    /// Generates the chessboard with tiles.
    /// </summary>
    private void GenerateChessboard()
    {
        // Initialize the dictionary to store tiles
        tiles = new Dictionary<string, Vector3>();

        // Initialize the list to store created tiles
        createdTiles = new List<GameObject>();

        for (int row = 0; row < size; row++)
        {
            for (int col = 0; col < size; col++)
            {
                Vector3 position = CalculateTilePosition(row, col);
                position += settings.BoardPosition;

                // Use the TileFactory to create the tile
                GameObject tile = tileFactory.CreateTile(position, row, col, size);

                NameTile(tile, col, row, size);

                // Store the tile in the dictionary using the file and rank coordinates as the key
                string key = GetTileKey(col, row); 
                // Calculate center position
                position += new Vector3(settings.SquareSize * 0.5f, 0f, settings.SquareSize * 0.5f);
                tiles[key] = position;

                // Add the tile to the list of created tiles
                createdTiles.Add(tile); 

            }
        }
    }

    /// <summary>
    /// Calculates the position of a tile based on the row and column indices.
    /// </summary>
    /// <param name="row">The row index of the tile.</param>
    /// <param name="col">The column index of the tile.</param>
    /// <returns>The calculated position of the tile.</returns>
    Vector3 CalculateTilePosition(int row, int col)
    {
        return new Vector3(row * settings.SquareSize, 0, col * settings.SquareSize);
    }

    /// <summary>
    /// Cleans up the tile meshes.
    /// If the createdTiles list is null, the method returns without further execution.
    /// </summary>
    private void CleanupTiles()
    {
        if (createdTiles == null)
            return;

        foreach (GameObject tile in createdTiles)
        {
            Destroy(tile);
        }

        createdTiles.Clear();
    }

    /// <summary>
    /// Names the tile GameObject based on its position on the chessboard.
    /// </summary>
    /// <param name="tile">The tile GameObject to name.</param>
    /// <param name="col">The column index of the tile.</param>
    /// <param name="row">The row index of the tile.</param>
    /// <param name="size">The size of the chessboard.</param>
    void NameTile(GameObject tile, int col, int row, int size)
    {
        tile.name = letters[col] + (size - row).ToString();
    }

    public Vector3 GetPositionByFileRank(string file, int rank)
    {
        string key = file + rank.ToString();

        if (tiles.ContainsKey(key))
        {
            // Retrieve the center position from the dictionary
            return tiles[key];
        }

        return Vector3.zero; // Default position if tile not found
    }

    private string GetTileKey(int col, int row)
    {
        string file = letters[col];
        int rank = size - row;

        return file + rank.ToString();
    }

    public void GenerateBoard(ref Material whiteMaterial, ref Material blackMaterial)
    {
        // Create an instance of TileFactory
        tileFactory = new TileFactory(ref whiteMaterial, ref blackMaterial, settings.SquareSize, settings.CubeHeight, transform, size);

        // Generate the chessboard
        GenerateChessboard();

        // Combine the tile meshes into a single mesh
        tileFactory.CombineTilesIntoMesh();

        // Clean up the tile meshes
        CleanupTiles();
    }
}
