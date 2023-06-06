using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking; 
using UnityEngine.UI;
using TMPro;
using EasyTransition; 

public class ExamCardScript : MonoBehaviour
{
    private string examName; 
    private string examDescription;
    private string examImage; 
    private string examSubject;
    private string teacherName;
    private string dueDate;
    private int questionCount;
    private int examId; 
    

    private GameObject examTitle; 
    private GameObject examDescriptionText;
    private GameObject examImageObject;

    public TransitionSettings transition;
    public float loadDelay; 

    public bool isDragging = false; 

    void Awake() {
        examTitle = transform.Find("ExamTitle").gameObject;
        examDescriptionText = transform.Find("ExamDescription").gameObject;
        examImageObject = transform.Find("ExamImage").gameObject;
    }

    private void OnMouseUp() {
        if (isDragging) {
            return;
        }
        Store.actualExam_subject = examSubject; 
        Store.actualExam_dueDate = dueDate;
        Store.actualExam_title = examName;
        Store.actualExam_description = examDescription;
        Store.actualExam_image = examImage;
        Store.actualExam_questionCount = questionCount;
        Store.actualExam_id = examId;

        TransitionManager.Instance().Transition("ExamDetails", transition, loadDelay); // Carga la siguiente escena

    }


    public void setProps(string _examName, string _examDescription, string _examImage, string _examSubject, string _teacherName, string _dueDate, int _questionCount, int _examId) {
        examName = _examName;
        examDescription = _examDescription;
        examImage = _examImage;
        examSubject = _examSubject;
        teacherName = _teacherName;
        dueDate = _dueDate;
        questionCount = _questionCount;
        examId = _examId;
    }

    public void updateCardData() {
        examTitle.GetComponent<TMPro.TextMeshProUGUI>().text = examName;
        examDescriptionText.GetComponent<TMPro.TextMeshProUGUI>().text = examDescription;
        
        StartCoroutine(GetExamImage(examImage));
    }

    public void showCard() {
        gameObject.SetActive(true);
    }

    public void hideCard() {
        gameObject.SetActive(false);
    }

    IEnumerator GetExamImage(string examImage) {
        UnityWebRequest examSpriteRequest = UnityWebRequestTexture.GetTexture(examImage); 

        yield return examSpriteRequest.SendWebRequest();

        if (examSpriteRequest.result == UnityWebRequest.Result.ProtocolError) {
            Debug.LogError(examSpriteRequest.error);
            yield break;
        }

        examImageObject.GetComponent<RawImage>().texture = ((DownloadHandlerTexture) examSpriteRequest.downloadHandler).texture;
        examImageObject.GetComponent<RawImage>().texture.filterMode = FilterMode.Point;
    }



    
}
