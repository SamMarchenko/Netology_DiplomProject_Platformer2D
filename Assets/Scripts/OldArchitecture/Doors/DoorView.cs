using System;
using UnityEngine;

namespace DefaultNamespace.Doors
{
    public class DoorView : MonoBehaviour
    {
        [SerializeField] private Sprite[] _sprites = new Sprite[2];
        [SerializeField] private SpriteRenderer _spriteRenderer;

        public bool IsLocked { get; set; } = true;
        public Action OnPlayerEntered;
        
        public void Open()
        {
            _spriteRenderer.sprite = _sprites[1];
            IsLocked = false;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (!col.isTrigger && col.gameObject.CompareTag("Player") && !IsLocked)
            {
                OnPlayerEntered?.Invoke();
            }
        }
    }
}