using UnityEngine;

public class WorldLetter : MonoBehaviour
{
    private NewErganeLetterObj _letter;

    public void SetLetter(NewErganeLetterObj letter)
    {
        this._letter = letter;
        var image = GetComponent<SpriteRenderer>();
        var spriteTexture = Sprite.Create(letter.texture, new Rect(0.0f, 0.0f, letter.texture.width, letter.texture.height), new Vector2(0.5f, 0.5f), 100.0f);
        image.sprite = spriteTexture;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("COLLIDER");
        if (other.CompareTag("Player"))
        {
            Destroy(this.gameObject);
            NewErganeDictionary.Instance.UnlockLetter(this._letter);
        }
    }
}