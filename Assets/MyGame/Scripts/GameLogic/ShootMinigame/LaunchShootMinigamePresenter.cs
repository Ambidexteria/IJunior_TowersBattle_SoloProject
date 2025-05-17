using UnityEngine;
using UnityEngine.UI;

namespace Base.GameLogic.ShootMinigame
{
    public class LaunchShootMinigamePresenter
    {
        private readonly ShootMinigameLauncherModel _model;
        private readonly Image _view;

        public LaunchShootMinigamePresenter(ShootMinigameLauncherModel model, Image view)
        {
            _model = model;
            _view = view;
        }

        public void Enable()
        {
            _model.StatusChanged += OnStatusChanged;
        }

        private void OnStatusChanged(Sprite sprite)
        {
            _view.sprite = sprite;
        }
    }
}
