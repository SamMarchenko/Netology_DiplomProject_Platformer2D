using UnityEngine;

namespace DefaultNamespace.Factories
{
    public class PeekOutEnemyFactory
    {
        public PeekOutEnemyView CreateEnemy(EnemyData data)
        {
            //todo: не нравится, что приходится преобразование делать!
            var view = (PeekOutEnemyView) MonoBehaviour.Instantiate(data.Prefab, data.SpawnPosition.position, Quaternion.identity);
            var model = new PeekOutEnemyModel(data);
            var controller = new PeekOutEnemyController(model, view);

            Debug.Log($"Создан враг {model.Type}, на позиции {view.transform.position}");
            return view;
        } 
    }
}