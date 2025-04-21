using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class ColorChangerMark : MonoBehaviour
{
    private Renderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    public void SetMaterial(Material material)
    {
        _renderer.material = material;
    }
}
