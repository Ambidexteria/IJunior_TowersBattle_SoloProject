using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class LaunchMinigameButtonController : MonoBehaviour
{
    private const string Scale = nameof(Scale);

    [SerializeField] private Animator _animator;
    [SerializeField] private ButtonClickHandler _launchButton;
    [SerializeField] private Sprite _disabledSprite;
    [SerializeField] private Sprite _enabledSprite;
    [SerializeField] private ParticleSystemController _particleSystemController;

    private Image _image;

    public event Action Clicked;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _image.sprite = _disabledSprite;
    }

    private void OnEnable()
    {
        _launchButton.Clicked += OnLaunchButtonPressed;
    }

    private void OnDisable()
    {
        _launchButton.Clicked -= OnLaunchButtonPressed;
    }

    public void Enable()
    {
        _launchButton.Enable();
        _image.sprite = _enabledSprite;
        _particleSystemController.Play();
        _animator.Play(Scale);
        Debug.Log("Play Scale anim");
    }

    public void Disable()
    {
        _launchButton.Disable();
        _image.sprite = _disabledSprite;
        _particleSystemController.Stop();
    }

    private void OnLaunchButtonPressed()
    {
        Clicked?.Invoke();
    }
}
