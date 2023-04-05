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
    
    void Start()
    {
        _nextLevelButton.onClick.AddListener(LoadNextLevelScene);
        _exitButton.onClick.AddListener(LoadMenuScene);
        
        
        int currentLevelNumber = PlayerPrefs.GetInt(SavesStrings.CurrentLevel);
        if (currentLevelNumber > 0 && currentLevelNumber <= _maxLevelsCount)
        {
            _nextLevelButton.interactable = true;
            _currentlLevelName = "Level" + currentLevelNumber;
        }
        else
        {
            _nextLevelButton.gameObject.SetActive(false);
        }
        
        _currentlLevelName = "Level" + currentLevelNumber;
    }
    
    private void LoadNextLevelScene()
    {
        SceneTransition.SwitchToScene(_currentlLevelName);
    }
    
    public void LoadMenuScene()
    {
        SceneTransition.SwitchToScene("MainMenu");
    }

    private void OnDisable()
    {
        _nextLevelButton.onClick.RemoveAllListeners();
        _exitButton.onClick.RemoveAllListeners();
    }
}