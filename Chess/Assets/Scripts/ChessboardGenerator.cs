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

    /// <summary>
    /// Generates the chessboard with tiles.
    /// </summary>
    private void GenerateChessboard()
    {
        // Initialize the dictionary to store tiles
        tiles = new Dictionary<string, Vector3>();

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

    /*
    /// <summary>
    /// Creates a tile GameObject at the specified position.
    /// </summary>
    /// <param name="position">The position at which to create the tile.</param>
    /// <param name="row">The row index of the tile.</param>
    /// <param name="col">The column index of the tile.</param>
    /// <returns>The created tile GameObject.</returns>
    GameObject CreateTile(Vector3 position, int row, int col)
    {
        GameObject tile = GameObject.CreatePrimitive(PrimitiveType.Cube);
        tile.transform.position = position;
        SetTileSize(tile);
        SetTileMaterial(tile, row, col);
        SetTileParent(tile);
        return tile;
    }
    /// <summary>
    /// Sets the size of the tile GameObject.
    /// </summary>
    /// <param name="tile">The tile GameObject to set the size for.</param>
    void SetTileSize(GameObject tile)
    {
        tile.transform.localScale = new Vector3(settings.SquareSize, settings.CubeHeight, settings.SquareSize);
    }

    /// <summary>
    /// Sets the material of the tile based on its position on the chessboard.
    /// </summary>
    /// <param name="tile">The tile GameObject to set the material for.</param>
    /// <param name="row">The row index of the tile.</param>
    /// <param name="col">The column index of the tile.</param>
    void SetTileMaterial(GameObject tile, int row, int col)
    {
        Renderer tileRenderer = tile.GetComponent<Renderer>();
        tileRenderer.material = (row + col) % 2 == 0 ? whiteMaterial : blackMaterial;
    }

    /// <summary>
    /// Sets the parent of the tile GameObject to the ChessboardGenerator.
    /// </summary>
    /// <param name="tile">The tile GameObject to set the parent for.</param>
    void SetTileParent(GameObject tile)
    {
        tile.transform.parent = transform;
    }

    */

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
        tileFactory = new TileFactory(ref whiteMaterial, ref blackMaterial, settings.SquareSize, settings.CubeHeight, transform);

        // Generate the chessboard
        GenerateChessboard();
    }
}
