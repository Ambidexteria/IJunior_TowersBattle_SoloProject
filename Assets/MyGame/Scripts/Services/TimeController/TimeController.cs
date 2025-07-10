using UnityEngine;

namespace Base.Services.TimeManagment
{
    public class TimeController
    {
        private float _defaultTimeScale = 1f;
        private float _pauseTimeScale = 0f;
        private float _slowMotionTimeScale = 0.2f;

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
            Time.timeScale = _pauseTimeScale;
        }

        public void Resume()
        {
            Time.timeScale = _defaultTimeScale;
        }

        public void SetSlowMotionTimeScale()
        {
            Time.timeScale = _defaultTimeScale * _slowMotionTimeScale;
        }
    }
}
