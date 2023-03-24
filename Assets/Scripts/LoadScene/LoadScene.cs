using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{
    private AsyncOperation asyncOperation;
    public Image LoadBar;
    public TMP_Text BarText;
    public int SceneId;

    private void Start()
    {
        StartCoroutine(LoadSceneCor());
    }

    IEnumerator LoadSceneCor()
    {
        yield return new WaitForSeconds(1f);
        asyncOperation = SceneManager.LoadSceneAsync(SceneId);
        while (!asyncOperation.isDone)
        {
            float progress = asyncOperation.progress / 0.9f;
            LoadBar.fillAmount = progress;
            BarText.text = "Загрузка" + string.Format("{0:0}%", progress * 100f);
            yield return 0;
        }
    }
}
