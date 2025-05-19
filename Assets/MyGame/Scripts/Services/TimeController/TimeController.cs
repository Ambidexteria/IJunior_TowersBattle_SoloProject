using UnityEngine;

namespace Base.Services.TimeManagment
{
    public class TimeController
    {
        private float _defaultTimeScale = 1f;
        private float _pauseTimeScale = 0f;
        private float _slowMotionTimeScale = 0.2f;

        private float _timeScaleBeforePause;
        private bool _paused;

        public TimeController(float defaultTimeScale, float pauseTimeScale, float slowMotionTimeScale)
        {
            _defaultTimeScale = defaultTimeScale;
            _pauseTimeScale = pauseTimeScale;
            _slowMotionTimeScale = slowMotionTimeScale;
        }

        public float SlowMotionTimeScale => _slowMotionTimeScale;

        public void SetDefaultTimeScale()
        {
            Time.timeScale = _defaultTimeScale;
        }

        public void Pause()
        {
            Debug.Log("Pause");
            _timeScaleBeforePause = Time.timeScale;
            Time.timeScale = _pauseTimeScale;
            _paused = true;
        }

        public void Resume()
        {
            Debug.Log("Resume");

            if (_paused)
            {
                Time.timeScale = _timeScaleBeforePause;
                _paused = false;
            }
        }

        public void SetSlowMotionTimeScale()
        {
            Time.timeScale *= _slowMotionTimeScale;
        }
    }
}
