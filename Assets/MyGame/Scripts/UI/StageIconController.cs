using System;
using UnityEngine;
using UnityEngine.UI;

public class StageIconController : MonoBehaviour
{
    [SerializeField] private Image _borderImage;
    [SerializeField] private Image _stageIcon;
    [SerializeField] private ButtonClickHandler _button;

    private Stage _stage;

    //public int StageId => _stage.Id;

    public event Action<StageIconController> Choosed;

    private void Awake()
    {
        //_borderImage.raycastTarget = false;
    }

    private void OnEnable()
    {
        _button.Clicked += OnButtonClicked;
    }

    private void OnDisable()
    {
        _button.Clicked -= OnButtonClicked;
    }

    //public void SetStage(Stage stage)
    //{
    //    _stage = stage;
    //    _stageIcon.sprite = stage.Icon;
    //}

    public void ShowBorderImage()
    {
        _borderImage.enabled = true;
    }

    public void HideBorderImage()
    {
        _borderImage.enabled = false;
    }

    private void OnButtonClicked()
    {
        Choosed?.Invoke(this);
    }
}
