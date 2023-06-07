using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class Answer : MonoBehaviour
{
    private string answerText;
    public bool isCorrect;
    public int id; 

    GameObject answerContainerObject; 
    GameObject answerTextObject;


    public void setData(string _answerText, bool _isCorrect, string _id) {
        answerContainerObject = transform.Find("AnswerContainer").gameObject;
        answerTextObject = answerContainerObject.transform.Find("AnswerText").gameObject;
        answerText = _answerText;
        isCorrect = _isCorrect;
        id = int.Parse(_id); 

        setAnswerText();
    }

    private void setAnswerText() {
        answerTextObject.GetComponent<TextMeshPro>().text = answerText;
    }
}
