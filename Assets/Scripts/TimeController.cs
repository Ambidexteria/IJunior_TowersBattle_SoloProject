using UnityEngine;

public class TimeController : MonoBehaviour
{
    [SerializeField] private float _defaultTimeScale = 1f;
    [SerializeField] private float _pauseTimeScale = 0f;

    [Range(0f, 1f)]
    [SerializeField] private float _slowMotionTimeScale = 0.2f;

    private float _timeScaleBeforePause;
    private bool _paused;

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
