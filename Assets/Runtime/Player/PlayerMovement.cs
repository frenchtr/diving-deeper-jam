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
            gravity,
            maxFallSpeed;

        [Header("Dash")]
        [SerializeField] private float
            dashSpeed;
        [SerializeField] private float
            dashDuration,
            dashCooldown;

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
            drillDrone,
            rockPlink;

        private float
            tiltAmount, tiltVel,
            effectsPercent,
            dashCooldownRemaining;

        public event System.Action<float, float> dashChargeUpdate;

        private enum State {
            regular,
            dashing,
        }

        private State state, prevState;
        private float stateDur;
        private void ChangeState(State newState) {
            prevState = state;
            state = newState;
            stateDur = 0;
        }

        private void Start() {

            drillDrone.Init(gameObject, AudioReferences);
            rockPlink.Init(gameObject, AudioReferences);

            drillDrone.Play();
        }

        private void Update() {

            Vector2
                input = Input.Movement.Vector,
                vel = Rigidbody.velocity;

            dashCooldownRemaining -= Time.deltaTime;
            dashChargeUpdate?.Invoke(dashCooldownRemaining, dashCooldown);

            bool dashDown = Input.Dash.Pressed,
                 canDash = dashDown && dashCooldownRemaining < 0,
                 doneDashing = stateDur > dashDuration;

            stateDur += Time.deltaTime;
            switch (state) {

                case State.regular:

                    vel.x = horizontalMovementSpeed * input.x;
                    vel.y -= gravity * Time.deltaTime;
                    vel.y = Mathf.Max(vel.y, -maxFallSpeed);

                    if (canDash) ChangeState(State.dashing);
                    break;

                case State.dashing:

                    vel.y = -dashSpeed;

                    if (doneDashing) ChangeState(State.regular);
                    break;
            }

            if (stateDur == 0) {

                // so enter state can't be triggered twice
                stateDur = Mathf.Epsilon;

                // enter state
                switch (state) {

                    case State.dashing:
                        dashCooldownRemaining = dashCooldown;
                        break;
                }

                // exit state
                switch (prevState) { }
            }

            // effects
            tiltAmount = Mathf.SmoothDampAngle(tiltAmount, input.x * maxMovementTilt, ref tiltVel, movementTiltSpeed);
            sprite.eulerAngles = Vector3.forward * tiltAmount;

            effectsPercent = Mathf.MoveTowards(effectsPercent, Mathf.Abs(input.x), effectsTweenSpeed * Time.deltaTime);

            drillDrone.source.pitch = Mathf.LerpUnclamped(minSpeedDronePitch, maxSpeedDronePitch, effectsPercent);
            Animator.speed = Mathf.LerpUnclamped(minAnimatorSpeed, maxAnimatorSpeed, effectsPercent);

            // apply velocity
            Rigidbody.velocity = vel;
        }

        private void OnCollisionEnter2D(Collision2D collision) {

            if (collision.gameObject.layer == GameInfo.GroundLayer
                && collision.GetContact(0).normal == Vector2.up) {

                Vector3 vel = Rigidbody.velocity;
                vel.y = Mathf.Sqrt(bounceHeight * gravity * 2);
                Rigidbody.velocity = vel;

                rockPlink.Play();
            }
        }
    }
}
