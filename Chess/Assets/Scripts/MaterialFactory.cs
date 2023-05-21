using UnityEngine;

public class MaterialFactory
{
    private Shader shader;

    public MaterialFactory(Shader shader)
    {
        this.shader = shader;
    }

    public Material CreateMaterial(Color color)
    {
        Material material = new Material(shader);
        material.color = color;
        return material;
    }
}
