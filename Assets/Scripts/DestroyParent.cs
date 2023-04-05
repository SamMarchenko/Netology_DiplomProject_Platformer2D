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
        //вызывается в конце анимации взрыва
        _enemy.Dead();
    }
}
