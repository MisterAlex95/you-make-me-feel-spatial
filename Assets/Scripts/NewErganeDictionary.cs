using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NewErganeDictionary : MonoBehaviour
{
    private static NewErganeDictionary _instance;
    [SerializeField] private TMP_Text letterCounter;
    [SerializeField] private GameObject codex;

    public event Action<NewErganeLetterObj> OnUnlockLetter;

    private List<NewErganeLetterObj> _language = new();

    public static NewErganeDictionary Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindAnyObjectByType<NewErganeDictionary>();

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
        letterCounter.text = (this._language.Count + "/9");
    }

    public bool IsLetterExist(NewErganeLetterObj letter) => this._language.FindIndex((l) => l.letter == letter.letter) >= 0;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            codex.SetActive(!codex.activeSelf);
        }
    }
}