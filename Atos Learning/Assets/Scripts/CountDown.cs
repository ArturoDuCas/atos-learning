using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class CountDown : MonoBehaviour
{
    public float countdownTime = 15f; // Tiempo inicial del contador
   
    public float currentTime;

    public int seconds; 

    private int coinCount;

    private void Start()
    {
        Coin.OnCoinCollected += HandleCoinCollected;
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
        countdownTime = time;
        currentTime = countdownTime;
    }

    private void HandleCoinCollected()
    {
        coinCount++;

        // Check if the coin count reaches 5
        if (coinCount == 5)
        {
            IncreaseTime(10f);
            coinCount = 0;
        }
    }

    private void IncreaseTime(float amount)
    {
        currentTime += amount;
    }
}

