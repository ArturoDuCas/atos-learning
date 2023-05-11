using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float inferiorLimit;
    public float superiorLimit;
    public float depth = 1; 
    PlayerMovement playerMovement;
    
    private void Awake() 
    {
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }


    void Start()
    {
        
    }


    void FixedUpdate()
    {
        float realVelocity = playerMovement.velocity.x / depth;
        Vector2 pos = transform.position;
        pos.x -= realVelocity * Time.fixedDeltaTime;

        if(pos.x <= inferiorLimit) {
            pos.x = superiorLimit;
        }

        transform.position = pos;
    }
}
