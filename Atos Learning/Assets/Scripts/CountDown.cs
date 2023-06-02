using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountDown : MonoBehaviour
{
    public float countdownTime = 15f; // Tiempo inicial del contador
   
    public float currentTime;

    public int seconds; 

    private void Start()
    {
        currentTime = countdownTime;
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
}

