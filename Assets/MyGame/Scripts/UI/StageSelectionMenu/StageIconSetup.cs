using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Base.UI.StageSelection
{
    public class StageIconSetup : MonoBehaviour
    {
        [SerializeField] private StageIconView _view;

        private StageIconModel _model;
        private StageIconPresenter _presenter;

        public StageIconModel CreateModel(bool unlocked, string stageName)
        {
            _view.Init(unlocked, stageName);
            _model = new StageIconModel(stageName, unlocked);

            _presenter = new StageIconPresenter(_view, _model);
            _presenter.Enable();

            return _model;
        }
    }

    public class StageIconPresenter
    {
        private readonly StageIconView _view;
        private readonly StageIconModel _model;

        public StageIconPresenter(StageIconView view, StageIconModel model)
        {
            _view = view;
            _model = model;
        }

        public void Enable()
        {
            _view.Clicked += OnViewCLicked;

            _model.BorderEnabled += OnBorderEnabled;
            _model.BorderDisabled += OnBorderDisabled;
        }

        public void Disable()
        {
            _view.Clicked -= OnViewCLicked;

            _model.BorderEnabled -= OnBorderEnabled;
            _model.BorderDisabled -= OnBorderDisabled;
        }

        private void OnBorderEnabled()
        {
            _view.ShowBorder();
        }

        private void OnBorderDisabled()
        {
            _view.HideBorder();
        }

        private void OnViewCLicked(StageIconView view)
        {
            _model.Choose();
        }
    }

    public class StageIconModel
    {
        private readonly string _name;
        private bool _unlocked;

        public StageIconModel(string name, bool unlocked)
        {
            _name = name;
            _unlocked = unlocked;
        }

        public string Name => _name;

        public event Action<string> Choosed;
        public event Action BorderEnabled;
        public event Action BorderDisabled;

        public void Choose()
        {
            Choosed?.Invoke(_name);
        }

        public void ShowBorder()
        {
            BorderEnabled?.Invoke();
        }

        public void HideBorder()
        {
            BorderDisabled?.Invoke();
        }
    }
}
