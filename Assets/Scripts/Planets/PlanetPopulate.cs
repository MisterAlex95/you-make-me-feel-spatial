using UnityEngine;

public class PlanetPopulate : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private GameObject populateItem;

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
    }
}