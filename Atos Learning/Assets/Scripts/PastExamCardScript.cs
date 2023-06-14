using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking; 
using UnityEngine.UI;
using TMPro;

public class PastExamCardScript : MonoBehaviour
{
    private string examName; 
    private string examDescription;
    private string examImage; 
    private string examSubject;
    private string teacherName;
    private string dueDate;
    private int questionCount;
    private int examId; 
    private string examScore;
    private string examRealizationDate;
    
    [SerializeField]
    private GameObject examTitle; 
    [SerializeField]
    private GameObject examGrade;
    [SerializeField]
    private GameObject realizationDate;
    [SerializeField]
    private GameObject examImageObject;






    public void setProps(string _examName, string _examDescription, string _examImage, string _examSubject, string _teacherName, string _dueDate, int _questionCount, int _examId, string _examScore, string _realizationDate) {
        examName = _examName;
        examDescription = _examDescription;
        examImage = _examImage;
        examSubject = _examSubject;
        teacherName = _teacherName;
        dueDate = _dueDate;
        questionCount = _questionCount;
        examId = _examId;
        examScore = _examScore;
        examRealizationDate = _realizationDate;
    }

    public void updateCardData() {
        examTitle.GetComponent<TMPro.TextMeshProUGUI>().text = examName;
        examGrade.GetComponent<TMPro.TextMeshProUGUI>().text = examScore;
        realizationDate.GetComponent<TMPro.TextMeshProUGUI>().text = examRealizationDate;
        
        StartCoroutine(GetPastExamImage(examImage));
    }

    public void showCard() {
        gameObject.SetActive(true);
    }

    public void hideCard() {
        gameObject.SetActive(false);
    }

    IEnumerator GetPastExamImage(string examImage) {
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
