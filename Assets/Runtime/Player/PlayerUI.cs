using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using OTStudios.DDJ.Runtime.Runtime.Bricks;

namespace OTStudios.DDJ.Runtime {

    public class PlayerUI : PlayerComponent {

        [SerializeField] private Transform healthUnitsParent;
        [SerializeField] private GameObject healthUnitPrefab;
        [SerializeField] private TextMeshProUGUI bricksTextMesh;

        private int bricksDestroyed;

        private void OnEnable() {
            Health.healthUpate += UpdateHealthUI;
            BrickRegistry.Deregistered += BrickDeregistered;
            BrickRegistry.Registered += BrickRegistered;
        }

        private void OnDisable() {
            Health.healthUpate -= UpdateHealthUI;
            BrickRegistry.Deregistered -= BrickDeregistered;
            BrickRegistry.Registered -= BrickRegistered;
        }

        private void BrickRegistered(Brick brick) {
            if (brick.TryGetComponent(out Destructible destructible))
                destructible.Destroyed += BrickDestroyed;
        }

        private void BrickDeregistered(Brick brick) {
            if (brick.TryGetComponent(out Destructible destructible))
                destructible.Destroyed -= BrickDestroyed;
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

        private void BrickDestroyed() {
            bricksDestroyed++;
            bricksTextMesh.text = $"{bricksDestroyed:0000}";
                //string.Format("{0:D4}", bricksDestroyed);
        }
    }
}
