using System;
using Components.Gui;
using UnityEngine;

namespace Components {

    public interface IHealthComponent {
        WorldSpaceLabel Label { get; set; }

        float MaxHealth     { get; set; }
        float CurrentHealth { get; set; }

        event Action<IHealthComponent> OnDead;
        event Action<float>            OnTakeDamage;

        void TakeDamage(float            amount);
        void TakeDamage(IAttackComponent attackComponent);
    }

    public sealed class HealthComponent : MonoBehaviour, IHealthComponent {
        [SerializeField] private float           _maxHealth     = 100f;
        [SerializeField] private float           _currentHealth = 100f;
        [SerializeField] private WorldSpaceLabel _label;

        public WorldSpaceLabel Label {
            get { return _label; }
            set { _label = value; }
        }

        public float MaxHealth {
            get { return _maxHealth; }
            set { _maxHealth = value; }
        }

        public float CurrentHealth {
            get { return _currentHealth; }
            set { _currentHealth = value; }
        }

        public event Action<IHealthComponent> OnDead       = delegate { };
        public event Action<float>            OnTakeDamage = delegate { };

        private void Start() {
            UpdateLabel();
        }

        [Obsolete]
        public void TakeDamage(float amount) {
            CurrentHealth -= amount;
            AfterTakingDamage(amount);

            if (CurrentHealth <= 10e-5) {
                FireDeadEvent();
            }
        }

        private void AfterTakingDamage(float amount) {
            OnTakeDamage(amount);
            UpdateLabel();
        }

        private void UpdateLabel() {
            if (Label != null) {
                Label.Text = CurrentHealth.ToString();
            }
        }

        public void TakeDamage(IAttackComponent attackComponent) {
            if (attackComponent == null) {
                return;
            }

#pragma warning disable 612
            TakeDamage(attackComponent.Damage);
#pragma warning restore 612
        }

        private void FireDeadEvent() {
            OnDead(this);
        }
    }

}