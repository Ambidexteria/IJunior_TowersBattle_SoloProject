using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Image))]
public class ImageRecolorer : MonoBehaviour
{
    [SerializeField] private Team _team;

    private TeamColorDatabase _teamColorDatabase;
    private Image _image;

    [Inject]
    private void Init(TeamColorDatabase database)
    {
        _teamColorDatabase = database;
        _image = GetComponent<Image>();

        if (_team == null)
            Debug.LogError($"{gameObject.name} :: {name} :: {nameof(_team)} isn't assingned");

        _image.color = _teamColorDatabase.GetMaterialByTeamType(_team.Type).color;
    }
}
