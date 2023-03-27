using UnityEngine;

namespace DefaultNamespace
{
    public class PassiveEnemyView : EnemyView
    {
        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                OnConnectWithPlayer?.Invoke(EUnitType.Enemy);
            }
        }
    }
}