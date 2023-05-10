using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float gravity = -200.0f;
    public Vector2 velocity;
    public float maxAcceleration = 10; 
    public float acceleration = 10; 
    public float distance = 0; 
    public float jumpVelocity = 20; 
    public float maxXVelocity = 100;
    public float groundHeight = -2.5f; 
    public bool isGrounded = false; 

    public bool isHoldingJump = false;
    public float maxJumpTime = 0.3f;
    public float jumpTime = 0.0f;

    public float jumpGroundThreshold = 1;

    void Start() { 

    }

    void Update() {
        Vector2 pos = transform.position; 
        float groundDistance = Mathf.Abs(pos.y - groundHeight);
        if (isGrounded || groundDistance <= jumpGroundThreshold) {
            if (Input.GetKeyDown(KeyCode.Space)) { // Is holding a jump 
                isGrounded = false;
                velocity.y = jumpVelocity;
                isHoldingJump = true;
                jumpTime = 0.0f;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space)) {
            isHoldingJump = false;
        }
    }

    private void FixedUpdate() {
        Vector2 pos = transform.position;

        if (!isGrounded) {
            if (isHoldingJump) {
                jumpTime += Time.fixedDeltaTime;
                if (jumpTime >= maxJumpTime) {
                    isHoldingJump = false;
                }
            }

            pos.y += velocity.y * Time.fixedDeltaTime;
            if (!isHoldingJump) {
                velocity.y += gravity * Time.fixedDeltaTime;
            }

            if (pos.y <= groundHeight) { // Hit the ground
                pos.y = groundHeight;
                isGrounded = true;
            }
        }

        distance += velocity.x * Time.fixedDeltaTime;

        if (isGrounded) {
            float velocityRatio = velocity.x / maxXVelocity;
            acceleration = maxAcceleration * (1 - velocityRatio);

            velocity.x += acceleration * Time.fixedDeltaTime;
            if (velocity.x > maxXVelocity) {
                velocity.x = maxXVelocity;
            }
        }

        transform.position = pos; 
    }
}
