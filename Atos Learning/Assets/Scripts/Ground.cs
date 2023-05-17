using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class Ground : MonoBehaviour
{
    public float groundHeight; 

    public float groundRight; 
    public float screenRight; 
    public float screenLeft; 
    public float screenBottom; 

    BoxCollider2D boxCollider2D;
    PlayerMovement player; 

    bool didGenerateGround = false;

    public Enemy reaperTemplate;  

    private void Awake() {
        player = GameObject.Find("Player").GetComponent<PlayerMovement>();
        boxCollider2D = GetComponent<BoxCollider2D>(); 
        groundHeight = boxCollider2D.bounds.max.y + 1.5f;  
        screenRight = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)).x;
        screenBottom = Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y;
        screenLeft = Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate() {
        Vector2 pos = transform.position;
        pos.x -= player.velocity.x * Time.fixedDeltaTime; // Move the ground to the left based on the player's velocity

    
        groundRight = boxCollider2D.bounds.max.x;

        if(groundRight < screenLeft) { // If the ground is off the screen, destroy it
            Destroy(gameObject);
            return; 
        }

        if (!didGenerateGround) {
            if(groundRight < screenRight) {
                didGenerateGround = true;
                generateGround(); 
            }
        }

        transform.position = pos;
    }

    void generateGround() {
        GameObject go = Instantiate(gameObject);
        BoxCollider2D goCollider = go.GetComponent<BoxCollider2D>();
        Vector2 pos; 

        float h1 = player.jumpVelocity * player.maxJumpTime;  
        float t = player.jumpVelocity / -player.gravity;
        float h2 = player.jumpVelocity * t + (0.5f * (player.gravity * (t * t)));  // h = v0t + 1/2at^2
        float maxJumpHeight = h1 + h2; 
        float maxY = maxJumpHeight * 0.7f; // The 70% of the max jump height the player can perform
        maxY += groundHeight; // Add the actual ground Height
        float minY = screenBottom; // The minimum height the ground can be at 
        float actualY = Random.Range(minY, maxY); 
        
        pos.y = actualY;
        if (pos.y > 4.5f)
            pos.y = 4.5f;

        float t1 = t + player.maxJumpTime;
        float t2 = Mathf.Sqrt((2.0f * (maxY - actualY)) / -player.gravity); 
        float totalTime = t1 + t2; 
        float maxX = totalTime * player.velocity.x; 
        maxX *= 0.7f;
        maxX += groundRight;
        float minX = screenRight + 2;
        float actualX = Random.Range(minX, maxX);

        pos.x = actualX + goCollider.bounds.extents.x; 

        go.transform.position = pos;
        
        Ground goGround = go.GetComponent<Ground>();
        goGround.groundHeight = pos.y + goCollider.bounds.extents.y * 2 + 0.4f;

        int obstacleNum = Random.Range(0, 2); 
        for (int i = 0; i < obstacleNum; i++) {
            GameObject reaper = Instantiate(reaperTemplate.gameObject);
            float halfWidth = goCollider.bounds.extents.x * 0.9f; // 90% of the ground's width
            float goMinX = go.transform.position.x - halfWidth; 
            float goMaxX = go.transform.position.x + halfWidth;
            Debug.Log("goMinX: " + goMinX + " goMaxX: " + goMaxX);
            float x = Random.Range(goMinX, goMaxX);
            float y = goGround.groundHeight; 
            Vector2 reaperPos = new Vector2(x, y);
            reaper.transform.position = reaperPos;
        }
    }
}
