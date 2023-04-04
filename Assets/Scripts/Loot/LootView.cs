using System;
using DG.Tweening;
using UnityEngine;

namespace DefaultNamespace.Loot
{
    public class LootView : MonoBehaviour
    {
        [SerializeField] ELootType _lootType;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        public ELootType LootType => _lootType;
        public Action<LootView> OnPickUpLoot;
        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                OnPickUpLoot?.Invoke(this);
                _spriteRenderer.DOFade(0, 0.5f).OnComplete(() => Destroy(gameObject));
            }
        }
    }
}