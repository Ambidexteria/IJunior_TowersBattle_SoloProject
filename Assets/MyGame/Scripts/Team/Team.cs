using UnityEngine;

public class Team : MonoBehaviour
{
    [SerializeField] private TeamType _type;

    public TeamType Type => _type;
}
