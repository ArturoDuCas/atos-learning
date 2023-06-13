using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float gravity = -200.0f;
    public Vector2 velocity;
    [SerializeField]
    private float maxAcceleration = 10; 
    [SerializeField]
    private float acceleration = 10; 
    public float distance = 0; 
    public float jumpVelocity = 20; 
    public float maxXVelocity = 100;
    public float groundHeight; 
    public bool isGrounded = false; 

    private bool isHoldingJump = false;
    public float maxJumpTime = 0.3f;
    private float maxMaxJumpTime = 0.3f;
    private float jumpTime = 0.0f;

    private float jumpGroundThreshold = 1;

    Animator playerAnimator; 

    public float screenBottom;
    private float screenTop; 
    public string sceneName;  

    public int coins = 0;
    public TextMeshProUGUI coinCountText;

    public bool isDead = false; 
    private bool onPosition = false;
    GameController gameController;
    private GameObject claw; 
    private bool isDropping = false;

    public LayerMask groundLayerMask; 
    public LayerMask obstacleLayerMask;

    private int coinCount;

    public GameObject coinSoundSource; 
    public GameObject hurtSoundSource;
    public GameObject jumpSoundSource;
    public GameObject runningSoundSource;

    public void IncreaseCoinCount()
    {
        coinCount++;
    }




    void Start() { 
        playerAnimator = GetComponent<Animator>();
        playerAnimator.ResetTrigger("Dead");
        screenBottom = Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y;
        screenTop = Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y;
        groundHeight = screenBottom;
        gameController = GameObject.Find("GameControllerObject").GetComponent<GameController>();
        claw = GameObject.Find("Claw");
    }

    void Update() {
        Vector2 pos = transform.position; 

        float groundDistance = Mathf.Abs(pos.y - groundHeight);
        if (isGrounded || groundDistance <= jumpGroundThreshold) {
            if (Input.GetKeyDown(KeyCode.Space)) { // Is holding a jump 
                runningSoundSource.GetComponent<AudioSource>().Stop(); 
                jumpSoundSource.GetComponent<AudioSource>().Play();
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

        if (isDead) { // Si el usuario se murio 
            if (onPosition && !isDropping) { // Cuando llegue a la posicion donde se movera (altura)
                distance += maxXVelocity * Time.fixedDeltaTime * 2;
                if (gameController.finishedCollectionTime) {
                    StartCoroutine(dropPlayer(pos)); 
                    isDropping = true;
                }
            }
            return; 
        }

        if (pos.y < screenBottom - 5f) {
            velocity.x = 0; 
            isDead = true;
            playerAnimator.SetTrigger("Dead");
            float initialPos = screenBottom - 5f;
            float finalPos = screenTop - 1f; 
            StartCoroutine(deadFunctionality(initialPos, finalPos));
            return; 
        }

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
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, rayDirection, rayDistance, groundLayerMask);
            if(hit.collider != null) {
                Ground ground = hit.collider.GetComponent<Ground>();
                if (ground != null) {
                    if(pos.y >= ground.groundHeight) {
                        runningSoundSource.GetComponent<AudioSource>().Play();
                        groundHeight = ground.groundHeight;
                        velocity.y = 0.0f;
                        pos.y = groundHeight;
                        isGrounded = true;
                    }
                }
            }

            // Detect when hit wall
            Vector2 wallOrigin = new Vector2(pos.x, pos.y);
            RaycastHit2D wallHit = Physics2D.Raycast(wallOrigin, Vector2.right, velocity.x * Time.fixedDeltaTime, groundLayerMask);
            if (wallHit.collider != null) {
                Ground ground = wallHit.collider.GetComponent<Ground>();
                if (ground != null) {
                    if (pos.y < ground.groundHeight) {
                        hurtSoundSource.GetComponent<AudioSource>().Play();
                        velocity.x = 0.0f;
                    }
                }
            }
            Debug.DrawRay(wallOrigin, Vector2.right * velocity.x * Time.fixedDeltaTime, Color.red);
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
                runningSoundSource.GetComponent<AudioSource>().Stop();
                isGrounded = false; 
                }
        }

        // Enemy interaction 
        Vector2 enemyOrigin = new Vector2(pos.x, pos.y);
        RaycastHit2D enemyHitX = Physics2D.Raycast(enemyOrigin, Vector2.right, velocity.x * Time.fixedDeltaTime, obstacleLayerMask);
        if (enemyHitX.collider != null) {
            Enemy enemy = enemyHitX.collider.GetComponent<Enemy>();
            if (enemy != null) {
                hitEnemy(enemyHitX); 
            }
        }

        RaycastHit2D enemyHitY = Physics2D.Raycast(enemyOrigin, Vector2.up, velocity.y * Time.fixedDeltaTime, obstacleLayerMask);
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
        hurtSoundSource.GetComponent<AudioSource>().Play();
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
        coins -= 5; 
        if (coins < 0) {
            coins = 0; 
        }
        coinCountText.text =  coins.ToString();
    }

    private IEnumerator ReturnToIdle(float time)
    {
        // Esperar el tiempo especificado
        yield return new WaitForSeconds(time);
        playerAnimator.SetTrigger("Running");
        playerAnimator.ResetTrigger("Hurt");
    }

    private IEnumerator dropPlayer(Vector2 pos) {
        yield return new WaitForSeconds(1.25f);
        velocity.x = 0f;
        while (pos.y > screenBottom - 3f) {
            velocity.y += gravity * Time.fixedDeltaTime;
            pos.y += velocity.y * Time.fixedDeltaTime / 25f;
            transform.position = pos;
            yield return new WaitForSeconds(0.01f);
        }
    }


    private IEnumerator deadFunctionality(float initialPos, float finalPos) {
        // Bajar la garra
        Vector2 clawPos = claw.transform.position;
        clawPos.x = transform.position.x - 0.5f;
        while (clawPos.y - 20f > initialPos) {
            clawPos.y -= 0.25f;
            claw.transform.position = clawPos;
            yield return new WaitForSeconds(0.01f);
        }

        // Subir la garra y el usuario 
        Vector2 pos = transform.position;
        while (pos.y < finalPos) {
        pos.y += 0.25f;
        clawPos.y += 0.25f;
        claw.transform.position = clawPos;
        transform.position = pos;
        yield return new WaitForSeconds(0.02f);
        }

        yield return new WaitForSeconds(0.5f);
        velocity.x = maxXVelocity * 2;
        onPosition = true; 
        
    }

   


    void collectCoin(RaycastHit2D hit) {
        coinSoundSource.GetComponent<AudioSource>().Play();
        Destroy(hit.collider.gameObject);
        coins += 1;
        coinCountText.text = coins.ToString();
    }

    
}
