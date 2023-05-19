using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    Animator playerAnimator; 

    public float screenBottom;
    public string sceneName;  

    public int coins = 0;

    void Start() { 
        playerAnimator = GetComponent<Animator>();
        screenBottom = Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y;
    }

    void Update() {
        Vector2 pos = transform.position; 


        if(pos.y < screenBottom - 10f) {
            SceneManager.LoadScene(sceneName);
            return; 
        }


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
        }

        // Enemy interaction 
        Vector2 enemyOrigin = new Vector2(pos.x, pos.y);
        RaycastHit2D enemyHitX = Physics2D.Raycast(enemyOrigin, Vector2.right, velocity.x * Time.fixedDeltaTime);
        if (enemyHitX.collider != null) {
            Enemy enemy = enemyHitX.collider.GetComponent<Enemy>();
            if (enemy != null) {
                hitEnemy(enemyHitX); 
            }
        }

        RaycastHit2D enemyHitY = Physics2D.Raycast(enemyOrigin, Vector2.up, velocity.y * Time.fixedDeltaTime);
        if (enemyHitY.collider != null) {
            Enemy enemy = enemyHitY.collider.GetComponent<Enemy>();
            if (enemy != null) {
                hitEnemy(enemyHitY);
            }
        }

        //Coin interaction
        Vector2 coinOrigin = new Vector2(pos.x, pos.y);
        RaycastHit2D coinHitX = Physics2D.Raycast(coinOrigin, Vector2.right, velocity.x * Time.fixedDeltaTime);
        if (coinHitX.collider != null) {
            Coin coin = coinHitX.collider.GetComponent<Coin>();
            if (coin != null) {
                collectCoin(coinHitX); 
            }
        }
        
        RaycastHit2D coinHitY = Physics2D.Raycast(coinOrigin, Vector2.up, velocity.y * Time.fixedDeltaTime);
        if (coinHitY.collider != null) {
            Coin coin = coinHitY.collider.GetComponent<Coin>();
            if (coin != null) {
                collectCoin(coinHitY); 
            }
        }

        transform.position = pos; 
    }

    void hitEnemy(RaycastHit2D hit) {
        Debug.Log("Hit enemy");
        CapsuleCollider2D enemyCollider = hit.collider.GetComponent<CapsuleCollider2D>();
        Animator enemyAnimator = hit.collider.GetComponent<Animator>();
        Vector3 enemyScale = hit.collider.transform.localScale;

        enemyScale.x *= -1;
        hit.collider.transform.localScale = enemyScale;
        enemyAnimator.SetTrigger("Attack");
        Destroy(enemyCollider);
        velocity.x *= 0.7f;
        
        playerAnimator.SetTrigger("Hurt");
        float hurtTime = 0.5f;
        StartCoroutine(ReturnToIdle(hurtTime)); // Iniciar una corrutina para regresar al estado de Idle después del tiempo especificado
    }

    private IEnumerator ReturnToIdle(float time)
    {
        // Esperar el tiempo especificado
        yield return new WaitForSeconds(time);
        playerAnimator.SetTrigger("Running");
        playerAnimator.ResetTrigger("Hurt");
    }

    void collectCoin(RaycastHit2D hit) {
        Debug.Log("Moneda");
    }
}
