using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class Interactable : MonoBehaviour
{
    protected bool isActive = false;

    public void IsTriggerable(bool state)
    {
        isActive = state;
    }
}