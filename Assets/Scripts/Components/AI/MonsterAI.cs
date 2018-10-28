using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Components.AI {

    [RequireComponent(typeof(MovableComponent))]
    public class MonsterAI : MonoBehaviour {
        private List<GameObject> _waypoints       = new List<GameObject>(1);
        private int              _currentWaypoint = 0;
        private IEnumerator      _moveToTargetAction;

        private MovableComponent _movableComponent;

        private void Start() {
            _movableComponent = GetComponent<MovableComponent>();

            MoveToNextWaypoint();
        }

        private void MoveToNextWaypoint() {
            if (++_currentWaypoint >= _waypoints.Count) {
                // Last waypoint reached
                Events.FireEnemyReachPlayerBaseEvent(gameObject);
                Events.FireBeforeEnemyDestroyedEvent(gameObject);
                Destroy(gameObject);
                return;
            }

            _moveToTargetAction =
                Actions.MoveToPosition(_movableComponent, _waypoints[_currentWaypoint].transform.position, MoveToNextWaypoint);
            StartCoroutine(_moveToTargetAction);
        }

        public void SetWaypoints(List<GameObject> waypoints) {
            Debug.Assert(waypoints.Count > 0);
            _waypoints = waypoints;
        }
    }

}