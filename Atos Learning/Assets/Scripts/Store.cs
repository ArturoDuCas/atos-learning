using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON; 

public static class Store
{
    public static int user_id;
    public static string user_name;
    public static string user_surname;
    public static string user_email; 
    public static string user_nickname; 
    public static int user_characterId; 
    public static string user_image; 
    public static int user_totalScore;
    public static bool isTeacher; 

    public static JSONArray exams;
    public static JSONArray submittedExams;

    public static string actualExam_subject = "Matematicas"; 
    public static string actualExam_dueDate = "2021-05-20T00:00:00";
    public static string actualExam_title = "Examen de prueba"; 
    public static string actualExam_description = "Descripcion de prueba Descripcion de prueba Descripcion de prueba Descripcion de prueba Descripcion de prueba"; 
    public static string actualExam_image = "https://concepto.de/wp-content/uploads/2019/12/algebra-e1577465340342.jpg";
    public static int actualExam_questionCount = 10; 
    public static int actualExam_id = 10;

    public static int player_coinCount;


    public static JSONArray examQuestions; 

    public static string player_endTime;
    public static int player_actualQuestion = 0; 
    public static int player_correctAnswers = 0;
    public static List<bool> player_answersHistory = new List<bool>();
    public static List<int> player_answersID = new List<int>();

}


