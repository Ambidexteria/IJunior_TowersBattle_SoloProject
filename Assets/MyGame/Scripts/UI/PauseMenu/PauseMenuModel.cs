using Base.Services.SceneManagment;
using Base.Services.TimeManagment;
using Base.UI.Game.StateMachine;
using System;
using UnityEngine;

namespace Base.UI.PauseMenu
{
    public class PauseMenuModel
    {
        private readonly TimeController _timeController;
        private readonly SceneChanger _sceneChanger;

        public PauseMenuModel(TimeController timeController, SceneChanger sceneChanger)
        {
            _timeController = timeController;
            _sceneChanger = sceneChanger;
        }

        public void RestartLevel()
        {
            _sceneChanger.ReloadGameScene();
        }

        public void ReturnToMainMenu()
        {
            _sceneChanger.ReloadGameScene();
        }

        public void Pause()
        {
            Debug.LogWarning("Pause Model");
            _timeController.Pause();
        }

        public void Resume()
        {
            _timeController.Resume();
        }
    }
}
