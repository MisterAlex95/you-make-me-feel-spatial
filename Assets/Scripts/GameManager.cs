using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<AudioClip> audioClips;
    [SerializeField] private GameObject[] spheres;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private TMP_Text letterByPlanetText;

    [SerializeField] private int currentPlanet = 0;
    [SerializeField] private List<int> letterByPlanet;

    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindAnyObjectByType<GameManager>();

                if (_instance == null)
                {
                    var singletonObject = new GameObject("GameManager");
                    _instance = singletonObject.AddComponent<GameManager>();
                }
            }

            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this as GameManager;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        this.spheres = GameObject.FindGameObjectsWithTag("Planet")
            .OrderBy(sphere => sphere.name)
            .ToArray();

        this._audioSource.clip = audioClips[currentPlanet];
        this._audioSource.Play();
        this.letterByPlanet = new List<int>(new int[this.spheres.Length]);

        NewErganeDictionary.Instance.OnUnlockLetter += LootALetter;
    }

    private void LootALetter(NewErganeLetterObj obj)
    {
        this.letterByPlanet[this.currentPlanet]++;
        letterByPlanetText.text = this.letterByPlanet[this.currentPlanet].ToString() + "/3";
    }

    public void SwitchToNextPlanet()
    {
        if (currentPlanet == (spheres.Length - 1))
        {
            currentPlanet = 0;
        }
        else
        {
            currentPlanet++;
        }

        letterByPlanetText.text = this.letterByPlanet[this.currentPlanet].ToString() + "/3";
        this._audioSource.Stop();
        this._audioSource.clip = audioClips[currentPlanet];
        this._audioSource.Play();
    }
}