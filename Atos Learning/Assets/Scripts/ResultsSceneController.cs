using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 
using System;

public class ResultsSceneController : MonoBehaviour
{
    [SerializeField]
    private GameObject pointsTextObject;
    private double points; 
    // Start is called before the first frame update
    
    void Awake() {
        points = (float)Store.player_correctAnswers / Store.actualExam_questionCount * 100;
        points = Math.Round(points, 1);
        // pointsTextObject.GetComponent<TMPro.TextMeshProUGUI>().text = (Store.player_correctAnswers / Store.actualExam_questionCount * 100).ToString + "%";
        pointsTextObject.GetComponent<TMPro.TextMeshProUGUI>().text = points.ToString() + "%";
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
