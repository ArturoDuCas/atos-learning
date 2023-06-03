using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking; 
using UnityEngine.UI;
using TMPro;
using EasyTransition; 
using SimpleJSON; 



public class DetallesExamen : MonoBehaviour
{
    private GameObject titleText;
    private GameObject subjectText;
    private GameObject dueDateText;
    private GameObject descriptionText;
    private GameObject image;
    private GameObject questionCountText;
    private GameObject loadingPanel;


    [SerializeField]
    private TransitionSettings transition; 
    [SerializeField]
    private float loadDelay; 

    void Awake(){
        titleText = GameObject.Find("Title"); 
        subjectText = GameObject.Find("Subject");
        dueDateText = GameObject.Find("Date");
        descriptionText = GameObject.Find("Description");
        image = GameObject.Find("Image");
        questionCountText = GameObject.Find("Questions");
        loadingPanel = GameObject.Find("LoadingPanel");


        titleText.GetComponent<TMPro.TextMeshProUGUI>().text = Store.actualExam_title;
        subjectText.GetComponent<TMPro.TextMeshProUGUI>().text = Store.actualExam_subject;
        dueDateText.GetComponent<TMPro.TextMeshProUGUI>().text = "Hasta: " + Store.actualExam_dueDate.Replace("T", " - ");
        descriptionText.GetComponent<TMPro.TextMeshProUGUI>().text = Store.actualExam_description;
        questionCountText.GetComponent<TMPro.TextMeshProUGUI>().text = "Preguntas: " + Store.actualExam_questionCount.ToString();
        StartCoroutine(GetImage(Store.actualExam_image));
    }

    void Start() {
        loadingPanel.SetActive(false);
    }

    IEnumerator GetImage(string url) {
        UnityWebRequest examSpriteRequest = UnityWebRequestTexture.GetTexture(url); 

        yield return examSpriteRequest.SendWebRequest();

        if (examSpriteRequest.result == UnityWebRequest.Result.ProtocolError) {
            Debug.LogError(examSpriteRequest.error);
            yield break;
        }

        image.GetComponent<RawImage>().texture = ((DownloadHandlerTexture) examSpriteRequest.downloadHandler).texture;
        image.GetComponent<RawImage>().texture.filterMode = FilterMode.Point;
    }


    public void OnPlayButton() {
        StartCoroutine(GetExamQuestions(Store.actualExam_id)); 
        loadingPanel.SetActive(true);
    }

    IEnumerator GetExamQuestions(int examID) {
        string url = "https://atoslearningapi.azurewebsites.net/VideoGameExams/questions?examId=" + examID.ToString();

        using(UnityWebRequest questionsRequest = UnityWebRequest.Get(url)) {
            yield return questionsRequest.SendWebRequest(); 

            if (questionsRequest.result == UnityWebRequest.Result.ProtocolError) { // Si se genera un error de conexion
                Debug.LogError(questionsRequest.error);
                yield break;
            } else {
                if (questionsRequest.result == UnityWebRequest.Result.ProtocolError) { // Si hay un error en la peticion 
                    Debug.LogError(questionsRequest.error);
                    yield break;
                } else { // Se obtuvieron los datos correctamente
                JSONNode questions = JSON.Parse(questionsRequest.downloadHandler.text);
                Store.examQuestions = questions.AsArray;
                }
            }
        }
        TransitionManager.Instance().Transition("TestsScene", transition, loadDelay); 
    }
}
