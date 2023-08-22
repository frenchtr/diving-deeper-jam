using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OTStudios.DDJ.Runtime {

    public class PlayerHealth : PlayerComponent {

        [SerializeField] private int startHealth;

        private int health;

        private void Start() {
            health = startHealth;
        }

        public void TakeDamage(int amount) {

            health -= amount;

            if (health <= 0) Die();
        }

        private void Die() {
            SceneManager.ReloadScene();
        }
    }
}
