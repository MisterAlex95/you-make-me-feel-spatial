using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSphereController : MonoBehaviour
{
    [SerializeField]
    private GameObject environment;

    [SerializeField] private GameObject[] spheres;

    [SerializeField] private Transform currentRotationOrigin;

    [SerializeField]
    private int sphereIndex = 0;
    private float speed = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        currentRotationOrigin = spheres[sphereIndex].transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            environment.transform.RotateAround(spheres[sphereIndex].transform.position, Vector3.back, speed);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            environment.transform.RotateAround(spheres[sphereIndex].transform.position, Vector3.forward, speed);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            sphereIndex++;
            if (sphereIndex == spheres.Length)
            {
                sphereIndex = 0;
            }
            currentRotationOrigin = spheres[sphereIndex].transform;
            transform.position = spheres[sphereIndex].transform.GetChild(0).position;
            transform.rotation = spheres[sphereIndex].transform.GetChild(0).rotation;
            
            Vector3 cameraPos = transform.position;
            cameraPos.z = Camera.main.transform.position.z;
            Camera.main.transform.position = cameraPos;
            Camera.main.transform.rotation = transform.rotation;
        }
    }
}
