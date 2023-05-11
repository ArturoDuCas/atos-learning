using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{

    public float groundHeight; 
    CompositeCollider2D compositeCollider2D;

    private void Awake() {
        compositeCollider2D = GetComponent<CompositeCollider2D>(); 
        groundHeight = compositeCollider2D.bounds.max.y + 1.5f;  
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
