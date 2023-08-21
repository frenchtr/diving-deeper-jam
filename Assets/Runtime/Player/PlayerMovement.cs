using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : PlayerComponent {

    [SerializeField] private float
        horizontalMovementSpeed,
        bounceHeight,
        gravity,
        maxMovementTilt,
        movementTiltSpeed,
        dronePitchChangeSpeed,
        maxSpeedDronePitch,
        minSpeedDronePitch;
    [SerializeField] private Transform
        sprite;
    [SerializeField] private SoundEffect
        drillDrone;

    private float
        tiltAmount, tiltVel,
        dronePitch;

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

        float dronePitchTarget = Mathf.LerpUnclamped(minSpeedDronePitch, maxSpeedDronePitch, Mathf.Abs(input.x));
        dronePitch = Mathf.MoveTowards(dronePitch, dronePitchTarget, dronePitchChangeSpeed * Time.deltaTime);
        drillDrone.source.pitch = dronePitch;

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
