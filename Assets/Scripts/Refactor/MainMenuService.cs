using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Refactor
{
    public class MainMenuService : MonoBehaviour
    {
        [SerializeField] private MainMenuView _mainMenuView;
        [SerializeField] private ControlsScreenView _controlsScreenView;
        private List<Button> _mainMenuBtns = new List<Button>();
        private List<Button> _controlsScreenBtns = new List<Button>();


        public void OpenMainMenu()
        {
            _mainMenuView.Open();
            _controlsScreenView.Close();
        }

        public void OpenControls()
        {
            _controlsScreenView.Open();
            _mainMenuView.Close();
        }

        private void Start()
        {
            Subscribe();
        }

        private void Subscribe()
        {
            _mainMenuBtns.Add(_mainMenuView.NewGameBtn);
            _mainMenuView.NewGameBtn.onClick.AddListener(LoadNewGame);
            
            _mainMenuBtns.Add(_mainMenuView.ExitBtn);
            _mainMenuView.ExitBtn.onClick.AddListener(ExitGame);
            
            _mainMenuBtns.Add(_mainMenuView.CurrentLvlBtn);
            _mainMenuView.CurrentLvlBtn.onClick.AddListener(LoadCurrentLevel);
            
            _mainMenuBtns.Add(_mainMenuView.ControlsScreenBtn);  
            _mainMenuView.ControlsScreenBtn.onClick.AddListener(OpenControlsScreen);
            
            _controlsScreenBtns.Add(_controlsScreenView.CloseScreenButton);
            _controlsScreenView.CloseScreenButton.onClick.AddListener(CloseControlsScreen);
        }

        private void CloseControlsScreen()
        {
            _controlsScreenView.Close();
            _mainMenuView.Open();
        }

        private void OpenControlsScreen()
        {
            _controlsScreenView.Open();
            _mainMenuView.Close();
        }

        private void LoadCurrentLevel()
        {
            Debug.Log("LoadCurrentLevel");
        }

        private void ExitGame()
        {
            Debug.Log("ExitGame");
        }

        private void LoadNewGame()
        {
            Debug.Log("LoadNewGame");
        }

        private void OnDisable()
        {
            UnSubscribe();
        }

        private void UnSubscribe()
        {
            foreach (var button in _mainMenuBtns)
            {
                button.onClick.RemoveAllListeners();
            }
            _mainMenuBtns.Clear();
            
            foreach (var button in _controlsScreenBtns)
            {
                button.onClick.RemoveAllListeners();
            }
            _controlsScreenBtns.Clear();
           
        }
    }
}