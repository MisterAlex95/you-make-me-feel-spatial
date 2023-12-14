using UnityEngine;

public class OnlyClickGame : Interactable
{
    [SerializeField] private GameObject worldLetterPrefabs;
    [SerializeField] private NewErganeLetterObj letterToWin;
    [SerializeField] private Transform spawnPosition = null;

    private void OnMouseUp()
    {
        if (this.isActive == false) return;

        var go = Instantiate(worldLetterPrefabs, transform.parent);
        go.transform.position = spawnPosition?.position ?? this.transform.position;
        if (go.TryGetComponent<WorldLetter>(out var worldLetter))
        {
            worldLetter.SetLetter(letterToWin);
        }

        Destroy(this.gameObject);
    }
}