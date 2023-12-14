using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<AudioClip> audioClips;
    [SerializeField] private GameObject[] spheres;
    [SerializeField] private AudioSource _audioSource;

    [FormerlySerializedAs("currentPlanete")] [SerializeField]
    private int currentPlanet = 0;

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
        spheres = GameObject.FindGameObjectsWithTag("Planet")
            .OrderBy(sphere => sphere.name)
            .ToArray();

        this._audioSource.clip = audioClips[currentPlanet];
        this._audioSource.Play();
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

        this._audioSource.Stop();
        this._audioSource.clip = audioClips[currentPlanet];
        this._audioSource.Play();
    }
}