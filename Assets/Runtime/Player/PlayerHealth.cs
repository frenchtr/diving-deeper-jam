using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OTStudios.DDJ.Runtime {

    public class PlayerHealth : PlayerComponent {

        [SerializeField] private int startHealth;

        private int health;
        public event System.Action<int> healthUpate;

        private void Start() {
            health = startHealth;
        }

        public void Heal(int amount) {
            health += amount;
            healthUpate.Invoke(health);
        }

        public void TakeDamage(int amount) {

            health -= amount;
            healthUpate.Invoke(health);

            if (health <= 0) Die();
        }

        public void Die() {

            Movement.enabled = false;
            Rigidbody.velocity = Vector2.zero;

            FindObjectOfType<GameOverMenu>().Show();
        }
    }
}
