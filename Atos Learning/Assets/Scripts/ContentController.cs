using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class ContentController : MonoBehaviour
{
    public GameObject examCardPrefab;

    void Awake() {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void generateExamList() {
        foreach(JSONNode exam in Store.exams) {
            string examName = exam["title"]; 
            string examDescription = exam["description"]; 
            string examImage = exam["imageUrl"];
            string examSubject = exam["subjectName"]; 
            string teacherName = exam["teacherName"];
            string dueDate = exam["dueDate"];
            int questionCount = exam["questionsCount"]; 
            int examId = exam["id"];

            GameObject examCard = Instantiate(examCardPrefab, transform);
            examCard.GetComponent<ExamCardScript>().setProps(examName, examDescription, examImage, examSubject, teacherName, dueDate, questionCount, examId);
            examCard.GetComponent<ExamCardScript>().updateCardData();
        }
    }

    
}
