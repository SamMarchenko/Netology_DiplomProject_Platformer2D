using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Refactor.States
{
    public class LoadingCurtain : MonoBehaviour
    {
        [SerializeField] CanvasGroup Curtain;
        [SerializeField] private TMP_Text _loadingPercentageText;
        [SerializeField] private Slider _loadingProgressBar;

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        public void Show()
        {
            gameObject.SetActive(true);
            Curtain.alpha = 1;
        }

        public void UpdateProgress(float progress)
        {
            _loadingPercentageText.text = progress * 100 + "%";
            _loadingProgressBar.value = progress;
        }

        public void Hide() => StartCoroutine(DoFadeIn());

        private IEnumerator DoFadeIn()
        {
            while (Curtain.alpha > 0)
            {
                Curtain.alpha -= 0.03f;
                yield return new WaitForSeconds(0.03f);
            }

            gameObject.SetActive(false);
        }
    }
}