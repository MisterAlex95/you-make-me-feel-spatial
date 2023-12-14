using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    protected bool IsActive = false;

    public void IsTriggerable(bool state)
    {
        IsActive = state;
    }
}