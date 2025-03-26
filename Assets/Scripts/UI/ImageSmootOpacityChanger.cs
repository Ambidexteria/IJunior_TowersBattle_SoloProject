using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ImageSmootOpacityChanger : MonoBehaviour
{
    [SerializeField] private float _startDelay = 0f;
    [SerializeField] private float _changingTime = 1f;

    private Coroutine _coroutine;
    private WaitForSeconds _waitDelay;
    private Image _image;
    private float _targetOpacity;

    private void Awake()
    {
        _image = GetComponent<Image>();

        _targetOpacity = _image.color.a;
        _waitDelay = new WaitForSeconds(_startDelay);
    }

    private void OnEnable()
    {
        _coroutine = StartCoroutine(ChangeOpacity());
    }

    private void OnDisable()
    {
        if(_coroutine != null) 
            StopCoroutine(_coroutine);
    }

    private IEnumerator ChangeOpacity()
    {
        float currentOpacity = 0;
        float changingSpeed = _targetOpacity / _changingTime;
        Color tempColor = _image.color;
        tempColor.a = currentOpacity;
        _image.color = tempColor;

        yield return _waitDelay;

        while (currentOpacity < _targetOpacity)
        {
            currentOpacity = Mathf.MoveTowards(currentOpacity, _targetOpacity, changingSpeed * Time.deltaTime);

            tempColor.a = currentOpacity;
            _image.color = tempColor;

            yield return null;
        }
    }
}
