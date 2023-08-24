using System;
using OTStudios.DDJ.Runtime.Runtime.Bricks;
using UnityEngine;
using UnityEngine.Serialization;

namespace OTStudios.DDJ.Runtime.Runtime.CollapsingCeiling
{
    public class CollapsingCeiling : MonoBehaviour
    {
        [SerializeField]
        private BrickRegistry brickRegistry;
        [SerializeField]
        private float baseCollapseSpeed = 0.1f;
        [SerializeField]
        private float collapseSpeedIncrement = 0.1f;

        public float BaseCollapseSpeed
        {
            get => this.baseCollapseSpeed;
            set => this.baseCollapseSpeed = value;
        }

        public float CollapseSpeedIncrement
        {
            get => this.collapseSpeedIncrement;
            set => this.collapseSpeedIncrement = value;
        }

        private void OnEnable()
        {
            this.brickRegistry.Registered += OnBrickRegistered;
            this.brickRegistry.Deregistered += OnBrickDeregistered;
        }

        private void OnDisable()
        {
            this.brickRegistry.Registered -= OnBrickRegistered;
            this.brickRegistry.Deregistered -= OnBrickDeregistered;
        }

        private void Update()
        {
            this.transform.position += (Vector3)Vector2.down * BaseCollapseSpeed * Time.deltaTime;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var playerHealth = other.GetComponent<PlayerHealth>();

            if (playerHealth == null)
            {
                return;
            }

            playerHealth.Die();
        }
        
        private void OnBrickRegistered(Brick brick)
        {
            var destructible = brick.GetComponent<Destructible>();
            
            if (destructible == null)
            {
                return;
            }
            
            destructible.Destroyed += OnDestructibleDestroyed;
        }
        
        private void OnBrickDeregistered(Brick brick)
        {
            var destructible = brick.GetComponent<Destructible>();
            
            if (destructible == null)
            {
                return;
            }
            
            destructible.Destroyed -= OnDestructibleDestroyed;
        }
        
        private void OnDestructibleDestroyed()
        {
            this.BaseCollapseSpeed += this.CollapseSpeedIncrement;
        }
    }
}
