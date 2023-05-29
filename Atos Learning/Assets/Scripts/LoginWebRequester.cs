using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking; 
using SimpleJSON; 
using UnityEngine.UI;
using TMPro;
using EasyTransition; 



public class LoginWebRequester : MonoBehaviour
{
    public TMP_InputField usernameInputField;
    public TMP_InputField passwordInputField;

    
    private GameObject loadingPanel; 
    private GameObject errorPanel; 
    private GameObject badInputPanel; 

    
    public TransitionSettings transition;
    public float loadDelay; 

    void Awake() {
        loadingPanel = GameObject.Find("LoadingPanel");
        errorPanel = GameObject.Find("ErrorPanel");
        badInputPanel = GameObject.Find("BadInputPanel");

    }

    void Start()
    {
        usernameInputField.text = "";
        passwordInputField.text = "";
        loadingPanel.SetActive(false);
        errorPanel.SetActive(false);
        badInputPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnLoginButtonClicked() {
        errorPanel.SetActive(false);

        if (usernameInputField.text == "" || passwordInputField.text == "") {
            badInputPanel.SetActive(true);
            return; 
        }

        loadingPanel.SetActive(true);
        StartCoroutine(LoginRequest());
    }

    IEnumerator LoginRequest() {
        string username = usernameInputField.text;
        string password = passwordInputField.text;

        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);

        string url = "https://atoslearningapi.azurewebsites.net/Auth";

        using(UnityWebRequest request = UnityWebRequest.Post(url, form)) {
            request.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded");
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError) { // Si se genera un error de conexion.
                Debug.LogError(request.error);
                yield break; 
            } else {
                loadingPanel.SetActive(false);
                if (request.result == UnityWebRequest.Result.ProtocolError) // Si el usuario o la contraseña son incorrectos.
                {
                    errorPanel.SetActive(true);
                    errorPanel.GetComponent<PanelController>().DesactivarPanel();
                    passwordInputField.text = "";
                    Debug.Log(request.error);
                    yield break;
                } else { // Usuario y contraseña correctos
                    JSONNode response = JSON.Parse(request.downloadHandler.text);
                    Store.user_id = response["id"];
                    Store.user_name = response["name"];
                    Store.user_surname = response["surname"];
                    Store.user_email = response["email"];
                    Store.user_nickname = response["nickname"];
                    Store.user_characterId = response["characterId"];
                    Store.user_image = response["image"];
                    Store.user_totalScore = response["totalScore"];
                    Store.isTeacher = response["isTeacher"];
                    Debug.Log(response["name"]);
                    TransitionManager.Instance().Transition("HomeScene", transition, loadDelay); // Carga la siguiente escena

                }



            }
        }
    }

    public void OnInputTextValueChanged() {
        if (errorPanel.activeSelf)
            errorPanel.SetActive(false);
        if (badInputPanel.activeSelf)
            badInputPanel.SetActive(false);
    }
}
