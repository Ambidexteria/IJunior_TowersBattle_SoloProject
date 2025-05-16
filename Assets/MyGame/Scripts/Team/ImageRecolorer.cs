using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Image))]
public class ImageRecolorer : MonoBehaviour
{
    [SerializeField] private Team _team;

    private TeamColorChanger _colorChanger;
    private Image _image;

    [Inject]
    private void Init(TeamColorChanger database)
    {
        _colorChanger = database;
        _image = GetComponent<Image>();

        if (_team == null)
            Debug.LogError($"{gameObject.name} :: {name} :: {nameof(_team)} isn't assingned");

        _image.color = _colorChanger.GetColor(_team);
    }
}
