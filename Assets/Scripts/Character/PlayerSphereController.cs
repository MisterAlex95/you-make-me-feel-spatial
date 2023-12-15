using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;


public class PlayerSphereController : MonoBehaviour
{
    [SerializeField] private GameObject environment;
    [SerializeField] private GameObject[] spheres;
    [SerializeField] private GameObject travelButton;
    [SerializeField] private PlayableDirector fuseDirector;
    [SerializeField] private PlayableDirector gameDirector;
    [SerializeField] private PlayableDirector endDirector;
    [SerializeField] private int sphereIndex = 0;
    [SerializeField] private CutOutUiScript _cutout;
    [SerializeField] private GameObject dialogs;
    [SerializeField] private GameObject alienDialogs;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Animator _animator;

    public float _playerSpeed = 15f;
    private Camera _camera;
    private bool playerActive = false;
    private bool introEnded = false;
    private bool canTravel = false;
    private bool canDialog = false;

    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
        spheres = GameObject.FindGameObjectsWithTag("Planet")
            .OrderBy(sphere => sphere.name)
            .ToArray();

        SetPlayerPosition();
    }

    void SetPlayerPosition()
    {
        for (var i = 0; i < spheres[sphereIndex].transform.childCount; i++)
        {
            var go = spheres[sphereIndex].transform.GetChild(i).gameObject;
            if (go.CompareTag("PlayerSpawn"))
            {
                Transform playerPosition = go.transform;
                var rotation = playerPosition.rotation;
                var position = playerPosition.position;
                Vector3 cameraPos = position;

                transform.position = position;
                transform.rotation = rotation;

                var cameraTransform = _camera.transform;
                cameraPos.z = cameraTransform.position.z;
                cameraTransform.position = cameraPos;
                cameraTransform.rotation = rotation;

                if (!playerActive)
                {
                    _cutout.FadeOut();
                    playerActive = true;
                }

                break;
            }
        }
    }

    void PlayerControl()
    {
        if (!playerActive || !introEnded)
        {
            return;
        }

        var moveX = Input.GetAxisRaw("Horizontal");

        if (moveX < 0)
        {
            // Move LEFT
            _animator.SetBool("Walking", true);
            _spriteRenderer.flipX = true;
            environment.transform.RotateAround(spheres[sphereIndex].transform.position, Vector3.back, _playerSpeed * Time.deltaTime);
        }
        else if (moveX > 0)
        {
            // Move RIGHT
            _animator.SetBool("Walking", true);
            _spriteRenderer.flipX = false;
            environment.transform.RotateAround(spheres[sphereIndex].transform.position, Vector3.forward, _playerSpeed * Time.deltaTime);
        }
        else
        {
            _animator.SetBool("Walking", false);
        }

        // Switch planet
        if (Input.GetKeyDown(KeyCode.Space) && canTravel)
        {
            playerActive = false;
            sphereIndex++;
            if (sphereIndex == spheres.Length)
            {
                sphereIndex = 0;
            }

            StartCoroutine(ChangePlanet());
        }
        
        // Show dialog
        if (Input.GetKeyDown(KeyCode.Space) && canDialog)
        {
            if (NewErganeDictionary.Instance.GetDictionary().Count >= 10)
            {
                endDirector.Play();
            }
            else
            {
                dialogs.SetActive(true);
                alienDialogs.SetActive(true);
                travelButton.gameObject.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        PlayerControl();
    }

    IEnumerator ChangePlanet()
    {
        fuseDirector.Play();
        yield return new WaitForSeconds(1);
        GameManager.Instance.SwitchToNextPlanet();
        SetPlayerPosition();
    }

    public void OnEndIntro()
    {
        introEnded = true;
    }

    public void OnStartGame()
    {
        gameDirector.Play();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Fuse" && introEnded && playerActive)
        {
            travelButton.gameObject.SetActive(true);
            canTravel = true;
        }
        
        if (other.name == "Alien" && introEnded && playerActive)
        {
            travelButton.gameObject.SetActive(true);
            canDialog = true;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "Fuse")
        {
            travelButton.gameObject.SetActive(false);
            canTravel = false;
        }
        
        if (other.name == "Alien" && canDialog)
        {
            dialogs.SetActive(false);
            alienDialogs.SetActive(false);
            travelButton.gameObject.SetActive(false);
            canDialog = false;
        }
    }
}