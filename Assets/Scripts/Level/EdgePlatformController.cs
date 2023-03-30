using DefaultNamespace;
using UnityEngine;

public class EdgePlatformController : MonoBehaviour
{
    [SerializeField] private EPlatformType _type;

    private void OnTriggerEnter2D(Collider2D col)
    {
        switch (_type)
        {
            case EPlatformType.WalkingEnemyPlatform:
            {
                if (!col.isTrigger && col.CompareTag("Enemy"))
                {
                    col.GetComponent<EnemyView>()?.OnTheEdgePlatform?.Invoke();
                }

                break;
            }
            case EPlatformType.FlyingEnemyPlatform:
                if (!col.isTrigger && col.CompareTag("Enemy"))
                {
                    col.GetComponent<EnemyView>().OnTheEdgePlatform?.Invoke();
                }

                break;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (_type == EPlatformType.FlyingEnemyPlatform)
        {
            if (!other.isTrigger && other.gameObject.CompareTag("Enemy"))
            {
                other.GetComponent<EnemyView>().OnFarFromPlatform?.Invoke();
            }
        }
    }
}