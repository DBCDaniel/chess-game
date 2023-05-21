using UnityEngine;

public class GameManager : MonoBehaviour
{
    [System.Serializable]
    private class GraphicalReferences
    {
        [SerializeField, RequiredReference]
        private Shader chessShader;
        [ReadOnly]
        public Material whiteMaterial;
        [ReadOnly]
        public Material blackMaterial;
        [Space]
        [SerializeField]
        private Color whiteColor;
        [SerializeField]
        private Color blackColor;

        public Shader ChessShader { get => chessShader; }
        public Color WhiteColor { get => whiteColor; }
        public Color BlackColor { get => blackColor; }
    }

    [SerializeField]
    private GraphicalReferences graphicalReferences;

    [SerializeField, RequiredReference]
    private ChessboardGenerator chessboardGenerator;

    private MaterialFactory materialFactory;

    /// <summary>
    /// Initializes the GameManager by creating a MaterialFactory instance with the specified chess shader.
    /// </summary>
    private void Awake()
    {
        materialFactory = new MaterialFactory(graphicalReferences.ChessShader);
    }

    /// <summary>
    /// Starts the GameManager by generating the chessboard with the appropriate materials.
    /// </summary>
    private void Start()
    {
        GenerateBoard();
    }

    /// <summary>
    /// Generates the chessboard by creating and assigning materials based on the material factory and colors.
    /// </summary>
    private void GenerateBoard()
    {
        graphicalReferences.whiteMaterial = materialFactory.CreateMaterial(graphicalReferences.WhiteColor);
        graphicalReferences.blackMaterial = materialFactory.CreateMaterial(graphicalReferences.BlackColor);

        chessboardGenerator.GenerateBoard(ref graphicalReferences.whiteMaterial, ref graphicalReferences.blackMaterial);
    }
}
