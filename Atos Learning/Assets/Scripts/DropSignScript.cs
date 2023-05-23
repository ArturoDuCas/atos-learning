using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropSignScript : MonoBehaviour
{
    public PlayerMovement player;

    private float screenRight;
    private float signFinalPosition; 

private void Awake() {
    screenRight = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)).x;
    signFinalPosition = screenRight - 6f;

}
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate() {
        Vector2 pos = transform.position; 
        pos.x -= 0.1f; 

        if(pos.x < signFinalPosition) {
            pos.x = signFinalPosition; 
        }

        transform.position = pos;
    }
}
