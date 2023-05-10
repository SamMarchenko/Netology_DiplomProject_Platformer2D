using UnityEngine;
using UnityEngine.UI;

public class ControllsScreen : MonoBehaviour
{
    [SerializeField] private Button _exitButton;

   

    private void Start()
    {
        _exitButton.onClick.AddListener(Exit);
    }

    private void Exit()
    {
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        _exitButton.onClick.RemoveAllListeners();
    }
}
