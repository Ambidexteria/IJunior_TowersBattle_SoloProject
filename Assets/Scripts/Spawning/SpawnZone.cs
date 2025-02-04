using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class SpawnZone : MonoBehaviour
{
    private Collider2D _collider;
    private Vector2 _scale;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _collider.isTrigger = true;

        _scale = _collider.transform.localScale;
    }

    public Vector2 GetRandomSpawnPosition()
    {
        Vector2 position = _collider.transform.position;
        float x = GetRandomCoordinate(_scale.x, position.x);
        float y = GetRandomCoordinate(_scale.y, position.y);

        return new Vector2(x, y);
    }

    private float GetRandomCoordinate(float scale, float position)
    {
        scale = Mathf.Abs(scale);

        float coordinate1 = position - (scale / 2);
        float coordinate2 = position + (scale / 2);

        return Random.Range(coordinate1, coordinate2);
    }
}
