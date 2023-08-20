using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : PlayerComponent {

    [SerializeField] private float
        horizontalMovementSpeed,
        bounceHeight,
        gravity,
        maxMovementTilt,
        movementTiltSpeed;
    [SerializeField] private Transform
        sprite,
        groundDetectPosition;

    private float tiltAmount, tiltVel;

    private void Update() {

        Vector2
            input = Input.Movement.Vector,
            vel = Rigidbody.velocity;

        Vector2 toGroundDetectPosition = groundDetectPosition.position - transform.position;
        var onGround = Physics2D.Raycast(transform.position, toGroundDetectPosition, toGroundDetectPosition.magnitude, GameInfo.GroundMask);

        tiltAmount = Mathf.SmoothDampAngle(tiltAmount, input.x * maxMovementTilt, ref tiltVel, movementTiltSpeed);
        sprite.eulerAngles = Vector3.forward * tiltAmount;

        vel.x = horizontalMovementSpeed * input.x;

        if (onGround)
            vel.y = Mathf.Sqrt(bounceHeight * gravity * 2);
        else
            vel.y -= gravity * Time.deltaTime;

        Rigidbody.velocity = vel;
    }
}
