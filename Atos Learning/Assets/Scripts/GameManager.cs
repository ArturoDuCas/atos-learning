using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public PlayerMovement player; 
    public float limitDistance = 50f; 

    public bool finishedCollectionTime = false; 

    public GameObject TiratePanel; 

    private float screenRight;
    private float panelFinalPosition; 
    private float screenBottom;


    private void Awake() {
        TiratePanel.SetActive(false);
        screenRight = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)).x;
        panelFinalPosition = screenRight - 4f;

        screenBottom = Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.distance > limitDistance) {
            finishedCollectionTime = true;
            TiratePanel.SetActive(true);
        }
    }

    private void FixedUpdate() {
        if (finishedCollectionTime) {
            Vector2 panelPos = TiratePanel.transform.position;
            panelPos.x -= 0.2f; 

            if(panelPos.x < panelFinalPosition) {
                panelPos.x = panelFinalPosition;
            }

            TiratePanel.transform.position = panelPos;


            if (player.transform.position.y < screenBottom - 2f) {
                SceneManager.LoadScene("QuestionScene");
            }
        }
    }
}
