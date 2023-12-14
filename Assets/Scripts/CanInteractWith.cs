using UnityEngine;

public class CanInteractWith : MonoBehaviour
{
    [SerializeField] GameObject interactionIndicator;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Interactable"))
        {
            interactionIndicator.SetActive(true);
            if (interactionIndicator.TryGetComponent<Interactable>(out var interactable))
            {
                interactable.IsTriggerable(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Interactable"))
        {
            interactionIndicator.SetActive(false);
            if (interactionIndicator.TryGetComponent<Interactable>(out var interactable))
            {
                interactable.IsTriggerable(false);
            }
        }
    }
}