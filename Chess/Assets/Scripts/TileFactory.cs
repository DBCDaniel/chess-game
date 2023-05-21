using UnityEngine;

public class TileFactory
{
    private readonly Material whiteMaterial;
    private readonly Material blackMaterial;
    private float squareSize;
    private float cubeHeight;
    private Transform parentTransform;

    private MeshCombineData whiteCombineData;
    private MeshCombineData blackCombineData;

    /// <summary>
    /// Constructor for the TileFactory class.
    /// </summary>
    /// <param name="whiteMaterial">The material for white tiles.</param>
    /// <param name="blackMaterial">The material for black tiles.</param>
    /// <param name="squareSize">The size of each tile.</param>
    /// <param name="cubeHeight">The height of each tile cube.</param>
    /// <param name="parentTransform">The parent transform to which the tiles will be attached.</param>
    /// <param name="size">The size of the grid (number of tiles per row/column).</param>
    public TileFactory(ref Material whiteMaterial, ref Material blackMaterial, float squareSize, float cubeHeight, Transform parentTransform, int size)
    {
        this.whiteMaterial = whiteMaterial;
        this.blackMaterial = blackMaterial;
        this.squareSize = squareSize;
        this.cubeHeight = cubeHeight;
        this.parentTransform = parentTransform;

        // Calculate number of tiles
        int numTiles = size * size;
        int numWhiteTiles = (int)(numTiles * 0.5f);
        int numBlackTiles = numTiles - numWhiteTiles;

        // Initialize white and black mesh combine data
        whiteCombineData = new MeshCombineData(numWhiteTiles);
        blackCombineData = new MeshCombineData(numBlackTiles);
    }

    /// <summary>
    /// Creates a tile GameObject at the specified position.
    /// </summary>
    /// <param name="position">The position of the tile.</param>
    /// <param name="row">The row index of the tile.</param>
    /// <param name="col">The column index of the tile.</param>
    /// <param name="size">The size of the grid (number of tiles per row/column).</param>
    /// <returns>The created tile GameObject.</returns>
    public GameObject CreateTile(Vector3 position, int row, int col, int size)
    {
        // Create tile object
        GameObject tile = GameObject.CreatePrimitive(PrimitiveType.Cube);
        tile.transform.position = position;
        SetTileSize(tile);
        SetTileMaterial(tile, row, col);
        SetTileParent(tile);
        return tile;
    }

    private void SetTileSize(GameObject tile)
    {
        // Set tile size
        tile.transform.localScale = new Vector3(squareSize, cubeHeight, squareSize);
    }

    private void SetTileMaterial(GameObject tile, int row, int col)
    {
        Renderer tileRenderer = tile.GetComponent<Renderer>();
        bool isWhite = (row + col) % 2 == 0;

        // Set tile material and combine tile mesh
        if (isWhite)
        {
            tileRenderer.material = whiteMaterial;
            CombineTileMesh(tile, whiteCombineData);
        }
        else
        {
            tileRenderer.material = blackMaterial;
            CombineTileMesh(tile, blackCombineData);
        }
    }

    private void SetTileParent(GameObject tile)
    {
        // Set tile's parent transform
        tile.transform.parent = parentTransform;
    }

    private void CombineTileMesh(GameObject tile, MeshCombineData combineData)
    {
        MeshFilter meshFilter = tile.GetComponent<MeshFilter>();

        if (meshFilter != null && meshFilter.sharedMesh != null)
        {
            // Create combine instance for the tile mesh
            CombineInstance combineInstance = new CombineInstance();
            combineInstance.mesh = meshFilter.sharedMesh;
            combineInstance.transform = tile.transform.localToWorldMatrix;

            // Add combine instance to the corresponding combine data
            combineData.combineInstances[combineData.meshIndex] = combineInstance;
            combineData.meshIndex++;
        }
    }

    /// <summary>
    /// Combines the white and black tiles into separate meshes.
    /// </summary>
    public void CombineTilesIntoMesh()
    {
        // Combine white tiles into a mesh
        CombineTilesIntoMesh(whiteCombineData, true);

        // Combine black tiles into a mesh
        CombineTilesIntoMesh(blackCombineData, false);
    }

    private void CombineTilesIntoMesh(MeshCombineData combineData, bool isWhite)
    {
        // Combine tile meshes into a single mesh
        combineData.combinedMesh.CombineMeshes(combineData.combineInstances, true, true);

        // Create combined tile object
        GameObject combinedTile = new GameObject(isWhite ? "WhiteCombinedTile" : "BlackCombinedTile");
        combinedTile.transform.position = Vector3.zero;
        combinedTile.transform.localScale = Vector3.one;

        // Add combined mesh and renderer to the combined tile object
        combinedTile.AddComponent<MeshFilter>().sharedMesh = combineData.combinedMesh;
        combinedTile.AddComponent<MeshRenderer>().material = isWhite ? whiteMaterial : blackMaterial;

        // Set the parent of the combined tile object
        combinedTile.transform.parent = parentTransform;
    }

    private class MeshCombineData
    {
        public Mesh combinedMesh;
        public CombineInstance[] combineInstances;
        public int meshIndex;

        public MeshCombineData(int numTiles)
        {
            combinedMesh = new Mesh();
            combineInstances = new CombineInstance[numTiles];
            meshIndex = 0;
        }
    }
}
