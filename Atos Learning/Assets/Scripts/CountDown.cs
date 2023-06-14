using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class CountDown : MonoBehaviour
{
    public float countdownTime = 15f; // Tiempo inicial del contador
   
    public float currentTime;

    public int seconds; 

    private void Start()
    {
        
    }

    private void Update()
    {
        currentTime -= Time.deltaTime; // Restar el tiempo transcurrido desde el último frame

        if (currentTime <= 0)
        {
            currentTime = 0;
            // Realizar alguna acción al llegar a cero
        }
        
        // Actualizar el texto del contador
        seconds = Mathf.FloorToInt(currentTime % 60);

        // Si el contador llega a cero, realizar alguna acción o detener el tiempo
        if (currentTime <= 0)
        {
            currentTime = 0;
            // Realizar alguna acción al llegar a cero
        }
    }

    public void setTime(float time) {
        if (Store.player_coinCount >= 15){
            time += 8f;
        }
        else if (Store.player_coinCount >= 10) {
            time += 5f;
        }
        else if (Store.player_coinCount >= 5) {
            time += 3f;
        }
        countdownTime = time;
        currentTime = countdownTime;
    }

}

