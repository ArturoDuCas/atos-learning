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
    public float maxMaxJumpTime = 0.3f;
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

            Vector2 rayOrigin = new Vector2(pos.x + 0.7f, pos.y - 1.5f) ; 
            Vector2 rayDirection = Vector2.up;
            float rayDistance = velocity.y * Time.fixedDeltaTime;
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, rayDirection, rayDistance);
            if(hit.collider != null) {
                Ground ground = hit.collider.GetComponent<Ground>();
                if (ground != null) {
                    groundHeight = ground.groundHeight;
                    velocity.y = 0.0f;
                    pos.y = groundHeight;
                    isGrounded = true;
                }
            }
            Debug.DrawRay(rayOrigin, rayDirection * rayDistance, Color.red);
        }

        distance += velocity.x * Time.fixedDeltaTime;

        if (isGrounded) {
            float velocityRatio = velocity.x / maxXVelocity;
            acceleration = maxAcceleration * (1 - velocityRatio);
            maxJumpTime = maxMaxJumpTime * velocityRatio; // For more velocity, more jump time. 

            velocity.x += acceleration * Time.fixedDeltaTime;
            if (velocity.x > maxXVelocity) {
                velocity.x = maxXVelocity;
            }

            Vector2 rayOrigin = new Vector2(pos.x - 0.7f, pos.y - 1.5f) ; 
            Vector2 rayDirection = Vector2.up;
            float rayDistance = velocity.y * Time.fixedDeltaTime;
            
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, rayDirection, rayDistance);
            if(hit.collider == null) {
                isGrounded = false; 
                }
            Debug.DrawRay(rayOrigin, rayDirection * rayDistance, Color.yellow);
        }

        transform.position = pos; 
    }
}
