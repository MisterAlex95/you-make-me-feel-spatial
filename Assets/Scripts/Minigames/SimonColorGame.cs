using UnityEngine;

public class SimonColorGame : MonoBehaviour
{
    [SerializeField] private SimonGame _simonGame;
    [SerializeField] private COLOR color;
    [SerializeField] private AudioSource _audioSource;

    private void OnMouseDown()
    {
        this._simonGame.PushNewColor(color);
        PlaySound();
    }

    public void PlaySound()
    {
        _audioSource.Play();
    }
}