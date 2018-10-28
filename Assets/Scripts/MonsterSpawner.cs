using System.Collections.Generic;
using Components.AI;
using Extensions;
using Helpers;
using UnityEngine;

public class MonsterSpawner : Singleton<MonsterSpawner> {
    [SerializeField] private List<GameObject> _monsters;

    private GameObject       _playerBase;
    private GameObject       _enemyBase;
    private List<GameObject> _waypoints;

    private void Start() {
        Events.OnMonsterSpawnRequest += OnMonsterSpawnRequest;

        // Bad
        Events.OnPlayerBaseInitialized += delegate(GameObject       playerBase) { this._playerBase = playerBase; };
        Events.OnEnemyBaseInitialized  += delegate(GameObject       enemyBase) { this._enemyBase   = enemyBase; };
        Events.OnWaypointsInitialized  += delegate(List<GameObject> waypoints) { this._waypoints   = waypoints; };
    }

    private void OnMonsterSpawnRequest(int wave) {
        GameObject randomMonster = Instantiate(_monsters.RandomElementOrDefault(null), _enemyBase.transform.position, Quaternion.identity);
        MonsterAI  ai            = randomMonster.GetComponent<MonsterAI>();

        ai.SetWaypoints(_waypoints);
    }
}