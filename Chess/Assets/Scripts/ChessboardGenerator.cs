using UnityEngine;

public class ChessboardGenerator : MonoBehaviour
{
    [System.Serializable]
    private class ChessboardReferences
    {
        [SerializeField, RequiredReference]
        private Shader chessboardShader;

        [SerializeField, ReadOnly]
        private Material whiteMaterial;
        [SerializeField, ReadOnly]
        private Material blackMaterial;

        public Shader ChessboardShader { get => chessboardShader; }
        public Material WhiteMaterial { get => whiteMaterial; }
        public Material BlackMaterial { get => blackMaterial; }

        // Additional method to create and assign materials
        public void CreateMaterials()
        {
            whiteMaterial = new Material(chessboardShader);
            whiteMaterial.color = Color.white;

            blackMaterial = new Material(chessboardShader);
            blackMaterial.color = Color.black;
        }
    }

    [System.Serializable]
    private class ChessboardSettings
    {
        [SerializeField]
        private float squareSize = 1.0f;

        [SerializeField]
        private float cubeHeight = 1.0f;

        [SerializeField]
        private Vector3 boardPosition = Vector3.zero;

        public float SquareSize { get => squareSize;  }
        public float CubeHeight { get => cubeHeight; }
        public Vector3 BoardPosition { get => boardPosition; }
    }

    [SerializeField]
    private ChessboardReferences references;

    [SerializeField]
    private ChessboardSettings settings;

    private string[] letters = { "A", "B", "C", "D", "E", "F", "G", "H" };

    /// <summary>
    /// Generates the chessboard with cubes.
    /// </summary>
    void GenerateChessboard()
    {
        int size = 8; // Number of rows and columns in the chessboard

        for (int row = 0; row < size; row++)
        {
            for (int col = 0; col < size; col++)
            {
                Vector3 position = CalculateCubePosition(row, col);
                position += settings.BoardPosition; // Add board position offset
                GameObject cube = CreateCube(position);
                SetCubeSize(cube);
                SetCubeMaterial(cube, row, col);
                SetCubeParent(cube);
                NameCube(cube, col, row, size);
            }
        }
    }

    /// <summary>
    /// Calculates the position of a cube based on the row and column indices.
    /// </summary>
    /// <param name="row">The row index of the cube.</param>
    /// <param name="col">The column index of the cube.</param>
    /// <returns>The calculated position of the cube.</returns>
    Vector3 CalculateCubePosition(int row, int col)
    {
        return new Vector3(row * settings.SquareSize, 0, col * settings.SquareSize);
    }

    /// <summary>
    /// Creates a cube GameObject at the specified position.
    /// </summary>
    /// <param name="position">The position at which to create the cube.</param>
    /// <returns>The created cube GameObject.</returns>
    GameObject CreateCube(Vector3 position)
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = position;
        return cube;
    }

    /// <summary>
    /// Sets the size of the cube GameObject.
    /// </summary>
    /// <param name="cube">The cube GameObject to set the size for.</param>
    void SetCubeSize(GameObject cube)
    {
        cube.transform.localScale = new Vector3(settings.SquareSize, settings.CubeHeight, settings.SquareSize);
    }

    /// <summary>
    /// Sets the material of the cube based on its position on the chessboard.
    /// </summary>
    /// <param name="cube">The cube GameObject to set the material for.</param>
    /// <param name="row">The row index of the cube.</param>
    /// <param name="col">The column index of the cube.</param>
    void SetCubeMaterial(GameObject cube, int row, int col)
    {
        Renderer cubeRenderer = cube.GetComponent<Renderer>();
        cubeRenderer.material = (row + col) % 2 == 0 ? references.WhiteMaterial : references.BlackMaterial;
    }

    /// <summary>
    /// Sets the parent of the cube GameObject to the ChessboardGenerator.
    /// </summary>
    /// <param name="cube">The cube GameObject to set the parent for.</param>
    void SetCubeParent(GameObject cube)
    {
        cube.transform.parent = transform;
    }

    /// <summary>
    /// Names the cube GameObject based on its position on the chessboard.
    /// </summary>
    /// <param name="cube">The cube GameObject to name.</param>
    /// <param name="col">The column index of the cube.</param>
    /// <param name="row">The row index of the cube.</param>
    /// <param name="size">The size of the chessboard.</param>
    void NameCube(GameObject cube, int col, int row, int size)
    {
        cube.name = letters[col] + (size - row).ToString();
    }

    void Start()
    {
        // Create the materials
        references.CreateMaterials();
        
        GenerateChessboard();
    }
}
