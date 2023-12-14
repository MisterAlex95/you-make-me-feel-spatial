using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vector3 = System.Numerics.Vector3;

public class PlanetPopulate : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private GameObject populateItem;
    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 360; i += 15)
        {
            //var go = Instantiate(populateItem, transform);
            //go.transform.RotateAround(transform.position, Vector2.right, i);
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
