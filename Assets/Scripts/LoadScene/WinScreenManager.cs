using System;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class WinScreenManager : MonoBehaviour
{
    [SerializeField] private int _maxLevelsCount = 2;
    [SerializeField] private Button _nextLevelButton;
    [SerializeField] private Button _exitButton;
    private string _currentlLevelName;
    private int _currentLevelNumber;
    
    void Start()
    {
        _nextLevelButton.onClick.AddListener(LoadNextLevelScene);
        _exitButton.onClick.AddListener(LoadMenuScene);
        
        
        _currentLevelNumber = PlayerPrefs.GetInt(SavesStrings.CurrentLevel);
        if (_currentLevelNumber > 0 && _currentLevelNumber <= _maxLevelsCount)
        {
            _nextLevelButton.interactable = true;
            _currentlLevelName = "Level" + _currentLevelNumber;
        }
        else
        {
            _nextLevelButton.gameObject.SetActive(false);
        }
        
        _currentlLevelName = "Level" + _currentLevelNumber;
    }
    
    private void LoadNextLevelScene()
    {
        PlayerPrefs.SetInt(SavesStrings.IsNewGame, 0);
        SceneTransition.SwitchToScene(_currentlLevelName);
    }
    
    public void LoadMenuScene()
    {
        if (_currentLevelNumber > 0 && _currentLevelNumber <= _maxLevelsCount)
        {
            PlayerPrefs.SetInt(SavesStrings.IsNewGame, 0);
        }
        else
        {
            PlayerPrefs.SetInt(SavesStrings.IsNewGame, 1);
        }
        
        SceneTransition.SwitchToScene("MainMenu");
    }

    private void OnDisable()
    {
        _nextLevelButton.onClick.RemoveAllListeners();
        _exitButton.onClick.RemoveAllListeners();
    }
}