using UnityEngine;
using UnityEngine.UI;

namespace Base.GameLogic.ShootMinigame
{
    public class ShootMinigameSetup : MonoBehaviour
    {
        [Range(0f, 1f)]
        [SerializeField] private float _pressRange = 0.1f;
        [SerializeField] private float _sliderSpeedRate;
        [SerializeField] private Image _pressRangeImage;
        [SerializeField] private RectTransform _fullRangeRectTransform;

        [SerializeField] private ShootMinigameLauncherModel _launchButtonController;
        [SerializeField] private UIWindowController _minigameUI;
        [SerializeField] private SliderValueChanger _slider;
        [SerializeField] private ButtonClickHandler _shootButton;
        [SerializeField] private TimeController _timeController;

        private ShootMinigameModel _model;

        public ShootMinigameModel GetModel() => _model;
    }
}
