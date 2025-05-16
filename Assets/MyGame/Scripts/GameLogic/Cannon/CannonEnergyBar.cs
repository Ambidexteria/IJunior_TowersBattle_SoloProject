using Base.Infrastructure;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Base.GameLogic.Cannon
{
    public class CannonEnergyBar
    {
        private readonly ControlPointDatabase _controlPointDatabase;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly Team _team;

        private List<ControlPoint> _controlPoints = new();
        private int _energyIncome = 0;
        private float _currentEnergy = 0;
        private float _energyMax = 100f;
        private bool _active = true;
        private Coroutine _coroutine;

        public CannonEnergyBar(Team team, ControlPointDatabase controlPointDatabase, float maxEnergy, ICoroutineRunner coroutineRunner)
        {
            Debug.LogWarning($"{controlPointDatabase} INITIATED = {controlPointDatabase != null}");
            _team = team;
            _controlPointDatabase = controlPointDatabase;
            _energyMax = maxEnergy;
            _coroutineRunner = coroutineRunner;
        }

        public float MaxEnergy => _energyMax;

        public event Action Filled;
        public event Action<float> CurrentEnergyChanged;

        public void Enable()
        {
            _active = true;

            if(_coroutine != null)
                _coroutineRunner.StopCoroutine(_coroutine);

            _coroutine = _coroutineRunner.StartCoroutine(Update());

            _controlPointDatabase.ControlPointCaptured += OnControlPointCaptured;
        }

        public void Disable()
        {
            _active = false;

            if (_coroutine != null)
                _coroutineRunner.StopCoroutine(_coroutine);

            _controlPointDatabase.ControlPointCaptured -= OnControlPointCaptured;
        }

        public void RemoveCurrentEnergy()
        {
            _currentEnergy = 0;

            CurrentEnergyChanged?.Invoke(_currentEnergy);
        }

        private IEnumerator Update()
        {
            while (_active)
            {
                if (_energyIncome > 0 && _currentEnergy < _energyMax)
                    AddEnergy();

                yield return null;
            }
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
            if (controlPoint.Team == _team.Type)
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
