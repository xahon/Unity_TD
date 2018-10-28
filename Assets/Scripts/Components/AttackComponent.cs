using System;
using Helpers.Attributes;
using UnityEngine;

namespace Components {

    [Flags]
    public enum AttackableId {
        Group0  = 1 << 0,
        Group1  = 1 << 1,
        Group2  = 1 << 2,
        Group3  = 1 << 3,
        Group4  = 1 << 4,
        Group5  = 1 << 5,
        Group6  = 1 << 6,
        Group7  = 1 << 7,
        Group8  = 1 << 8,
        Group9  = 1 << 9,
        Group10 = 1 << 10,
        Group11 = 1 << 11,
        Group12 = 1 << 12,
        Group13 = 1 << 13
    }

    public interface IAttackComponent {
        float Damage { get; set; }
        float Range  { get; set; }

        AttackableId Id        { get; set; }
        AttackableId CanAttack { get; set; }
    }

    public class AttackComponent : MonoBehaviour, IAttackComponent {
        [SerializeField]            private float        _damage    = 10f;
        [SerializeField]            private float        _range     = 0f;
        [SerializeField]            private AttackableId _id        = AttackableId.Group0;
        [SerializeField, EnumFlags] private AttackableId _canAttack = AttackableId.Group0 | AttackableId.Group1 | AttackableId.Group2;

        public float Damage {
            get { return _damage; }
            set { _damage = value; }
        }

        public float Range {
            get { return _range; }
            set { _range = value; }
        }

        public AttackableId Id {
            get { return _id; }
            set { _id = value; }
        }

        public AttackableId CanAttack {
            get { return _canAttack; }
            set { _canAttack = value; }
        }
    }

}