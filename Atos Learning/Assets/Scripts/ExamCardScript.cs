using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExamCardScript : MonoBehaviour
{
    public string examName; 
    public string examDescription;
    public string examImage; 

    private GameObject examTitle; 
    private GameObject examDescriptionText;
    private GameObject examImageObject;

    void Awake() {
        examTitle = transform.Find("ExamTitle").gameObject;
        examDescriptionText = transform.Find("ExamDescription").gameObject;
        examImageObject = transform.Find("ExamImage").gameObject;
    }

    public void updateData(string _examTitle, string _examDescription, string _examImage) {
        examTitle.GetComponent<TMPro.TextMeshProUGUI>().text = _examTitle;
        examDescriptionText.GetComponent<TMPro.TextMeshProUGUI>().text = _examDescription;
        examImageObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(_examImage);

    }
}
