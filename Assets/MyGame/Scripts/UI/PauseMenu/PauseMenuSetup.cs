using Base.Services.SceneManagment;
using Base.Services.TimeManagment;
using UnityEngine;

namespace Base.UI.PauseMenu
{
    public class PauseMenuSetup : MonoBehaviour
    {
        [SerializeField] private PauseMenuView _view;

        private PauseMenuPresenter _presenter;
        private PauseMenuModel _model;

        public PauseMenuModel CreatePauseMenu(TimeController timeController, SceneChanger sceneChanger)
        {
            _model = new(timeController, sceneChanger);

            _presenter = new PauseMenuPresenter(_view, _model);
            _presenter.Enable();

            return _model;
        }
    }
}
