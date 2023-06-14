using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomePageMenuController : MonoBehaviour
{
    [SerializeField]
    private GameObject menuPanel; 

    private void OnMouseDown() {
        if (menuPanel.activeSelf) {
            menuPanel.SetActive(false);
        } else {
            menuPanel.SetActive(true);
        }

    }

    void Start()
    {
        menuPanel.SetActive(false);    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
