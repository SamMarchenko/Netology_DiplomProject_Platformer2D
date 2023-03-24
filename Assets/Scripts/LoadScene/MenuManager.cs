using UnityEngine;
using UnityEngine.UI;


public class MenuManager : MonoBehaviour
{
    [SerializeField] private Button _newGameButton;

    void Start()
    {
        _newGameButton.onClick.AddListener(LoadCoreScene);
    }

    public void LoadCoreScene()
    {
        Debug.Log("Клик по кнопке");
        SceneTransition.SwitchToScene("Core");
    }
}