using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking; 
using UnityEngine.UI;
using TMPro;
using EasyTransition; 



public class DetallesExamen : MonoBehaviour
{

    public string actualExam_subject;
    public string actualExam_dueDate;
    public string actualExam_title;
    public string actualExam_description;
    public string actualExam_image;
    public string actualExam_questionCount;

    public GameObject actualExam_subjectText;
    public GameObject actualExam_dueDateText;
    public GameObject actualExam_titleText;
    public GameObject actualExam_descriptionText;
    public GameObject actualExam_imageText;
    public GameObject actualExam_questionCountText;

    void Awake(){
        actualExam_subjectText = GameObject.Find("DETALLES EXAMEN").
    }







    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
