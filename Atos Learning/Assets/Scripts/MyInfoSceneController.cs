using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using UnityEngine.UI;
using TMPro;

public class MyInfoSceneController : MonoBehaviour
{
    [SerializeField]
    private GameObject loadingPanel;
    [SerializeField]
    private GameObject userScore; 

    [SerializeField]
    private GameObject nicknameInputPlaceholder; 

    private GameObject contentGameObject;

    void Awake() {
        loadingPanel.SetActive(false);
        contentGameObject = GameObject.Find("Content");
        nicknameInputPlaceholder.GetComponent<TextMeshProUGUI>().text = Store.user_nickname;
        userScore.GetComponent<TMPro.TextMeshProUGUI>().text = Store.user_totalScore.ToString();
        StartCoroutine(GetPastExams()); 

    }

    IEnumerator GetPastExams() {
        loadingPanel.SetActive(true);
        int userId = Store.user_id; 
        string url = "https://atoslearningapi.azurewebsites.net/VideoGameExams/submitted?userId=" + userId.ToString();

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
                                Store.submittedExams = examResponse.AsArray; 
                            }
                        }
                    }

        ShowPastExams(); 
    }

    private void ShowPastExams() {
        contentGameObject.GetComponent<ContentPastController>().generatePastExamList(); 

        loadingPanel.SetActive(false);
    }
}
