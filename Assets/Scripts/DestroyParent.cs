using DefaultNamespace;
using UnityEngine;

public class DestroyParent : MonoBehaviour
{
    private EnemyView _enemy;
    private void Start()
    {
        _enemy =  transform.root.GetComponent<EnemyView>();
    }

    public void SignalEnemyIsDead()
    {
        _enemy.Dead();
    }
}
