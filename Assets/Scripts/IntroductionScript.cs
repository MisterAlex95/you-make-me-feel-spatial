using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class IntroductionScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private PlayableDirector introDirector;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            introDirector.Play();
        }
    }
}
