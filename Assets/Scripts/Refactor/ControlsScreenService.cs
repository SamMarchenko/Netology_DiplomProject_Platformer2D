using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Refactor
{
    public class ControlsScreenService : MonoBehaviour
    {
        [SerializeField] private ControlsScreenView _view;
        private List<Button> _buttons = new List<Button>();
    }
}