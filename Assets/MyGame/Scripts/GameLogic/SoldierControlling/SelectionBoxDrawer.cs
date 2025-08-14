using Base.Infrastructure;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionBoxDrawer : MonoBehaviour
{
    [SerializeField] private Image _selectionBorder;

    private ICoroutineRunner _coroutineRunner;
    private Vector3 _firstPosition;
    private Vector3 _secondPosition;
    private Coroutine _drawSelectionBoxCoroutine;

    private bool _initialized;

    public void Init(ICoroutineRunner coroutineRunner)
    {
        _coroutineRunner = coroutineRunner;
        _initialized = true;
    }

    public void Draw(Vector3 firstPosition, Vector3 secondPosition)
    {
        if (_initialized == false)
            throw new InvalidOperationException(nameof(Draw));

        _firstPosition = firstPosition;
        _secondPosition = secondPosition;
        _selectionBorder.enabled = true;

        _drawSelectionBoxCoroutine = _coroutineRunner.LaunchCoroutine(DrawSelectionBoxCoroutine());
    }

    public void Stop()
    {
        if (_drawSelectionBoxCoroutine != null)
        {
            _coroutineRunner.EndCoroutine(_drawSelectionBoxCoroutine);

            if (_selectionBorder != null)
                _selectionBorder.enabled = false;
        }
    }

    private IEnumerator DrawSelectionBoxCoroutine()
    {
        _selectionBorder.enabled = true;
        _selectionBorder.rectTransform.anchoredPosition = _firstPosition;

        while (_selectionBorder.enabled)
        {
            _secondPosition = Input.mousePosition;
            float width = _secondPosition.x - _firstPosition.x;

            if (width <= 0)
                width = 5f;

            float height = _firstPosition.y - _secondPosition.y;

            if (height <= 0)
                height = 5f;

            _selectionBorder.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
            _selectionBorder.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);

            yield return null;
        }
    }
}
