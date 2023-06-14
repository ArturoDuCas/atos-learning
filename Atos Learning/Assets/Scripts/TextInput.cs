using UnityEngine;
using UnityEngine.UI;

public class TextInput : MonoBehaviour
{
    public InputField inputField;

    public void OnSubmit()
    {
        string userInput = inputField.text;
        Debug.Log("Texto ingresado: " + userInput);
    }
}
