using UnityEngine;

namespace Components {

    [RequireComponent(typeof(IAttackComponent))]
    public class BuildingComponent : MonoBehaviour {
        [SerializeField] private readonly int _cost;

        public int Cost => _cost;
    }

}