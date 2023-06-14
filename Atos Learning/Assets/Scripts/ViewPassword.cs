using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ViewPassword : MonoBehaviour
{
    public TMP_InputField passwordInputField;
    private Toggle visibilityToggle;

    void Awake()
    {
        visibilityToggle = GetComponent<Toggle>();
    }
    void Start()
    {
        visibilityToggle.onValueChanged.AddListener(OnToggleValueChanged);
    }

    void OnToggleValueChanged(bool value)
    {
        if (value)
        {
            passwordInputField.contentType = TMP_InputField.ContentType.Password;
        }
        else
        {
            passwordInputField.contentType = TMP_InputField.ContentType.Standard;
        }
        passwordInputField.ForceLabelUpdate();
    }
}
