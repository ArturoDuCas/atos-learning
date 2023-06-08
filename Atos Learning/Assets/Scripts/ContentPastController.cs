using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class ContentPastController : MonoBehaviour
{
    public GameObject pastExamCardPrefab;
    public GameObject noExamsPanel; 

    void Awake() {
        noExamsPanel.SetActive(false);
    }
    
    public void generatePastExamList() {
        if(Store.submittedExams.Count == 0) {
            noExamsPanel.SetActive(true);
            return;
        }

        foreach(JSONNode exam in Store.submittedExams) {
            string examName = exam["title"]; 
            string examDescription = exam["description"]; 
            string examImage = exam["imageUrl"];
            string examSubject = exam["subjectName"]; 
            string teacherName = exam["teacherName"];
            string dueDate = exam["dueDate"];
            int questionCount = exam["questionsCount"]; 
            int examId = exam["id"];
            // TODO quitar el hardcode
            string examScore = "60.5";
            string realizationDate = "2021-05-20T00:00:00";


            GameObject examCard = Instantiate(pastExamCardPrefab, transform);
            examCard.GetComponent<PastExamCardScript>().setProps(examName, examDescription, examImage, examSubject, teacherName, dueDate, questionCount, examId, examScore, realizationDate);
            examCard.GetComponent<PastExamCardScript>().updateCardData();
        }
    }
}
