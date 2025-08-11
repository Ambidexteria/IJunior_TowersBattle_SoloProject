using Base.Infrastructure;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Base.GameLogic.Cannon
{
    public class CannonEnergyBarModel
    {
        private const int DefaultEnergyIncomeMultiplyer = 1;

        private readonly ControlPointDatabase _controlPointDatabase;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly Team _team;

        private List<ControlPoint> _controlPoints = new();
        private int _energyIncome = 0;
        private int _energyIncomeMultiplyer;
        private float _currentEnergy = 0;
        private float _energyMax = 100f;
        private bool _active = true;
        private Coroutine _coroutine;

        public CannonEnergyBarModel(Team team, ControlPointDatabase controlPointDatabase, float maxEnergy, ICoroutineRunner coroutineRunner)
        {
            ExceptionsTest.NullRefMethodTest(nameof(CannonEnergyBarModel), "constructor", team, controlPointDatabase, coroutineRunner);

            _team = team;
            _controlPointDatabase = controlPointDatabase;
            _energyMax = maxEnergy;
            _coroutineRunner = coroutineRunner;

            _energyIncomeMultiplyer = DefaultEnergyIncomeMultiplyer;
        }

        public float MaxEnergy => _energyMax;

        public event Action<int> EnergyIncomeChanged;
        public event Action Filled;
        public event Action<float> CurrentEnergyChanged;

        public void Enable()
        {
            _active = true;

            if (_coroutine != null)
                _coroutineRunner.EndCoroutine(_coroutine);

            _coroutine = _coroutineRunner.LaunchCoroutine(Update());

            _controlPointDatabase.ControlPointCaptured += OnControlPointCaptured;
            EnergyIncomeChanged?.Invoke(_energyIncome);
        }

        public void Disable()
        {
            _active = false;

            if (_coroutine != null)
                _coroutineRunner.EndCoroutine(_coroutine);

            _controlPointDatabase.ControlPointCaptured -= OnControlPointCaptured;
        }

        public void RemoveCurrentEnergy()
        {
            _currentEnergy = 0;

            CurrentEnergyChanged?.Invoke(_currentEnergy);
        }

        public void MultiplyEnergyIncome(int multiplyer)
        {
            _energyIncomeMultiplyer = multiplyer;
        }

        public void RestoreDefaultEnergyIncome()
        {
            _energyIncomeMultiplyer = DefaultEnergyIncomeMultiplyer;
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
            _currentEnergy += _energyIncome * _energyIncomeMultiplyer * Time.deltaTime;
            _currentEnergy = Mathf.Clamp(_currentEnergy, 0, _energyMax);
            CurrentEnergyChanged?.Invoke(_currentEnergy);

            if (_currentEnergy >= _energyMax)
            {
                Filled?.Invoke();
            }
        }

        private void OnControlPointCaptured(ControlPoint controlPoint)
        {
            ExceptionsTest.NullRefMethodTest(nameof(CannonEnergyBarModel), nameof(OnControlPointCaptured), controlPoint);

            if (controlPoint.Team == _team.Type)
            {
                _controlPoints.Add(controlPoint);
                _energyIncome += controlPoint.EnergyRate;
                EnergyIncomeChanged?.Invoke(_energyIncome);
            }
            else
            {
                if (_controlPoints.Contains(controlPoint))
                {
                    _controlPoints.Remove(controlPoint);
                    _energyIncome -= controlPoint.EnergyRate;
                    EnergyIncomeChanged?.Invoke(_energyIncome);
                }
            }
        }
    }
}
