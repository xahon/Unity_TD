using UnityEngine;

namespace Components.AI {

    [RequireComponent(typeof(IHealthComponent))]
    public class PlayerBase : MonoBehaviour {
        private IHealthComponent _healthComponent;

        private void Start() {
            _healthComponent              =  GetComponent<HealthComponent>();
            _healthComponent.OnDead       += OnDead;
            Events.OnEnemyReachPlayerBase += OnEnemyReachBase;
        }

        private void OnEnemyReachBase(GameObject obj) {
            _healthComponent.TakeDamage(obj.GetComponent<AttackComponent>());
            Events.FirePlayerBaseTakeDamageEvent(this);
        }

        private void OnDead(IHealthComponent healthComponent) {
            Debug.Log("<color=red>You lose</color>");
            Events.FirePlayerBaseDestroyedEvent(this);
        }
    }

}