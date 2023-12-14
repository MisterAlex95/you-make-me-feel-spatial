using UnityEngine;

public class SimonColorGame : MonoBehaviour
{
    [SerializeField] private SimonGame _simonGame;
    [SerializeField] private COLOR color;

    private void OnMouseDown()
    {
        this._simonGame.PushNewColor(color);
    }
}