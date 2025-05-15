using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Base.GameLogic.Cannon
{
    public class CannonEnergyBar : MonoBehaviour
    {
        [SerializeField] private ControlPointDatabase _controlPointDatabase;
        [SerializeField] private List<ControlPoint> _controlPoints;
        [SerializeField] private TeamType _team;
        [SerializeField] private int _energyIncome = 0;
        [SerializeField] private float _currentEnergy = 0;
        [SerializeField] private float _energyMax = 100f;

        private bool _active = true;

        [Inject]
        private void Init(ControlPointDatabase controlPointDatabase)
        {
            Debug.LogWarning($"{controlPointDatabase} INITIATED = {controlPointDatabase != null}");
            _controlPointDatabase = controlPointDatabase;
        }

        public float MaxEnergy => _energyMax;

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
            if (_active)
                if (_energyIncome > 0 && _currentEnergy < _energyMax)
                    AddEnergy();
        }

        public void Stop()
        {
            _active = false;
        }

        public void RemoveCurrentEnergy()
        {
            _currentEnergy = 0;

            CurrentEnergyChanged?.Invoke(_currentEnergy);
        }

        private void AddEnergy()
        {
            _currentEnergy += _energyIncome * Time.deltaTime;
            _currentEnergy = Mathf.Clamp(_currentEnergy, 0, _energyMax);
            CurrentEnergyChanged?.Invoke(_currentEnergy);

            if (_currentEnergy >= _energyMax)
            {
                Filled?.Invoke();
            }
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
}
