using UnityEngine;

public class GameManager : MonoBehaviour
{
    [System.Serializable]
    private class ChessboardReferences
    {
        [SerializeField, RequiredReference]
        private Shader chessboardShader;
        [ReadOnly]
        public Material whiteMaterial;
        [ReadOnly]
        public Material blackMaterial;

        public Shader ChessboardShader { get => chessboardShader; }
    }

    [SerializeField]
    private ChessboardReferences chessboardReferences;

    [SerializeField, RequiredReference]
    private ChessboardGenerator chessboardGenerator;

    private MaterialFactory materialFactory;

    private void Awake()
    {
        // Initialize the material factory with the chessboardShader
        materialFactory = new MaterialFactory(chessboardReferences.ChessboardShader);
    }

    private void Start()
    {
        // Generate the board with the appropriate materials
        GenerateBoard();
    }

    private void GenerateBoard()
    {
        // Create and assign materials based on the material factory
        chessboardReferences.whiteMaterial = materialFactory.CreateMaterial(Color.white);
        chessboardReferences.blackMaterial = materialFactory.CreateMaterial(Color.black);

        // Generate the chessboard with the materials
        chessboardGenerator.GenerateBoard(ref chessboardReferences.whiteMaterial, ref chessboardReferences.blackMaterial);
    }

}
