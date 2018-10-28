using System;
using System.Collections.Generic;
using Components.AI;
using UnityEngine;

public static class Events {
    public static event Action      OnWaveFinished        = delegate { };
    public static event Action<int> OnMonsterSpawnRequest = delegate { };

    public static event Action<GameObject>       OnPlayerBaseInitialized = delegate { };
    public static event Action<GameObject>       OnEnemyBaseInitialized  = delegate { };
    public static event Action<List<GameObject>> OnWaypointsInitialized  = delegate { };

    public static event Action<GameObject> OnEnemyReachPlayerBase = delegate { };
    public static event Action<GameObject> OnBeforeEnemyDestroyed = delegate { };

    public static event Action<PlayerBase> OnPlayerBaseTakeDamage = delegate { };
    public static event Action<PlayerBase> OnPlayerBaseDestroyed  = delegate { };

    public static event Action<bool> OnBuildingStateToggled = delegate { };

    public static void FireWaveFinishEvent() {
        OnWaveFinished();
    }

    public static void FireMonsterSpawnRequestEvent(int wave) {
        OnMonsterSpawnRequest(wave);
    }

    public static void FirePlayerBaseInitializedEvent(GameObject playerBase) {
        OnPlayerBaseInitialized(playerBase);
    }

    public static void FireEnemyBaseInitializedEvent(GameObject enemyBase) {
        OnEnemyBaseInitialized(enemyBase);
    }

    public static void FireWaypointsInitializedEvent(List<GameObject> waypoints) {
        OnWaypointsInitialized(waypoints);
    }

    public static void FireEnemyReachPlayerBaseEvent(GameObject enemy) {
        OnEnemyReachPlayerBase(enemy);
    }

    public static void FireBeforeEnemyDestroyedEvent(GameObject enemy) {
        OnBeforeEnemyDestroyed(enemy);
    }

    public static void FirePlayerBaseTakeDamageEvent(PlayerBase playerBase) {
        OnPlayerBaseTakeDamage(playerBase);
    }

    public static void FirePlayerBaseDestroyedEvent(PlayerBase playerBase) {
        OnPlayerBaseDestroyed(playerBase);
    }

    public static void FireBuildingStateToggledEvent(bool isEnabled) {
        OnBuildingStateToggled(isEnabled);
    }
}