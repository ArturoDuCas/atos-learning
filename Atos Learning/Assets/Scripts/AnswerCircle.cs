using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AnswerCircle : MonoBehaviour
{
    public bool isCorrect; 
    // Start is called before the first frame update

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setData(int num, bool _isCorrect) {
        isCorrect = _isCorrect; 
        transform.GetChild(0).gameObject.GetComponent<TextMeshPro>().text = num.ToString();
        if (!isCorrect) {
            transform.GetComponent<SpriteRenderer>().color = Color.red;
        }
    }
}
