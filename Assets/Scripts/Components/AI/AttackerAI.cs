using UnityEngine;

namespace Components.AI {

    [RequireComponent(typeof(IAttackComponent))]
    public class AttackerAI : MonoBehaviour {
        private IAttackComponent _attackComponent;

        private void Start() {
            _attackComponent = GetComponent<IAttackComponent>();
        }

        private void Update() { }
    }

}