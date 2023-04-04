using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private int _maxLevelsCount = 2;
    [SerializeField] private Button _newGameButton;
    [SerializeField] private Button _currentLevelButton;
    private string _currentlLevelName;
    private bool _buttonClicked;

    void Start()
    {
        _buttonClicked = false;
        PlayerPrefs.SetInt("levelsCount", _maxLevelsCount);

        _currentLevelButton.interactable = false;

        _newGameButton.onClick.AddListener(LoadFirstLevelScene);
        _currentLevelButton.onClick.AddListener(LoadCurrentLevelScene);

        int currentLevelNumber = PlayerPrefs.GetInt("currentLevel");
        if (currentLevelNumber > 0 && currentLevelNumber <= _maxLevelsCount)
        {
            _currentLevelButton.interactable = true;
            _currentlLevelName = "Level" + currentLevelNumber;
        }
        else
        {
            _currentLevelButton.interactable = false;
            PlayerPrefs.SetInt("currentLevel", 1);
        }
    }


    public void LoadFirstLevelScene()
    {
        if (_buttonClicked)
        {
            return;
        }

        _buttonClicked = true;
        PlayerPrefs.SetInt("PlayerHasShield",0);
        PlayerPrefs.SetInt("currentLevel", 1);
        SceneTransition.SwitchToScene("Level1");
    }

    private void LoadCurrentLevelScene()
    {
        if (_buttonClicked)
        {
            return;
        }

        _buttonClicked = true;
        SceneTransition.SwitchToScene(_currentlLevelName);
    }

    private void OnDisable()
    {
        _newGameButton.onClick.RemoveAllListeners();
        _currentLevelButton.onClick.RemoveAllListeners();
    }
}