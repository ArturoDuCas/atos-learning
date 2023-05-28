using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelController : MonoBehaviour
{
    public float tiempoActivo = 10f; 
    
    public void DesactivarPanel() {
        StartCoroutine(Desactivar());
    }

    IEnumerator Desactivar() {
        yield return new WaitForSeconds(tiempoActivo);
        gameObject.SetActive(false);
    }
}
