using UnityEngine;
using UnityEngine.UI;

namespace Refactor
{
    public class ControlsScreenView : MonoBehaviour, IView
    {
        [SerializeField] private Button _closeScreenButton;

        public Button CloseScreenButton => _closeScreenButton;
        
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