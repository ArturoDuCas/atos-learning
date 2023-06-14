using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundHomeScene : MonoBehaviour
{
    [SerializeField]
    private GameObject menuPanel; 

    void Awake() {
         
    }

    private void OnMouseDown() {
        if (menuPanel.activeSelf) {
            menuPanel.SetActive(false);
        }

    }
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        
    }
}
