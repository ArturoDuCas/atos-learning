using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using TMPro;

public class UIController : MonoBehaviour
{
    PlayerMovement playerMovement;

    private void Awake() {
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>(); 
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
