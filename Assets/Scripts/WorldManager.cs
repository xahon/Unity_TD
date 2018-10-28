using System.Collections.Generic;
using Core;
using Helpers;
using UnityEngine;

public class WorldManager : Singleton<WorldManager> {
    [SerializeField] private GameObject _enemyBase;
    [SerializeField] private GameObject _playerBase;
    [SerializeField] private GameObject _ground;
    [SerializeField] private GameObject _road;
    [SerializeField] private GameObject _waypoint;

    private GameObject _rootObject;
    private Transform  _rootObjectTransform;

    private PerfectMaze      _perfectMaze;
    private List<GameObject> _waypoints;

    private void Start() {
        _perfectMaze         = new PerfectMaze(17);
        _rootObject          = new GameObject("WorldObjects");
        _rootObjectTransform = _rootObject.transform;

        Vector3 baseHeightOffset = new Vector3(0, 1, 0);

        for (int y = 0; y < _perfectMaze.Size; y++) {
            for (int x = 0; x < _perfectMaze.Size; x++) {
                Vector3              worldPosition = new Vector3(x, 0, y);
                Vector2Int           position2D    = new Vector2Int(x, y);
                PerfectMaze.CellType type          = _perfectMaze.Map[position2D];

                switch (type) {
                    case PerfectMaze.CellType.EnemyBase:
                        _rootObjectTransform = _rootObject.transform;
                        GameObject enemyBase = Instantiate(_enemyBase, _rootObjectTransform /*worldPosition + baseHeightOffset*/);
                        enemyBase.name               = "EnemyBase";
                        enemyBase.transform.position = worldPosition + baseHeightOffset;
                        Events.FireEnemyBaseInitializedEvent(enemyBase);

                        break;
                    case PerfectMaze.CellType.PlayerBase:
                        GameObject playerBase = Instantiate(_playerBase, _rootObjectTransform /*worldPosition + baseHeightOffset*/);
                        playerBase.name               = "PlayerBase";
                        playerBase.transform.position = worldPosition + baseHeightOffset;
                        Events.FirePlayerBaseInitializedEvent(playerBase);

                        break;
                    default:
                        if (type == PerfectMaze.CellType.Road) {
                            GameObject road = Instantiate(_road, _rootObjectTransform);
                            road.name               = "Road";
                            road.transform.position = worldPosition;
                        }
                        else if (type != PerfectMaze.CellType.Road) {
                            GameObject ground = Instantiate(_ground, _rootObjectTransform);
                            ground.name               = "Ground";
                            ground.transform.position = worldPosition;
                        }

                        break;
                }
            }
        }

        _waypoints = new List<GameObject>(_perfectMaze.VisitedCells.Count);

        int checkPointIndex = 0;
        foreach (Vector2Int visitedCell in _perfectMaze.VisitedCells) {
            Vector3 position = new Vector3(visitedCell.x, baseHeightOffset.y, visitedCell.y);

            GameObject waypoint = Instantiate(_waypoint, _rootObjectTransform);
            waypoint.name               = $"Waypoint {++checkPointIndex}";
            waypoint.transform.position = position;

            _waypoints.Add(waypoint);
        }

        Events.FireWaypointsInitializedEvent(_waypoints);
    }
}