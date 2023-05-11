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

    BoxCollider2D boxCollider2D;
    PlayerMovement player; 

    bool didGenerateGround = false; 

    private void Awake() {
        player = GameObject.Find("Player").GetComponent<PlayerMovement>();
        boxCollider2D = GetComponent<BoxCollider2D>(); 
        groundHeight = boxCollider2D.bounds.max.y + 1.5f; 
        screenRight = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)).x;
        screenLeft = Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(groundRight); 
        Debug.Log(groundHeight);
    }

    private void FixedUpdate() {
        Vector2 pos = transform.position;
        pos.x -= player.velocity.x * Time.fixedDeltaTime;

    
        groundRight = boxCollider2D.bounds.max.x;

        if(groundRight < screenLeft) {
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
        pos.x = screenRight - 5; 
        pos.y = transform.position.y;
        go.transform.position = pos;
    }
}
