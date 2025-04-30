using Base.Data;
using Base.Services.SaveLoad;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Base
{
    public class TestSavingProgress : MonoBehaviour, ISavedProgress
    {
        private ISaveLoadService _service;


        [Inject]
        private void Init(ISaveLoadService saveLoadService)
        {
            _service = saveLoadService;
        }

        public void LoadProgress(PlayerProgress playerProgress)
        {
            if (playerProgress == null)
                return;

            if (GetActiveScene() == playerProgress.WorldData.PositionOnLevel.Level)
            {
                Vector3Data savedPosition = playerProgress.WorldData.PositionOnLevel.Position;

                if (savedPosition != null)
                {
                    transform.position = savedPosition.AsUnityVector3();
                    Debug.Log(transform.position);
                }
            }
        }

        private  string GetActiveScene()
        {
            return SceneManager.GetActiveScene().name;
        }

        [ContextMenu(nameof(SaveProgress))]
        public void SaveProgress()
        {
            _service.SaveProgress();
            //playerProgress.WorldData.PositionOnLevel = new PositionOnLevel(GetActiveSceneName(), transform.position.AsVector3Data());
        }

        public void SaveProgress(PlayerProgress playerProgress)
        {
            playerProgress.WorldData.PositionOnLevel.Position = transform.position.AsVector3Data();
        }
    }
}
