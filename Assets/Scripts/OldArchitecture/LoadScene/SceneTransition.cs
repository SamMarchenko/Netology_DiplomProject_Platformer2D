using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    private static SceneTransition s_instance;
    private static bool s_shouldPlayOpenigAnimation = false;


    [SerializeField] private Animator _animator;
    [SerializeField] private TMP_Text _loadingPercentageText;
    [SerializeField] private Slider _loadingProgressBar;

    private AsyncOperation _loadingSceneOperation;


    public static void SwitchToScene(string sceneName)
    {
        s_instance._animator.SetTrigger("sceneClosing");

        s_instance._loadingSceneOperation = SceneManager.LoadSceneAsync(sceneName);
        s_instance._loadingSceneOperation.allowSceneActivation = false;
    }

    private void Start()
    {
        s_instance = this;

        if (s_shouldPlayOpenigAnimation)
        {
            _animator.SetTrigger("sceneOpening");
        }
    }

    private void Update()
    {
        if (_loadingSceneOperation != null)
        {
            _loadingPercentageText.text = Mathf.RoundToInt(_loadingSceneOperation.progress * 100) + "%";
            _loadingProgressBar.value = _loadingSceneOperation.progress;
        }
    }

    public void OnAnimationOver()
    {
        s_shouldPlayOpenigAnimation = true;
        _loadingSceneOperation.allowSceneActivation = true;
    }
}