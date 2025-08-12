using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class ColorChangerMark : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;

    //private void Awake()
    //{
    //    _renderer = GetComponent<Renderer>();

    //    ExceptionsTest.NullRefMethodTest(nameof(ColorChangerMark), nameof(Awake), _renderer);
    //}

    public void SetMaterial(Material material)
    {
        ExceptionsTest.NullRefMethodTest(nameof(ColorChangerMark), nameof(SetMaterial), material);

        _renderer.material = material;
    }
}
