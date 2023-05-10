using System;
using DefaultNamespace.Signals;
using UnityEngine;

namespace DefaultNamespace.UI
{
    public class InventaryManager : MonoBehaviour, IInventaryUpdateListener
    {
        [SerializeField] private InventaryView[] _inventaryViews;

        private void Start()
        {
            if (PlayerPrefs.GetInt(SavesStrings.PlayerHasShield) == 0)
            {
                _inventaryViews[0].TurnOffImage();
            }
            else if ((PlayerPrefs.GetInt(SavesStrings.PlayerHasShield) == 1))
            {
                _inventaryViews[0].TurnOnImage();
            }
        }

        public void OnInventaryUpdate(InventarySignal signal)
        {
            switch (signal.Type)
            {
                case EInventaryType.Shield:
                    _inventaryViews[0].TurnOnImage();
                    break;
                case EInventaryType.Weapon:
                    _inventaryViews[1].SwitchImage();
                    break;
            }
        }
    }
}