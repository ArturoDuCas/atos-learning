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
    private int timeLimit; 


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

        if (questionData["timeLimit"] > 0) {
            timeLimit = questionData["timeLimit"];
        } else {
            timeLimit = 10;
        }

        // Remove question from examQuestions
        JSONArray updatedArray = new JSONArray();
        for (int i = 0; i < Store.examQuestions.Count; i++) {
            if (i != questionNum) {
                updatedArray.Add(Store.examQuestions[i]);
            }
        }
        Store.examQuestions = updatedArray;
    }

    private void setData() {
        GameObject.Find("QuestionText").GetComponent<TMPro.TextMeshProUGUI>().text = question;
        Store.player_actualQuestion += 1;
        GameObject.Find("QuestionNumberText").GetComponent<TMPro.TextMeshProUGUI>().text = "Pregunta " + Store.player_actualQuestion.ToString() + "/" + Store.actualExam_questionCount.ToString();
        
        GameObject.Find("CountDown").GetComponent<CountDown>().setTime(timeLimit);

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
            GameObject.Find("A" + (index + 1).ToString()).GetComponent<Answer>().setData(answers[i]["text"], answers[i]["isCorrect"], answers[i]["id"]);
        }

    }
}
