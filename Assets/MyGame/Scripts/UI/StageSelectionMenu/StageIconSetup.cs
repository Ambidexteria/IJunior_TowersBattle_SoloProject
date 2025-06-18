using UnityEngine;

namespace Base.UI.StageSelection
{
    public class StageIconSetup : MonoBehaviour
    {
        [SerializeField] private StageIconView _view;

        private StageIconModel _model;
        private StageIconPresenter _presenter;

        private void Awake()
        {
            ExceptionsTest.NullRefMethodTest(nameof(StageIconSetup), nameof(Awake), _view);
        }

        public StageIconModel CreateModel(bool unlocked, string stageName)
        {
            _view.Init(unlocked, stageName);
            _model = new StageIconModel(stageName, unlocked);

            _presenter = new StageIconPresenter(_view, _model);
            _presenter.Enable();

            return _model;
        }
    }
}
