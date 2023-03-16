using UnityEngine;

namespace DefaultNamespace.Factories
{
    public class EnemyFactory
    {
        public EnemyView CreateEnemy(EnemyData data)
        {
            var enemyView = MonoBehaviour.Instantiate(data.Prefab, data.SpawnPosition.position, Quaternion.identity);
            var enemyModel = new EnemyModel(data);
            var enemyController = new EnemyController(enemyModel, enemyView);

            Debug.Log($"Создан враг {enemyModel.Type}, на позиции {enemyView.transform.position}");
            return enemyView;
        }
    }
}