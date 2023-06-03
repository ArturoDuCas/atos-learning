using System; 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON; 
using TMPro; 

public class QuestionSceneController : MonoBehaviour
{
    private string question; 
    private JSONArray answers; 

    void Awake() {
        getQuestion(); 
        setData(); 
    }

    private void getQuestion() {
        System.Random random = new System.Random();
        int questionNum = random.Next(0, Store.examQuestions.Count); 
        JSONNode questionData = Store.examQuestions[questionNum];

        question = questionData["title"];
        answers = questionData["answers"].AsArray;

        // Remove question from examQuestions
        JSONArray updatedArray = new JSONArray();
        for (int i = 0; i < Store.examQuestions.Count; i++) {
            if (i != questionNum) {
                updatedArray.Add(Store.examQuestions[i]);
            }
        }
        Store.examQuestions = updatedArray;
        Debug.Log(Store.examQuestions); 
    }

    private void setData() {
        GameObject.Find("QuestionText").GetComponent<TMPro.TextMeshProUGUI>().text = question;

        // Randomize answer order
        List<int> answerOptions = new List<int>(); 
        for (int i = 0; i < 4; i++) {
            answerOptions.Add(i);
        }
        System.Random random = new System.Random();
        
        for (int i = 0; i < 4; i++) {
            int answerOptionIndex = random.Next(0, answerOptions.Count);
            int index = answerOptions[answerOptionIndex];
            answerOptions.RemoveAt(answerOptionIndex);
            
            GameObject.Find("A" + (index + 1).ToString()).GetComponent<Answer>().setData(answers[i]["text"], answers[i]["isCorrect"]);
        }
    }
}
