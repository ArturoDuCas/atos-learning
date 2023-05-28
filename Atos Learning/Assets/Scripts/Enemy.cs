using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private PlayerMovement player; 
    private float screenLeft;


    private void Awake() {
        player = GameObject.Find("Player").GetComponent<PlayerMovement>();
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
        if(pos.x < screenLeft - 3f) {
            Destroy(gameObject);
        }

        transform.position = pos;  
    }
}
