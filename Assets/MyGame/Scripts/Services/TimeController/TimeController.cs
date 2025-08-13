using UnityEngine;

namespace Base.Services.TimeManagment
{
    public class TimeController
    {
        private readonly float _defaultTimeScale = 1f;
        private readonly float _pauseTimeScale = 0f;
        private readonly float _slowMotionTimeScale = 0.2f;

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
