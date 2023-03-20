using DefaultNamespace;
using UnityEngine;

public class EdgePlatformController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Я в триггере!!!");
        if (!col.isTrigger && col.CompareTag("Enemy"))
        {
            col.GetComponent<EnemyView>().OnTheEdgePlatform?.Invoke();
        }
    }
}