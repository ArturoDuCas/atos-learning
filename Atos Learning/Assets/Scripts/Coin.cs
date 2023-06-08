using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Coin : MonoBehaviour
{

    public static event Action OnCoinCollected;

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Increase the coin count
            player.IncreaseCoinCount();

            // Trigger the coin collected event
            OnCoinCollected?.Invoke();

            // Destroy the coin object
            Destroy(gameObject);
        }
    }
}
