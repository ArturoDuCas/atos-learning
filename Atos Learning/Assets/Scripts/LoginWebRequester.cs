using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking; 
using SimpleJSON; 
using UnityEngine.UI;
using TMPro;


public class LoginWebRequester : MonoBehaviour
{
    public TMP_InputField usernameInputField;
    public TMP_InputField passwordInputField;
    public GameObject loadingPrefab; 

    private GameObject loadingObject; 

    private readonly string baseURL = "";

    void Start()
    {
        usernameInputField.text = "";
        passwordInputField.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnLoginButtonClicked() {
        if (loadingObject == null) {
            loadingObject = Instantiate(loadingPrefab);
        }
        StartCoroutine(LoginRequest());
    }

    IEnumerator LoginRequest() { // PENDIENTE MIN 8:30 https://www.youtube.com/watch?v=GIxu8kA9EBU&ab_channel=TurboMakesGames
        string username = usernameInputField.text;
        string password = passwordInputField.text;

        string url = baseURL + "/login";

        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError) {
            Debug.LogError(request.error);
            yield break; 
        }

        JSONNode response = JSON.Parse(request.downloadHandler.text);
        


        Destroy(loadingObject);
        loadingObject = null;
    }
}
