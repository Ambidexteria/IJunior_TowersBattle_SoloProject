using System;
using System.Collections;
using System.Collections.Generic;
using Base.Infrastructure;
using UnityEngine;

namespace Base.GameLogic.Cannon
{
    public class CannonEnergyBarModel
    {
        private const int DefaultEnergyIncomeMultiplyer = 1;

        private readonly ControlPointDatabase _controlPointDatabase;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly Team _team;
        private readonly float _energyMax = 100f;
        private readonly List<ControlPoint> _controlPoints;

        private int _energyIncome = 0;
        private int _energyIncomeMultiplyer;
        private float _currentEnergy = 0;
        private bool _enabled = false;
        private Coroutine _coroutine;

        public CannonEnergyBarModel(Team team, ControlPointDatabase controlPointDatabase, float maxEnergy, ICoroutineRunner coroutineRunner)
        {
            _team = team;
            _controlPointDatabase = controlPointDatabase;
            _energyMax = maxEnergy;
            _coroutineRunner = coroutineRunner;
            _controlPoints = new List<ControlPoint>();

            _energyIncomeMultiplyer = DefaultEnergyIncomeMultiplyer;
        }

        public event Action Filled;
        public event Action<float> CurrentEnergyChanged;
        public event Action<int> EnergyIncomeChanged;

        public float MaxEnergy => _energyMax;

        public void Enable()
        {
            if (_enabled)
                return;

            _enabled = true;
            StopCoroutine();
            _coroutine = _coroutineRunner.LaunchCoroutine(ChangeEnergyCoroutine());

            _controlPointDatabase.ControlPointCaptured += OnControlPointCaptured;
            EnergyIncomeChanged?.Invoke(_energyIncome);
        }

        public void Disable()
        {
            if (_enabled == false)
                return;

            _enabled = false;
            StopCoroutine();

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

        private IEnumerator ChangeEnergyCoroutine()
        {
            while (_enabled)
            {
                if (_energyIncome > 0 && _currentEnergy < _energyMax)
                    AddEnergy();

                yield return null;
            }
        }

        private void StopCoroutine()
        {
            if (_coroutine != null)
                _coroutineRunner?.EndCoroutine(_coroutine);
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
