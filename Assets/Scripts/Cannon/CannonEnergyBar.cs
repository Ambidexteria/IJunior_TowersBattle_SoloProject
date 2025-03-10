using System;
using System.Collections.Generic;
using UnityEngine;

public class CannonEnergyBar : MonoBehaviour
{
    [SerializeField] private ControlPointDatabase _controlPointDatabase;
    [SerializeField] private List<ControlPoint> _controlPoints;
    [SerializeField] private TeamType _team;
    [SerializeField] private int _energyIncome = 0;
    [SerializeField] private float _currentEnergy = 0;
    [SerializeField] private int _energyMax = 100;

    public event Action Filled;
    public event Action<float> CurrentEnergyChanged;

    private void OnEnable()
    {
        _controlPointDatabase.ControlPointCaptured += OnControlPointCaptured;
    }

    private void OnDisable()
    {
        _controlPointDatabase.ControlPointCaptured -= OnControlPointCaptured;
    }

    private void Update()
    {
        if (_energyIncome > 0 && _currentEnergy < _energyMax)
            AddEnergy();
    }

    private void AddEnergy()
    {
        _currentEnergy += _energyIncome * Time.deltaTime;
        _currentEnergy = Mathf.Clamp(_currentEnergy, 0, _energyMax);
        CurrentEnergyChanged?.Invoke(_currentEnergy);

        if (_currentEnergy >= _energyMax)
        {
            Filled?.Invoke();
            RemoveCurrentEnergy();
        }
    }

    private void RemoveCurrentEnergy()
    {
        _currentEnergy = 0;

        CurrentEnergyChanged?.Invoke(_currentEnergy);
    }

    private void OnControlPointCaptured(ControlPoint controlPoint)
    {
        if (controlPoint.Team == _team)
        {
            _controlPoints.Add(controlPoint);
            _energyIncome += controlPoint.EnergyRate;
        }
        else
        {
            if (_controlPoints.Contains(controlPoint))
            {
                _controlPoints.Remove(controlPoint);
                _energyIncome -= controlPoint.EnergyRate;
            }
        }
    }
}
