using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.UI
{
    public class InventaryView : MonoBehaviour
    {
        [SerializeField] private EInventaryType _type;
        [SerializeField] private Image[] _images;

        public EInventaryType Type => _type;

        public void SwitchImage()
        {
            foreach (var image in _images)
            {
                image.gameObject.SetActive(!image.gameObject.activeSelf);
            }
        }

        public void TurnOnImage()
        {
            foreach (var image in _images)
            {
                image.color = Color.white;
            }
        }
        public void TurnOffImage()
        {
            foreach (var image in _images)
            {
                image.color = Color.black;
            }
        }
    }
}