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
    [SerializeField] private int sphereIndex = 0;
    [SerializeField] private CutOutUiScript _cutout;


    private float _playerSpeed = 0.2f;
    private Camera _camera;
    private bool playerActive = false;
    private SpriteRenderer _spriteRenderer;
    private bool introEnded = false;
    private bool canTravel = false;

    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
        spheres = GameObject.FindGameObjectsWithTag("Planet")
            .OrderBy(sphere => sphere.name)
            .ToArray();
        _spriteRenderer = GetComponent<SpriteRenderer>();

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
            _spriteRenderer.flipX = true;
            environment.transform.RotateAround(spheres[sphereIndex].transform.position, Vector3.back, _playerSpeed);
        }
        else if (moveX > 0)
        {
            // Move RIGHT
            _spriteRenderer.flipX = false;
            environment.transform.RotateAround(spheres[sphereIndex].transform.position, Vector3.forward, _playerSpeed);
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
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "Fuse")
        {
            travelButton.gameObject.SetActive(false);
            canTravel = false;
        }
    }
}