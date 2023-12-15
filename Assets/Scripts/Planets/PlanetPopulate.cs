using Unity.VisualScripting;
using UnityEngine;

public class PlanetPopulate : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private GameObject cloud;
    [SerializeField] private GameObject populateItem;

    public bool populateCloud = true;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 360; i += 0)
        {
            var go = Instantiate(populateItem, transform);
            var randomScale = Random.Range(0.15f, 0.3f);
            var v = go.transform.position;
            v.z = 0.5f - randomScale;
            v.y -= 0.8f;
            go.transform.position = v;
            go.transform.localScale = new Vector3(randomScale, randomScale);
            go.transform.RotateAround(transform.position, Vector3.forward, i);
            var randomSprite = Random.Range(0, sprites.Length);
            go.GetComponent<SpriteRenderer>().sprite = sprites[randomSprite];

            if (randomScale >= 0.15f && randomScale < 0.18f)
            {
                go.GetComponent<SpriteRenderer>().color = new Color(0.35f, 0.35f, 0.35f);
            }

            if (randomScale >= 0.18f && randomScale < 0.22f)
            {
                go.GetComponent<SpriteRenderer>().color = new Color(0.6f, 0.6f, 0.6f);
            }

            if (randomScale >= 0.22f)
            {
                go.GetComponent<SpriteRenderer>().color = new Color(0.8f, 0.8f, 0.8f);
            }

            if (Random.Range(0, 2) == 1)
            {
                go.GetComponent<SpriteRenderer>().flipX = true;
            }

            i += Random.Range(15, 20);
        }

        if (populateCloud)
        {
            for (int i = 0; i < 360; i += 5)
            {
                if (Random.Range(0, 1f) > 0.4)
                {
                    var goCloud = Instantiate(cloud, transform);
                    var cloudRandomScale = Random.Range(0.15f, 0.25f);
                    var cloudV = goCloud.transform.position;
                    cloudV.z = 0.5f - cloudRandomScale;
                    cloudV.x += Random.Range(-1f, 1f);
                    cloudV.y += Random.Range(-0.5f, 0.5f);
                    goCloud.transform.position = cloudV;
                    goCloud.transform.localScale = new Vector3(cloudRandomScale, cloudRandomScale);
                    goCloud.transform.RotateAround(transform.position, Vector3.forward, i);
                    goCloud.GetComponent<CloudAnimScript>().center = transform;
                    
                    if (cloudRandomScale >= 0.15f && cloudRandomScale < 0.18f)
                    {
                        goCloud.GetComponent<SpriteRenderer>().color = new Color(0.8f, 0.8f, 0.8f);
                    }

                    if (cloudRandomScale >= 0.18f && cloudRandomScale < 0.22f)
                    {
                        goCloud.GetComponent<SpriteRenderer>().color = new Color(0.9f, 0.9f, 0.9f);
                    }

                    if (cloudRandomScale >= 0.22f)
                    {
                        goCloud.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f);
                    }

                    goCloud.GetComponent<CloudAnimScript>().speed = Random.Range(3f, 5f);
                    
                    if (Random.Range(0, 2) == 1)
                    {
                        goCloud.GetComponent<SpriteRenderer>().flipX = true;
                    }
                }
            }
        }
    }
}