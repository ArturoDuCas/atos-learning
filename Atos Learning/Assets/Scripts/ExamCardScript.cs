using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking; 
using UnityEngine.UI;
using TMPro;
using EasyTransition; 

public class ExamCardScript : MonoBehaviour
{
    public string examName; 
    public string examDescription;
    public string examImage; 

    private GameObject examTitle; 
    private GameObject examDescriptionText;
    private GameObject examImageObject;

    public TransitionSettings transition;
    public float loadDelay; 

    void Awake() {
        examTitle = transform.Find("ExamTitle").gameObject;
        examDescriptionText = transform.Find("ExamDescription").gameObject;
        examImageObject = transform.Find("ExamImage").gameObject;
    }

    private void OnMouseDown() {
        Debug.Log("Se hizo click"); 
        TransitionManager.Instance().Transition("ExamDetails", transition, loadDelay); // Carga la siguiente escena

    }

    public void updateData(string _examTitle, string _examDescription, string _examImage) {
        examTitle.GetComponent<TMPro.TextMeshProUGUI>().text = _examTitle;
        examDescriptionText.GetComponent<TMPro.TextMeshProUGUI>().text = _examDescription;
        
        StartCoroutine(GetExamImage(_examImage));

    }

    IEnumerator GetExamImage(string _examImage) {
        UnityWebRequest examSpriteRequest = UnityWebRequestTexture.GetTexture(_examImage); 

        yield return examSpriteRequest.SendWebRequest();

        if (examSpriteRequest.result == UnityWebRequest.Result.ProtocolError) {
            Debug.LogError(examSpriteRequest.error);
            yield break;
        }

        examImageObject.GetComponent<RawImage>().texture = ((DownloadHandlerTexture) examSpriteRequest.downloadHandler).texture;
        examImageObject.GetComponent<RawImage>().texture.filterMode = FilterMode.Point;
    }



    
}
