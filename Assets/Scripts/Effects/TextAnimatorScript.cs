using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextAnimatorScript : MonoBehaviour
{
    public string text;
    private TMP_Text _text;
    private string _displayText = "";
    
    void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
        _text.SetText(_displayText);
        
        InvokeRepeating("SetText", 0, 0.01f);
    }

    void SetText()
    {
        if (_displayText.Length < text.Length)
        {
            _displayText += text[_displayText.Length];
            _text.SetText(_displayText);
        }
    }
}
