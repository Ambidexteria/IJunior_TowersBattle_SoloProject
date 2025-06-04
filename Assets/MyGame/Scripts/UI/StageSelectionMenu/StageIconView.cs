using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Base.UI.StageSelection
{
    public class StageIconView : MonoBehaviour
    {
        [SerializeField] private Image _lock;
        [SerializeField] private Image _border;
        [SerializeField] private TextMeshProUGUI _stageName;
        [SerializeField] private ButtonClickHandler _button;

        public event Action<string> Choosed;

        private void OnEnable()
        {
            _button.Clicked += OnButtonCLicked;
        }

        private void OnDisable()
        {
            _button.Clicked -= OnButtonCLicked;
        }

        public void Init(bool unlocked, string stageName)
        {
            _stageName.text = stageName;

            if (unlocked)
                Unlock();
        }

        public void Unlock()
        {
            _lock.enabled = false;
            _button.Enable();
        }

        public void ShowBorder()
        {
            _border.enabled = true;
        }

        public void HideBorder()
        {
            _border.enabled = false;
        }

        private void OnButtonCLicked()
        {
            ShowBorder();
            Choosed?.Invoke(_stageName.text);
        }
    }
}
