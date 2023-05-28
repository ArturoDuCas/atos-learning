using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private PlayerMovement player; 
    public float limitDistance = 50f; 

    [SerializeField]
    private DropSignScript dropSignScript;
    private bool generatedDropSign = false; 

    public bool finishedCollectionTime = false; 

    private float screenBottom;
    private float screenRight; 

    private bool fixedPlayer = false;

    

    private void Awake() {
        screenBottom = Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y;
        screenRight = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)).x;
    
    }
    void Start()
    {
        player.gravity = -25f; 
    }

    void Update()
    {
        if (player.distance > limitDistance) {
            finishedCollectionTime = true;
        }

        if (!fixedPlayer) { // Al inicio del juego, corregir la gravedad del jugador
            if (player.isGrounded) {
                player.gravity = -250f;
                fixedPlayer = true;
            }
        }


        
    }

    private void FixedUpdate() {
        if (finishedCollectionTime) {
            if (!generatedDropSign) {
                GameObject dropSign = Instantiate(dropSignScript.gameObject); 
                dropSign.transform.position = new Vector2(screenRight + 5f, 0f);
                generatedDropSign = true;
            }

            if (player.transform.position.y < screenBottom - 2f) {
                SceneManager.LoadScene("QuestionScene");
            }
        }
    }
}
