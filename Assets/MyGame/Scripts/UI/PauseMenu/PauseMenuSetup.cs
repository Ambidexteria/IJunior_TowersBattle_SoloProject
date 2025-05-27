using Base.Services.SceneManagment;
using UnityEngine;

namespace Base.UI.PauseMenu
{
    public class PauseMenuSetup : MonoBehaviour
    {
        [SerializeField] private PauseMenuView _view;

        private PauseMenuPresenter _presenter;
        private PauseMenuModel _model;

        public PauseMenuModel CreatePauseMenu(SceneChanger sceneChanger)
        {
            _model = new(sceneChanger);

            _presenter = new PauseMenuPresenter(_view, _model);
            _presenter.Enable();

            _view.Enable();

            return _model;
        }
    }
}
