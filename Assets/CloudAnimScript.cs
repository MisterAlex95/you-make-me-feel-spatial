using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudAnimScript : MonoBehaviour
{
    public Transform center;
    public float speed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (center)
        {
            transform.RotateAround(center.position, Vector3.back, speed * Time.deltaTime);
        }
    }
}
