using UnityEngine;

public class ParticleSystemController : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private AudioSource _soundEffect;

    [ContextMenu(nameof(Play))]
    public void Play()
    {
        if (_particleSystem != null)
            _particleSystem?.Play();

        if (_soundEffect != null)
            _soundEffect.Play();
    }

    public void Stop()
    {
        if (_particleSystem != null)
            _particleSystem.Stop();

        if (_soundEffect != null)
            _soundEffect.Stop();
    }

    public void SetPosition(Vector3 position)
    {
        gameObject.transform.position = position;
    }
}
