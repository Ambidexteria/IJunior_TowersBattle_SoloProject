using Base.Data;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Base
{
    public class TestSavingProgress : MonoBehaviour, ISavedProgress
    {
        public void LoadProgress(PlayerProgress playerProgress)
        {
            if (GetActiveSceneName() == playerProgress.WorldData.PositionOnLevel.Level)
            {
                Vector3Data savedPosition = playerProgress.WorldData.PositionOnLevel.Position;

                if (savedPosition != null)
                    transform.position = savedPosition.AsUnityVector3();
            }
        }

        public void SaveProgress(PlayerProgress playerProgress)
        {
            playerProgress.WorldData.PositionOnLevel = new PositionOnLevel(GetActiveSceneName(), transform.position.AsVector3Data());
        }

        private string GetActiveSceneName()
        {
            return SceneManager.GetActiveScene().name;
        }

    }
}
