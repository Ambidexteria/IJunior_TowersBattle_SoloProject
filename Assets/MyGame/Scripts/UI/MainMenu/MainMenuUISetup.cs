using Base.UI.MainMenu;
using UnityEngine;

namespace Base
{
    public class MainMenuUISetup : MonoBehaviour
    {
        [SerializeField] private MainMenuUIView _view;

        private MainMenuUIPresenter _presenter;
        private MainMenuUIModel _model;

        private void Awake()
        {
            _model = new MainMenuUIModel();

            _presenter = new MainMenuUIPresenter(_view, _model);
            _presenter.Enable();

            DontDestroyOnLoad(gameObject);
        }

        public MainMenuUIModel GetModel()
        {
            return _model;
        }
    }
}
