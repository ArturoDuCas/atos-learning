using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking; 
using SimpleJSON; 
using TMPro; 
using System;

public class ResultsSceneController : MonoBehaviour
{
    [SerializeField]
    private GameObject pointsTextObject;
    private double points; 
    
    [SerializeField]
    private GameObject loadingPanel;
    
    void Awake() {
        points = (float)Store.player_correctAnswers / Store.actualExam_questionCount * 100;
        points = Math.Round(points, 1);
        // pointsTextObject.GetComponent<TMPro.TextMeshProUGUI>().text = (Store.player_correctAnswers / Store.actualExam_questionCount * 100).ToString + "%";
        pointsTextObject.GetComponent<TMPro.TextMeshProUGUI>().text = points.ToString() + "%";
        loadingPanel.SetActive(false);
    }
    void Start()
    {
        Store.player_endTime = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
        StartCoroutine(SendResults()); 
    }

    IEnumerator SendResults() {
        WWWForm form = new WWWForm();
        form.AddField("userId", Store.user_id);
        form.AddField("examId", Store.actualExam_id);
        foreach(int answerId in Store.player_answersID) {
            form.AddField("answersIds", answerId);
        }
        // form.AddField("answersIds", string.Join(",", Store.player_answersID));
        form.AddField("score", points.ToString());
        form.AddField("endDateTime", Store.player_endTime);


        string url = "https://atoslearningapi.azurewebsites.net/api/Exams"; 
        using(UnityWebRequest request = UnityWebRequest.Post(url, form)) {
            request.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded");
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError) { // Si se genera un error de conexion.
                Debug.LogError(request.error);
                yield break; 
            } else {
                if (request.result == UnityWebRequest.Result.ProtocolError) // Si se genera un error en los datos enviados.
                {
                    Debug.Log(request.error);
                    yield break;
                } else {
                    Store.user_totalScore += (int)points;
                    Debug.Log("Results sent successfully");
                }
            }
        }

        
    }
}
