using UnityEngine;
using UnityEngine.Serialization;

public class WorldLetter : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    private NewErganeLetterObj _letter;
    public GameObject particles;

    public void SetLetter(NewErganeLetterObj letter)
    {
        this._letter = letter;
        var image = GetComponent<SpriteRenderer>();
        var spriteTexture = Sprite.Create(letter.texture, new Rect(0.0f, 0.0f, letter.texture.width, letter.texture.height), new Vector2(0.5f, 0.5f), 100.0f);
        image.sprite = spriteTexture;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var transform1 = transform;
            Instantiate(particles, transform1.position, transform1.rotation, transform1.parent);
            audioSource.Play();
            NewErganeDictionary.Instance.UnlockLetter(this._letter);
            Destroy(this.gameObject, audioSource.clip.length);
        }
    }
}