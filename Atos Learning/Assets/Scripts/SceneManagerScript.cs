using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    public string Scene;

    public void ChangeScene()
    {
        SceneManager.LoadScene(Scene);
        
    }
}
