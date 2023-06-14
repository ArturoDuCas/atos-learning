using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro; 
using SimpleJSON;

public class HomeSceneWebRequest : MonoBehaviour
{
    private TextMeshProUGUI hiUserText;
    [SerializeField]
    private GameObject loadingPanel;
    [SerializeField]
    private GameObject ExamContainer; 

    void Awake()
    {
        hiUserText = GameObject.Find("HiUser").GetComponent<TextMeshProUGUI>();
        hiUserText.text = "Hola, " + Store.user_nickname;
        StartCoroutine(getExamListRequest()); 
    } 
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    IEnumerator getExamListRequest()
    {
        loadingPanel.SetActive(true);
        Debug.Log("Empezar corutina"); 
        // Obtener los examenes del usuario: 
        int userId = Store.user_id;
        string url = "https://atoslearningapi.azurewebsites.net/VideoGameExams/pending?userId=" + userId.ToString(); 
        using(UnityWebRequest examsRequest = UnityWebRequest.Get(url))
        {
            Debug.Log("Peticion enviada"); 
            yield return examsRequest.SendWebRequest();
            Debug.Log("Peticion contestada"); 
                        
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
        loadingPanel.SetActive(false);
        ExamContainer.GetComponent<ContentController>().generateExamList();
    }
}
