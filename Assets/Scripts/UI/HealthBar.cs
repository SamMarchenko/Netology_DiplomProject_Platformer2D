using DefaultNamespace.Players.MVC;
using DefaultNamespace.Signals;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class HealthBar : MonoBehaviour, IPlayerDamageListener, IPlayerHealListener
{
    private int _playerHealthMax;
    private int _playerHealthCurrent;
    [SerializeField] private Image _totalHealthBar;
    [SerializeField] private Image _currentHealthBar;
    private PlayerController _playerController;

    [Inject]
    public void Construct(PlayerController playerController)
    {
        _playerController = playerController;
        SetHealthBar();
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