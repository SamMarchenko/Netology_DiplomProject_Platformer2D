// using UnityEngine;
//
// namespace DefaultNamespace.Factories
// {
//     public class PassiveEnemyFactory
//     {
//         public PassiveEnemyView CreateEnemy(EnemyData data)
//         {
//             //todo: не нравится, что приходится преобразование делать!
//             var view = (PassiveEnemyView) MonoBehaviour.Instantiate(data.Prefab, data.SpawnPosition.position, Quaternion.identity);
//             var model = new PassiveBaseEnemyModel(data);
//             var controller = new PassiveEnemyController(model, view);
//
//             Debug.Log($"Создан враг {model.Type}, на позиции {view.transform.position}");
//             return view;
//         } 
//     }
// }