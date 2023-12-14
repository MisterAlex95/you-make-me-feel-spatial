using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerSphereController : MonoBehaviour
{
    [SerializeField]
    private GameObject environment;
    [SerializeField] private GameObject[] spheres;
    [SerializeField] private GameObject travelButton;
    [SerializeField] private Transform currentRotationOrigin;
    [SerializeField]
    private int sphereIndex = 0;
    [SerializeField]
    private CutOutUiScript _cutout;


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
        
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            // Move LEFT
            _spriteRenderer.flipX = true;
            environment.transform.RotateAround(spheres[sphereIndex].transform.position, Vector3.back, _playerSpeed);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
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
        _cutout.FadeIn();
        yield return new WaitForSeconds(1);
        SetPlayerPosition();
        _cutout.FadeOut();
    }

    public void OnEndIntro()
    {
        introEnded = true;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Fuse")
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
