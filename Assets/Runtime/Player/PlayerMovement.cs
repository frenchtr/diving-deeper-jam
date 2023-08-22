using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OTStudios.DDJ.Runtime {

    public class PlayerMovement : PlayerComponent {

        [Header("Movement")]
        [SerializeField] private float
            horizontalMovementSpeed;
        [SerializeField] private float
            bounceHeight,
            gravity;

        [Header("Effects")]
        [SerializeField] private float
            maxMovementTilt;
        [SerializeField] private float
            movementTiltSpeed,
            effectsTweenSpeed,
            minSpeedDronePitch,
            maxSpeedDronePitch,
            minAnimatorSpeed,
            maxAnimatorSpeed;

        [SerializeField] private Transform
            sprite;
        [SerializeField] private SoundEffect
            drillDrone;

        private float
            tiltAmount, tiltVel,
            effectsPercent;

        private void Start() {
            drillDrone.Init(gameObject);
            drillDrone.Play();
        }

        private void Update() {

            Vector2
                input = Input.Movement.Vector,
                vel = Rigidbody.velocity;

            tiltAmount = Mathf.SmoothDampAngle(tiltAmount, input.x * maxMovementTilt, ref tiltVel, movementTiltSpeed);
            sprite.eulerAngles = Vector3.forward * tiltAmount;

            effectsPercent = Mathf.MoveTowards(effectsPercent, Mathf.Abs(input.x), effectsTweenSpeed * Time.deltaTime);

            drillDrone.source.pitch = Mathf.LerpUnclamped(minSpeedDronePitch, maxSpeedDronePitch, effectsPercent);
            Animator.speed = Mathf.LerpUnclamped(minAnimatorSpeed, maxAnimatorSpeed, effectsPercent);

            vel.x = horizontalMovementSpeed * input.x;
            vel.y -= gravity * Time.deltaTime;

            Rigidbody.velocity = vel;
        }

        private void OnCollisionEnter2D(Collision2D collision) {

            if (collision.gameObject.layer == GameInfo.GroundLayer
                && collision.GetContact(0).normal == Vector2.up) {

                Vector3 vel = Rigidbody.velocity;
                vel.y = Mathf.Sqrt(bounceHeight * gravity * 2);
                Rigidbody.velocity =vel;
            }
        }
    }
}
