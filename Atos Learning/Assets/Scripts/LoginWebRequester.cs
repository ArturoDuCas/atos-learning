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

    
    private GameObject loadingPanel; 
    private GameObject errorPanel; 

    void Awake() {
        loadingPanel = GameObject.Find("LoadingPanel");
        errorPanel = GameObject.Find("ErrorPanel");

    }

    void Start()
    {
        usernameInputField.text = "";
        passwordInputField.text = "";
        loadingPanel.SetActive(false);
        errorPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnLoginButtonClicked() {
        errorPanel.SetActive(false);
        loadingPanel.SetActive(true);
        StartCoroutine(LoginRequest());
    }

    IEnumerator LoginRequest() {
        string username = usernameInputField.text;
        string password = passwordInputField.text;

        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);

        string url = "https://atoslearningapi.azurewebsites.net/api/Auth";

        using(UnityWebRequest request = UnityWebRequest.Post(url, form)) {
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
                    Store.username = response["username"];

                    // Cargar escena de juego
                    UnityEngine.SceneManagement.SceneManager.LoadScene("HomeScene");
                }



            }
        }
    }

    public void OnInputTextValueChanged() {
        if (errorPanel.activeSelf)
            errorPanel.SetActive(false);
    }
}
