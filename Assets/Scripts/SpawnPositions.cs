using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPositions : MonoBehaviour
{
    [SerializeField] private Transform _playerSpawnPosition;
    [SerializeField] private Transform[] _enemiesSpawnPositions;
    [SerializeField] private Transform _doorSpawnPosirion;
    public Transform PlayerSpawnPos => _playerSpawnPosition;
    public Transform[] EnemiesSpawnPositions => _enemiesSpawnPositions;
    public Transform DoorSpawnPosition => _doorSpawnPosirion;
}

