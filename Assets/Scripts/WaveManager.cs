using System.Collections;
using Helpers;
using UnityEngine;

public class WaveManager : Singleton<WaveManager> {

    private enum State {
        Building,
        Wave
    }


    [SerializeField] private float BuildingTimeout = 1.0f;
    [SerializeField] private int   MonsterCount    = 1;
    [SerializeField] private float MonsterTimeout  = 1.0f;

    private int       _wave             = 0;
    private int       _monstersNowCount = 0;
    private State     _state            = State.Building;
    private Coroutine _currentCoroutine = null;

    private void Start() {
        Events.OnWaveFinished         += OnWaveFinishedEvent;
        Events.OnBeforeEnemyDestroyed += BeforeEnemyDestroyed;
    }

    private void BeforeEnemyDestroyed(GameObject obj) {
        --_monstersNowCount;

        if (_monstersNowCount <= 0) {
            Events.FireWaveFinishEvent();
            _monstersNowCount = 0;
        }
    }

    private void OnWaveFinishedEvent() {
        Debug.Log("Wave state finished. It's time to build");

        _state            = State.Building;
        _currentCoroutine = null;
    }

    private void Update() {
        if (_state == State.Building && _currentCoroutine == null) {
            _currentCoroutine = StartCoroutine(StartBuildingTimeout());
        }
        else if (_state == State.Wave && _currentCoroutine == null) {
            _currentCoroutine = StartCoroutine(StartWave());
        }
    }

    private IEnumerator StartBuildingTimeout() {
        Debug.Log($"Building state. Timeout {BuildingTimeout}s");
        yield return new WaitForSeconds(BuildingTimeout);

        Debug.Log("Building state finished. Starting wave state");

        _state            = State.Wave;
        _currentCoroutine = null;
    }

    private IEnumerator StartWave() {
        ++_wave;
        Debug.Log($"Wave state. Wave #{_wave}");

        for (int i = 0; i < MonsterCount; i++) {
            yield return new WaitForSeconds(MonsterTimeout);
            Debug.Log($"<color=red>Monster #{i}</color> spawn requested");
            Events.FireMonsterSpawnRequestEvent(_wave);
            ++_monstersNowCount;
        }

        Debug.Log("All monsters spawned. Waiting for finishing wave state");
    }
}