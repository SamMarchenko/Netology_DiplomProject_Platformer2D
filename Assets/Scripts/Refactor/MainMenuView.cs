using UnityEngine;
using UnityEngine.UI;

namespace Refactor
{
    public class MainMenuView : MonoBehaviour, IView
    {
        [SerializeField] private Button _newGameButton;
        [SerializeField] private Button _currentLevelButton;
        [SerializeField] private Button _exitButton;
        [SerializeField] private Button _controllScreenButton;

        public Button NewGameBtn => _newGameButton;
        public Button CurrentLvlBtn => _currentLevelButton;
        public Button ExitBtn => _exitButton;
        public Button ControlsScreenBtn => _controllScreenButton;
        
        
        public void Open()
        {
            gameObject.SetActive(true);    
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }
    }
}