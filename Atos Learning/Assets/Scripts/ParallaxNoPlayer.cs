using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxNoPlayer : MonoBehaviour
{
    [SerializeField]
    private float inferiorLimit; 
    [SerializeField]
    private float superiorLimit;
    [SerializeField]
    private float speed = 30f; 
    [SerializeField]
    private float depth = 1f;

    void FixedUpdate()
    {
        float realVelocity = speed / depth;
        Vector2 pos = transform.position;
        pos.x -= realVelocity * Time.fixedDeltaTime;

        if(pos.x <= inferiorLimit) {
            pos.x = superiorLimit;
        }

        transform.position = pos;
    }

}
