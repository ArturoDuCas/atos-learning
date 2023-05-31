using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class HomeSceneWebRequest : MonoBehaviour
{
    private TextMeshProUGUI hiUserText;

    void Awake()
    {
        hiUserText = GameObject.Find("HiUser").GetComponent<TextMeshProUGUI>();
        hiUserText.text = "Hi, " + Store.user_nickname;
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
