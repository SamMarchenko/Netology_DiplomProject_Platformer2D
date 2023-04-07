using System;
using DefaultNamespace.Signals;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace DefaultNamespace.UI
{
    public class PauseWindowManager : MonoBehaviour
    {
        [SerializeField] private Button _controlScreenButton;
        [SerializeField] private Button _exitLevelButton;
        [SerializeField] private Button _closeWindowButton;
        [SerializeField] private ControllsScreen _controlScreen;
        private PauseSignalBus _pauseSignalBus;
        private ExitLevelSignalBus _exitLevelSignalBus;

        public void Construct(PauseSignalBus pauseSignalBus, ExitLevelSignalBus exitLevelSignalBus)
        {
            _pauseSignalBus = pauseSignalBus;
            _exitLevelSignalBus = exitLevelSignalBus;
        }

        private void Start()
        {
            _controlScreenButton.onClick.AddListener(OpenControlsWindow);
            _exitLevelButton.onClick.AddListener(ExitLevel);
            _closeWindowButton.onClick.AddListener(ClosePauseWindow);
        }

        private void ClosePauseWindow()
        {
            Time.timeScale = 1;
            _pauseSignalBus.Pause(new PauseSignal(false));
            gameObject.SetActive(false);
        }

        private void ExitLevel()
        {
            PlayerPrefs.SetInt(SavesStrings.IsNewGame, 0);
            _exitLevelSignalBus.ExitLevel(new ExitLevelSignal());
            SceneTransition.SwitchToScene("MainMenu");
            ClosePauseWindow();
        }

        private void OpenControlsWindow()
        {
            Debug.Log("OpenControlsWindow");
            _controlScreen.gameObject.SetActive(true);
        }

        private void OnDestroy()
        {
            _controlScreenButton.onClick.RemoveAllListeners();
            _exitLevelButton.onClick.RemoveAllListeners();
            _closeWindowButton.onClick.RemoveAllListeners();
        }
    }
}