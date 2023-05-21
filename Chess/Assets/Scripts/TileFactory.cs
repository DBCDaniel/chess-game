using UnityEngine;

public class TileFactory
{
    private readonly Material whiteMaterial;
    private readonly Material blackMaterial;
    private float squareSize;
    private float cubeHeight;
    private Transform parentTransform;
    private Material combinedMaterial;

    public TileFactory(ref Material whiteMaterial, ref Material blackMaterial, float squareSize, float cubeHeight, Transform parentTransform)
    {
        this.whiteMaterial = whiteMaterial;
        this.blackMaterial = blackMaterial;
        this.squareSize = squareSize;
        this.cubeHeight = cubeHeight;
        this.parentTransform = parentTransform;
    }

    public GameObject CreateTile(Vector3 position, int row, int col, int size)
    {
        GameObject tile = GameObject.CreatePrimitive(PrimitiveType.Cube);
        tile.transform.position = position;
        SetTileSize(tile);
        SetTileMaterial(tile, row, col);
        SetTileParent(tile);
        return tile;
    }

    private void SetTileSize(GameObject tile)
    {
        tile.transform.localScale = new Vector3(squareSize, cubeHeight, squareSize);
    }

    private void SetTileMaterial(GameObject tile, int row, int col)
    {
        Renderer tileRenderer = tile.GetComponent<Renderer>();
        if (combinedMaterial == null)
        {
            combinedMaterial = new Material(whiteMaterial.shader);
            combinedMaterial.CopyPropertiesFromMaterial(whiteMaterial);
            combinedMaterial.name = "CombinedTileMaterial";
        }

        if ((row + col) % 2 == 0)
        {
            tileRenderer.material = combinedMaterial;
            tileRenderer.material.color = Color.white;
        }
        else
        {
            tileRenderer.material = combinedMaterial;
            tileRenderer.material.color = Color.black;
        }
    }

    private void SetTileParent(GameObject tile)
    {
        tile.transform.parent = parentTransform;
    }
}
