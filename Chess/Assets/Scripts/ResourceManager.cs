using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    private static ResourceManager instance;
    public static ResourceManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ResourceManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject("ResourceManager");
                    instance = obj.AddComponent<ResourceManager>();
                }
            }
            return instance;
        }
    }

    [SerializeField]
    private Color whiteColor;
    [SerializeField]
    private Color blackColor;
    [Space]
    [SerializeField, RequiredReference]
    private Shader chessShader;
    [Space]
    [SerializeField, RequiredReference]
    private GameObject pawnPrefab;
    [SerializeField, RequiredReference]
    private GameObject rookPrefab;
    [SerializeField, RequiredReference]
    private GameObject knightPrefab;
    [SerializeField, RequiredReference]
    private GameObject bishopPrefab;
    [SerializeField, RequiredReference]
    private GameObject queenPrefab;
    [SerializeField, RequiredReference]
    private GameObject kingPrefab;
    [Space]
    [SerializeField, ReadOnly]
    private Material whiteColorMaterial;
    [SerializeField, ReadOnly]
    private Material blackColorMaterial;

    public Shader ChessShader { get => chessShader; }
    public Color WhiteColor { get => whiteColor; }
    public Color BlackColor { get => blackColor; }
    public Material WhiteColorMaterial { get => whiteColorMaterial; }
    public Material BlackColorMaterial { get => blackColorMaterial; }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Initialize()
    {
        GenerateMaterials();
    }

    private void GenerateMaterials()
    {
        MaterialFactory materialFactory = new MaterialFactory(chessShader);
        whiteColorMaterial = materialFactory.CreateMaterial(whiteColor);
        blackColorMaterial = materialFactory.CreateMaterial(blackColor);
    }

    public GameObject GetPrefabForType(PieceType type)
    {
        switch (type)
        {
            case PieceType.Pawn:
                return pawnPrefab;
            case PieceType.Rook:
                return rookPrefab;
            case PieceType.Knight:
                return knightPrefab;
            case PieceType.Bishop:
                return bishopPrefab;
            case PieceType.Queen:
                return queenPrefab;
            case PieceType.King:
                return kingPrefab;
            default:
                return null;
        }
    }

    // Add additional methods or properties to manage other resources
}
