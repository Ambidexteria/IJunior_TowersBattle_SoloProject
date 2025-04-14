using UnityEngine;

public class Stage : MonoBehaviour
{
    [SerializeField] private int _id;
    [SerializeField] private Sprite _icon;

    public int Id => _id;
    public Sprite Icon => _icon;
}
