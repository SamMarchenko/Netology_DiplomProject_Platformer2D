using UnityEngine;
using UnityEngine.UI;

public class FailScreenManager : MonoBehaviour
{
    [SerializeField] private Button _repeatLevelButton;
    [SerializeField] private Button _exitButton;
    private string _currentlLevelName;

    void Start()
    {
        _repeatLevelButton.onClick.AddListener(LoadCurrentLevelScene);
        _exitButton.onClick.AddListener(LoadMenuScene);

        int currentLevelNumber = PlayerPrefs.GetInt("currentLevel");
        _currentlLevelName = "Level" + currentLevelNumber;
    }


    public void LoadMenuScene()
    {
        SceneTransition.SwitchToScene("MainMenu");
    }

    private void LoadCurrentLevelScene()
    {
        SceneTransition.SwitchToScene(_currentlLevelName);
    }

    private void OnDisable()
    {
        _repeatLevelButton.onClick.RemoveAllListeners();
        _exitButton.onClick.RemoveAllListeners();
    }
}