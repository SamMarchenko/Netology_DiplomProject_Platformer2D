using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private int _maxLevelsCount = 2;
    [SerializeField] private Button _newGameButton;
    [SerializeField] private Button _currentLevelButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _controllScreenButton;
    [SerializeField] private Image _controllScreen;
    private string _currentlLevelName;
    private bool _buttonClicked;

    void Start()
    {
        _buttonClicked = false;
        PlayerPrefs.SetInt(SavesStrings.LevelsCount, _maxLevelsCount);

        _currentLevelButton.interactable = false;

        _newGameButton.onClick.AddListener(LoadFirstLevelScene);
        _currentLevelButton.onClick.AddListener(LoadCurrentLevelScene);
        _exitButton.onClick.AddListener(ExitGame);
        _controllScreenButton.onClick.AddListener(OpenControllScreen);

        int currentLevelNumber = PlayerPrefs.GetInt(SavesStrings.CurrentLevel);
        if (PlayerPrefs.GetInt(SavesStrings.IsNewGame) == 0 && currentLevelNumber <= _maxLevelsCount)
        {
            _currentLevelButton.interactable = true;
            _currentlLevelName = "Level" + currentLevelNumber;
        }
        else
        {
            _currentLevelButton.interactable = false;
            PlayerPrefs.SetInt(SavesStrings.CurrentLevel, 1);
        }
    }

    private void OpenControllScreen()
    {
        _controllScreen.gameObject.SetActive(true);
    }

    private void ExitGame()
    {
        Application.Quit();
    }


    public void LoadFirstLevelScene()
    {
        if (_buttonClicked)
        {
            return;
        }

        _buttonClicked = true;
        PlayerPrefs.SetInt(SavesStrings.PlayerHasShield, 0);
        PlayerPrefs.SetInt(SavesStrings.CurrentLevel, 1);
        PlayerPrefs.SetInt(SavesStrings.IsNewGame, 1);
        SceneTransition.SwitchToScene("Level1");
    }

    private void LoadCurrentLevelScene()
    {
        if (_buttonClicked)
        {
            return;
        }

        _buttonClicked = true;
        PlayerPrefs.SetInt(SavesStrings.IsNewGame, 0);
        SceneTransition.SwitchToScene(_currentlLevelName);
    }

    private void OnDisable()
    {
        _newGameButton.onClick.RemoveAllListeners();
        _currentLevelButton.onClick.RemoveAllListeners();
    }
}