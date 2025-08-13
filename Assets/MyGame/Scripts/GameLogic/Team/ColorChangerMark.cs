using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class ColorChangerMark : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;

    public void SetMaterial(Material material)
    {
        _renderer.material = material;
    }
}
