using UnityEngine;

namespace Base.GameLogic.ShootMinigame
{
    public class ShootMinigamePressRangePresenter
    {
        private readonly ShootMinigamePressRangeModel _model;
        private readonly ShootMinigamePressRangeView _view;

        public ShootMinigamePressRangePresenter(ShootMinigamePressRangeModel model, ShootMinigamePressRangeView view)
        {
            _model = model;
            _view = view;
        }

        public void Enable()
        {
            _model.ValueChanged += OnValueChanged;
            _model.PlacingPressRange += OnPlacingPressRange;
        }

        public void Disable()
        {
            _model.ValueChanged -= OnValueChanged;
            _model.PlacingPressRange -= OnPlacingPressRange;
        }

        private void OnPlacingPressRange(float x)
        {
            _view.SetWidth(_model.PressRangeWidth);

            Vector2 position = _view.PressRangePosition;
            position.x = x;
            _view.PlacePressRange(position);
        }

        private void OnValueChanged(float value)
        {
            _view.SetSliderValue(value);
        }
    }
}
