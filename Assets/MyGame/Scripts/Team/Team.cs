using UnityEngine;

public class Team : MonoBehaviour
{
    [SerializeField] private TeamType _type;

    public TeamType Type => _type;

    public void SetType(TeamType type)
    {
        _type = type;
    }
}
