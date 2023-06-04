using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswersContainerController : MonoBehaviour
{
    [SerializeField]
    private GameObject answerCircle; 
    private List<bool> answers = new List<bool>() {true, false, false, false, true, true};

    private float componentLeftSide; 
    private float componentRightSide;

    void Awake() {
        componentLeftSide = transform.position.x - transform.localScale.x / 2;
        componentRightSide = transform.position.x + transform.localScale.x / 2;

        for(int i = 0; i < answers.Count; i++) {
            GameObject answerObject = Instantiate(answerCircle);
            Vector2 pos = new Vector2(); 
            pos.y = transform.position.y;
            pos.x = componentLeftSide + (componentRightSide - componentLeftSide) / (answers.Count + 1) * (i + 1);
            answerObject.GetComponent<AnswerCircle>().setData((i + 1), answers[i]);
            answerObject.transform.position = pos;
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
