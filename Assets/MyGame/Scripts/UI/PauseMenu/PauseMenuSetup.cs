using Base.Infrastructure;
using Base.Services.SceneManagment;
using UnityEngine;

namespace Base.UI.PauseMenu
{
    public class PauseMenuSetup : MonoBehaviour
    {
        [SerializeField] private PauseMenuView _view;

        private PauseMenuPresenter _presenter;
        private PauseMenuModel _model;

        public PauseMenuModel CreatePauseMenu(Game game)
        {
            _model = new(game);

            _presenter = new PauseMenuPresenter(_view, _model);
            _presenter.Enable();

            _view.Enable();

            return _model;
        }
    }
}
