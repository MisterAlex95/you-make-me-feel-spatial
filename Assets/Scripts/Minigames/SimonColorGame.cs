using System.Collections;
using UnityEngine;

public class SimonColorGame : MonoBehaviour
{
    [SerializeField] private SimonGame _simonGame;
    [SerializeField] private COLOR color;
    [SerializeField] private AudioSource _audioSource;

    private void OnMouseDown()
    {
        
        this._simonGame.PushNewColor(color);
        StartCoroutine(FlashSpot());
        PlaySound();
    }

    private IEnumerator FlashSpot()
    {
        GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(.2f);
        GetComponent<SpriteRenderer>().enabled = false;
    } 
    
    public void PlaySound()
    {
        _audioSource.Play();
    }
}