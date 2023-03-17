using UnityEngine;

namespace DefaultNamespace.Factories
{
    public class WalkingEnemyFactory
    {
        public WalkingEnemyView CreateEnemy(EnemyData data)
        {
            //todo: не нравится, что приходится преобразование делать!
            var view = (WalkingEnemyView) MonoBehaviour.Instantiate(data.Prefab, data.SpawnPosition.position, Quaternion.identity);
            var model = new WalkingEnemyModel(data);
            var controller = new WalkingEnemyController(model, view);

            Debug.Log($"Создан враг {model.Type}, на позиции {view.transform.position}");
            return view;
        } 
    }
}