using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderDistance : MonoBehaviour
{
    PlayerMovement player; 
    GameController gameController;
    Slider slider; 

    private void Awake() {
        player = GameObject.Find("Player").GetComponent<PlayerMovement>();
        gameController = GameObject.Find("GameControllerObject").GetComponent<GameController>();
        slider = GetComponent<Slider>();
        slider.maxValue = gameController.limitDistance;
    }

    void Update()
    {
        slider.value = player.distance;       
    }
}
