using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using OTStudios.DDJ.Runtime.Runtime.Bricks;
using UnityEngine.UI;

namespace OTStudios.DDJ.Runtime {

    public class PlayerUI : PlayerComponent {

        [Header("Health")]
        [SerializeField] private Transform healthUnitsParent;

        [Header("Bricks")]
        [SerializeField] private GameObject healthUnitPrefab;
        [SerializeField] private TextMeshProUGUI bricksTextMesh;

        [Header("Dash Charge")]
        [SerializeField] private Slider dashChargeSlider;
        [SerializeField] private TextMeshProUGUI dashChargeTextMesh;
        [SerializeField] private Image dashChargeBackground;
        [SerializeField] private float dashChargeFlashDur;
        [SerializeField] private Color chargedColor, flashColor, chargingColor;

        private int bricksDestroyed;
        private bool chargeFlashed;

        private void OnEnable() {
            Health.healthUpate += UpdateHealthUI;
            Movement.dashChargeUpdate += UpdateDashCharge;
            BrickRegistry.Deregistered += BrickDeregistered;
            BrickRegistry.Registered += BrickRegistered;
        }

        private void OnDisable() {
            Health.healthUpate -= UpdateHealthUI;
            Movement.dashChargeUpdate -= UpdateDashCharge;
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

        private void UpdateDashCharge(float remaining, float total) {
            dashChargeSlider.value = remaining / total;
            dashChargeTextMesh.text = $"{Mathf.Max(remaining, 0):0.00}";

            if (remaining < 0 && !chargeFlashed) {
                chargeFlashed = true;

                StartCoroutine(ChargeFlash());
                IEnumerator ChargeFlash() {

                    dashChargeBackground.color = flashColor;
                    yield return new WaitForSeconds(dashChargeFlashDur);
                    dashChargeBackground.color = chargedColor;
                }
            }

            else if (remaining > 0) {
                chargeFlashed = false;
                dashChargeBackground.color = chargingColor;
            }
        }

        private void BrickDestroyed() {
            bricksDestroyed++;
            bricksTextMesh.text = $"{bricksDestroyed:0000}";
                //string.Format("{0:D4}", bricksDestroyed);
        }
    }
}
