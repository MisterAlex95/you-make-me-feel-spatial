using UnityEngine;

public class SlideGame : Interactable
{
    [SerializeField] private GameObject worldLetterPrefabs;
    [SerializeField] private NewErganeLetterObj letterToWin;
    [SerializeField] private AudioSource soundEffect;

    private Vector3 mousePositionOffset = Vector3.zero;
    private Vector3 initialPosition = Vector3.zero;
    private bool gameStart = false;
    
    private void OnMouseDown()
    {
        if (!this.IsActive) return;
        gameStart = true;

        mousePositionOffset = transform.position - GetMouseWorldPosition();
        initialPosition = mousePositionOffset;
        soundEffect.Play();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        gameStart = false;
    }

    private void Update()
    {
        if (!this.IsActive || gameStart == false) return;

        if (Vector3.Distance(initialPosition, transform.position) > 10f ||
            Vector3.Distance(initialPosition, transform.position) < 10f)
        {
            var go = Instantiate(worldLetterPrefabs, transform.parent);
            go.transform.position = this.transform.position;
            if (go.TryGetComponent<WorldLetter>(out var worldLetter))
            {
                worldLetter.SetLetter(letterToWin);
            }

            Destroy(this.gameObject);
        }
    }


    private Vector3 GetMouseWorldPosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    
    private void OnMouseDrag()
    {
        if (!this.IsActive) return;
        transform.position = GetMouseWorldPosition() + mousePositionOffset;
    }
}