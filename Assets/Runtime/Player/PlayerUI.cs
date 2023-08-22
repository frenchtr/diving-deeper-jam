using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OTStudios.DDJ.Runtime {

    public class PlayerUI : PlayerComponent {

        [SerializeField] private Transform healthUnitsParent;
        [SerializeField] private GameObject healthUnitPrefab;

        private void Start() { 
            Health.healthUpate += UpdateHealthUI;
        }

        private void UpdateHealthUI(int health) {

            if (healthUnitsParent.childCount == health) return;

            int diff = health - healthUnitsParent.childCount,
                dir  = (int)Mathf.Sign(diff),
                count = Mathf.Abs(diff);

            if (dir > 0)
                for (int i = 0; i < count; i++)
                    Instantiate(healthUnitPrefab, healthUnitsParent);
            else
                for (int i = 0; i < count; i++)
                    Destroy(healthUnitsParent.GetChild(i).gameObject);
        }
    }
}
