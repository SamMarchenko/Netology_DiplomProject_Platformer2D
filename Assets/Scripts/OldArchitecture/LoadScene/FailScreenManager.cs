using DefaultNamespace;
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

        int currentLevelNumber = PlayerPrefs.GetInt(SavesStrings.CurrentLevel);
        _currentlLevelName = "Level" + currentLevelNumber;
    }


    public void LoadMenuScene()
    {
        PlayerPrefs.SetInt(SavesStrings.IsNewGame, 0);
        SceneTransition.SwitchToScene("MainMenu");
    }

    private void LoadCurrentLevelScene()
    {
        PlayerPrefs.SetInt(SavesStrings.IsNewGame, 0);
        SceneTransition.SwitchToScene(_currentlLevelName);
    }

    private void OnDisable()
    {
        _repeatLevelButton.onClick.RemoveAllListeners();
        _exitButton.onClick.RemoveAllListeners();
    }
}