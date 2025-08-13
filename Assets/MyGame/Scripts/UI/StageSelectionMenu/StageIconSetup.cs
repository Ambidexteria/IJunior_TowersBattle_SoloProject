using UnityEngine;

namespace Base.UI.StageSelection
{
    public class StageIconSetup : MonoBehaviour
    {
        [SerializeField] private StageIconView _view;

        private StageIconModel _model;
        private StageIconPresenter _presenter;

        public StageIconModel CreateModel(Sprite sprite, bool unlocked, string stageName)
        {
            _view.Init(sprite, unlocked, stageName);
            _model = new StageIconModel(stageName);

            _presenter = new StageIconPresenter(_view, _model);
            _presenter.Enable();

            return _model;
        }
    }
}
