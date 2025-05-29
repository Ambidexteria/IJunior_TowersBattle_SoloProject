using System.Collections;
using UnityEngine;

namespace Base.Infrastructure
{ 
    [RequireComponent (typeof(CanvasGroup))]
    public class LoadingCurtain : MonoBehaviour
    {
        private const float MaxAlpha = 1f;

        [SerializeField] private float _startDelay = 1.0f;        
        [SerializeField] private float _fadeSpeed = 10f;

        private CanvasGroup _curtain;
        private WaitForSeconds _wait;

        private void Awake()
        {
            _curtain = GetComponent<CanvasGroup>();
            _wait = new WaitForSeconds(_startDelay);

            DontDestroyOnLoad(gameObject);
        }

        public void Show()
        {
            gameObject.SetActive(true);
            _curtain.alpha = MaxAlpha;
        }

        public void Hide()
        {
            StartCoroutine(FadeIn());
        }

        private IEnumerator FadeIn()
        {
            yield return _wait;

            while (_curtain.alpha > 0)
            {
                _curtain.alpha -= _fadeSpeed * Time.deltaTime;
                yield return null;
            }

            gameObject.SetActive(false);
        }
    }
}
