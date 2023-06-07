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

    public void getExams(){
        StartCoroutine(getExamsRequest());
    }

    IEnumerator getExamsRequest() {
        loadingPanel.SetActive(true);
        // Obtener los examenes del usuario: 
        int userId = Store.user_id;
        string url = "https://atoslearningapi.azurewebsites.net/VideoGameExams/pending?userId=" + userId.ToString(); 

        using(UnityWebRequest examsRequest = UnityWebRequest.Get(url)) {
            yield return examsRequest.SendWebRequest(); 
            
            if (examsRequest.result == UnityWebRequest.Result.ConnectionError) { // Si se genera un error de conexion 
                Debug.LogError(examsRequest.error);
                yield break; 
            } else {
                if (examsRequest.result == UnityWebRequest.Result.ProtocolError) { // Si hay un error en la peticion 
                    Debug.LogError(examsRequest.error);
                    yield break; 
                } else { // Se obtuvieron los datos correctamente
                    JSONNode examResponse = JSON.Parse(examsRequest.downloadHandler.text);
                    Store.exams = examResponse.AsArray; 
                }
            }
        }

        ChangeScene(); 
    }



}
