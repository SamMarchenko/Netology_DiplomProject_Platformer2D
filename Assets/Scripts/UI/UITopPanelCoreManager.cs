using DefaultNamespace.Players.MVC;
using DefaultNamespace.Signals;
using DefaultNamespace.UI;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UITopPanelCoreManager : MonoBehaviour, IPlayerDamageListener, IPlayerHealListener
{
    private int _playerHealthMax;
    private int _playerHealthCurrent;
    [SerializeField] private Image _totalHealthBar;
    [SerializeField] private Image _currentHealthBar;
    [SerializeField] private Button _pauseButton;
    private PauseWindowManager _pauseWindowPrefab;
    private PauseWindowManager _pauseWindowManager;
    private PlayerController _playerController;
    private PauseSignalBus _pauseSignalBus;
    private ExitLevelSignalBus _exitLevelSignalBus;

    [Inject]
    public void Construct(PlayerController playerController, PauseSignalBus pauseSignalBus, ExitLevelSignalBus exitLevelSignalBus, PauseWindowManager pauseWindowPrefab)
    {
        _playerController = playerController;
        SetHealthBar();
        _pauseButton.onClick.AddListener(OnPauseButtonClick);
        _pauseSignalBus = pauseSignalBus;
        _exitLevelSignalBus = exitLevelSignalBus;
        _pauseWindowPrefab = pauseWindowPrefab;
    }

    private void OnPauseButtonClick()
    {
        //_ControllScreen.gameObject.SetActive(true);
        if (_pauseWindowManager == null)
        {
            _pauseWindowManager = Instantiate(_pauseWindowPrefab);
            _pauseWindowManager.Construct(_pauseSignalBus, _exitLevelSignalBus);
            _pauseWindowManager.transform.SetParent(transform);
            _pauseWindowManager.transform.localPosition = Vector3.zero;
        }
        else
        {
            _pauseWindowManager.gameObject.SetActive(true);
        }
        Time.timeScale = 0;
        _pauseSignalBus.Pause(new PauseSignal(true));
    }


    public void OnPlayerDamage(PlayerDamageSignal signal)
    {
        UpdateHealthBar();
    }

    private void SetHealthBar()
    {
        _playerHealthMax = _playerController.GetCurrentPlayerHealth();
        _playerHealthCurrent = _playerHealthMax;
        _totalHealthBar.fillAmount = (float) _playerHealthMax / 10;
        _currentHealthBar.fillAmount = _totalHealthBar.fillAmount;
    }

    private void UpdateHealthBar()
    {
        _playerHealthCurrent = _playerController.GetCurrentPlayerHealth();

        if (_playerHealthMax < _playerHealthCurrent)
        {
            _playerHealthMax = _playerHealthCurrent;
            _totalHealthBar.fillAmount = (float) _playerHealthMax / 10;
        }

        _currentHealthBar.fillAmount = (float) _playerHealthCurrent / 10;
    }

    public void OnPlayerHeal(PlayerHealSignal signal)
    {
        UpdateHealthBar();
    }
}