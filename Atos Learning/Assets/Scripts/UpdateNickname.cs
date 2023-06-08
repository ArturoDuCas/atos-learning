using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro; 

public class UpdateNickname : MonoBehaviour
{
    [SerializeField]
    private GameObject nicknameInput;
    [SerializeField]
    private GameObject nicknameInputPlaceholder;


    private string nickname; 
    
    public void OnNickNameSubmission() {
        nickname = nicknameInput.GetComponent<TMP_InputField>().text;
        if(nickname.Length > 0) {
            StartCoroutine(UpdateNicknameRequest());
        }
    }

    IEnumerator UpdateNicknameRequest() {
        string url = "https://atoslearningapi.azurewebsites.net/Users/updateNickname";

        WWWForm form = new WWWForm();
        form.AddField("userId", Store.user_id);
        form.AddField("nickname", nickname);



        byte[] bodyRaw = form.data;
        using(UnityWebRequest updateNicknameRequest = UnityWebRequest.Put(url, bodyRaw)) {
            updateNicknameRequest.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded");
            yield return updateNicknameRequest.SendWebRequest();

            if(updateNicknameRequest.result == UnityWebRequest.Result.ConnectionError) {
                Debug.LogError(updateNicknameRequest.error);
                yield break;
            } else {
                if(updateNicknameRequest.result == UnityWebRequest.Result.ProtocolError) {
                    Debug.LogError(updateNicknameRequest.error);
                    yield break;
                } else {
                    Debug.Log(updateNicknameRequest.downloadHandler.text);
                    Store.user_nickname = nickname;
                    nicknameInputPlaceholder.GetComponent<TextMeshProUGUI>().text = nickname;
                }
            }
        }
    }
}
