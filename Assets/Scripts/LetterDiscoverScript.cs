using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class LetterDiscoverScript : MonoBehaviour
{
    public string letter = "L";
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        var dic = NewErganeDictionary.Instance.GetDictionary();
        foreach (var newErganeLetterObj in dic)
        {
            if (newErganeLetterObj.letter == letter)
            {
                GetComponent<UnityEngine.UI.Image>().enabled = false;
                GetComponentInChildren<TextMeshProUGUI>().SetText(letter);
            }
        }
    }
}
