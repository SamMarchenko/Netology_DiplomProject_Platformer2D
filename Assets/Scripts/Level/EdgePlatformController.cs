using DefaultNamespace;
using UnityEngine;

public class EdgePlatformController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.isTrigger && col.CompareTag("Enemy"))
        {
            col.GetComponent<EnemyView>().OnTheEdgePlatform?.Invoke();
        }
    }
}