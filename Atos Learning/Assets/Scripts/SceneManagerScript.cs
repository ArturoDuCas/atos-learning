using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using SimpleJSON;
using EasyTransition; 

public class SceneManagerScript : MonoBehaviour
{
    public string scene;
    public TransitionSettings transition; 
    public float loadDelay; 

    [SerializeField]
    private GameObject loadingPanel;

    public void ChangeScene()
    {
        Debug.Log("Cambiando de escena"); 
        TransitionManager.Instance().Transition(scene, transition, loadDelay); // Carga la siguiente escena        
    }
    
}




