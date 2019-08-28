using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TTSExample : MonoBehaviour
{
    [SerializeField] TMP_InputField inputField;

    public void OnButtonClick()
    {
        TTSPluginWrapper.instance.TTS(inputField.text);
    }
}
