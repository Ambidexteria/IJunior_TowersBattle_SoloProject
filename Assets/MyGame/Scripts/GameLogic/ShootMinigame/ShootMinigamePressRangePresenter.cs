using UnityEngine;

namespace Base.GameLogic.ShootMinigame
{
    public class ShootMinigamePressRangePresenter
    {
        private readonly ShootMinigamePressRangeModel _model;
        private readonly ShootMinigamePressRangeView _view;

        public ShootMinigamePressRangePresenter(ShootMinigamePressRangeModel model, ShootMinigamePressRangeView view)
        {
            ExceptionsTest.NullRefMethodTest(nameof(ShootMinigamePressRangePresenter), ExceptionsTest.ConstructorName, model, view);

            _model = model;
            _view = view;
        }

        public void Enable()
        {
            _model.ValueChanged += OnValueChanged;
            _model.PlacingPressRange += OnPlacingPressRange;

            _view.SetWidth(_model.PressRangeWidth);
            _view.SetMinMaxValues(_model.FullRangeMinValue, _model.FullRangeMaxValue);
        }

        public void Disable()
        {
            _model.ValueChanged -= OnValueChanged;
            _model.PlacingPressRange -= OnPlacingPressRange;
        }

        private void OnPlacingPressRange(float x)
        {
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
