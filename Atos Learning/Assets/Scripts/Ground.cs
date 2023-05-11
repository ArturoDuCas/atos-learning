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

    CompositeCollider2D compositeCollider2D;
    PlayerMovement player; 

    bool didGenerateGround = false; 

    private void Awake() {
        player = GameObject.Find("Player").GetComponent<PlayerMovement>();
        compositeCollider2D = GetComponent<CompositeCollider2D>(); 
        groundHeight = compositeCollider2D.bounds.max.y + 1.5f;  
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
        
    }

    private void FixedUpdate() {
        Vector2 pos = transform.position;
        pos.x -= player.velocity.x * Time.fixedDeltaTime;

    

        groundRight = compositeCollider2D.bounds.max.x;

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
        CompositeCollider2D goCollider = go.GetComponent<CompositeCollider2D>();

        Vector2 pos; 
        pos.x = screenRight + 5; 
        pos.y = transform.position.y;
        go.transform.position = pos;
    }
}
