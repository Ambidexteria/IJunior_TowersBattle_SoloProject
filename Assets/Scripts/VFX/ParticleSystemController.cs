using UnityEngine;

public class ParticleSystemController : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private AudioSource _soundEffect;

    [ContextMenu(nameof(Play))]
    public void Play()
    {
        _particleSystem.Play();
        _soundEffect.Play();
    }
}
