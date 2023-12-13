using System;
using System.Collections.Generic;
using UnityEngine;

public class NewErganeDictionary : MonoBehaviour
{
    private static NewErganeDictionary _instance;
    public event Action<NewErganeLetterObj> OnUnlockLetter;

    private List<NewErganeLetterObj> _language = new();

    public static NewErganeDictionary Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<NewErganeDictionary>();

                if (_instance == null)
                {
                    var singletonObject = new GameObject("NewErganeDictionary");
                    _instance = singletonObject.AddComponent<NewErganeDictionary>();
                }
            }

            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this as NewErganeDictionary;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public List<NewErganeLetterObj> GetDictionary() => this._language;

    public void UnlockLetter(NewErganeLetterObj letter)
    {
        if (this.IsLetterExist(letter)) return;
        this._language.Add(letter);
        OnUnlockLetter?.Invoke(letter);
    }

    public bool IsLetterExist(NewErganeLetterObj letter) => this._language.FindIndex((l) => l.name == letter.letter) > 0;
}