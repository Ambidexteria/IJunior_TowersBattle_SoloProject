using System.Collections.Generic;
using UnityEngine;

public class PathFollower : MonoBehaviour
{
    [SerializeField] private int _maxPoints = 10;
    [SerializeField] private float _yOffsetCoefficient = 1.5f;

    private readonly List<Vector3> _points = new();

    private float _speed;
    private int _currentPointIndex;
    private bool _enabled = false;

    private void Update()
    {
        if (_enabled == false)
            return;

        Place();
    }

    public void StartMovement(float speed, Vector3 start, Vector3 finish)
    {
        _points.Clear();
        _currentPointIndex = 0;

        _points.Add(start);
        _points.Add(finish);

        CalculateLinearPath();
        ConvertLinearPathToSinusoidal();

        _speed = speed;
        _enabled = true;
    }

    private void Place()
    {
        transform.position = Vector3.MoveTowards(transform.position, _points[_currentPointIndex], _speed * Time.deltaTime);

        if (transform.position == _points[_currentPointIndex])
        {
            if (_currentPointIndex < _points.Count - 1)
                _currentPointIndex++;
            else
                _enabled = false;
        }
    }

    private void CalculateLinearPath()
    {
        Vector3 newPoint;
        float newVectordistance = Vector3.Distance(_points[1], _points[0]) / _maxPoints;
        Vector3 direction = (_points[1] - _points[0]).normalized * newVectordistance;

        for (int i = 0; i < _maxPoints - 1; i++)
        {
            newPoint = _points[i] + direction;
            _points.Insert(i + 1, newPoint);
        }
    }

    private void ConvertLinearPathToSinusoidal()
    {
        float yOffset;
        float part;
        Vector3 tempPoint;

        for (int i = 0; i < _points.Count; i++)
        {
            tempPoint = _points[i];
            part = i / (float)(_points.Count - 1);
            yOffset = _yOffsetCoefficient * Mathf.Sin(Mathf.PI * part);
            tempPoint.y += yOffset;
            _points[i] = tempPoint;
        }
    }
}
