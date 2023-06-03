using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class Answer : MonoBehaviour
{
    private string answerText;
    private bool isCorrect;

    GameObject answerContainerObject; 
    GameObject answerTextObject;


    public void setData(string _answerText, bool _isCorrect) {
        answerContainerObject = transform.Find("AnswerContainer").gameObject;
        answerTextObject = answerContainerObject.transform.Find("AnswerText").gameObject;
        answerText = _answerText;
        isCorrect = _isCorrect;

        setAnswerText();
    }

    private void setAnswerText() {
        answerTextObject.GetComponent<TextMeshPro>().text = answerText;
    }
}
