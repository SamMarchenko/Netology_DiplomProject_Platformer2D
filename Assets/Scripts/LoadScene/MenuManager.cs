using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Button _newGameButton;
    [SerializeField] private Button _currentLevelButton;
    private string _currentlLevelName;
void Start()
    {
        _newGameButton.onClick.AddListener(LoadFirstLevelScene);
        _currentLevelButton.onClick.AddListener(LoadCurrentLevelScene);
    }
    
    
    public void LoadFirstLevelScene()
    {
        PlayerPrefs.SetInt("currentLevel", 1);
        SceneTransition.SwitchToScene("Level1");
    }
    
    private void LoadCurrentLevelScene()
    {
        SceneTransition.SwitchToScene(_currentlLevelName);
    }

    private void OnDisable()
    {
        _newGameButton.onClick.RemoveAllListeners();
        _currentLevelButton.onClick.RemoveAllListeners();
    }
}