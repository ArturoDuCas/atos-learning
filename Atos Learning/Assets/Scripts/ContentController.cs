using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class ContentController : MonoBehaviour
{
    public GameObject examCardPrefab;

    void Awake() {
        generateExamList(); 
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void generateExamList() {
        foreach(JSONNode exam in Store.exams) {
            string examnName = exam["title"]; 
            string examDescription = exam["description"]; 
            string examImage = exam["image"]; 
            GameObject examCard = Instantiate(examCardPrefab, transform);
            examCard.GetComponent<ExamCardScript>().updateData(examnName, examDescription, examImage);
        }
    }
}
