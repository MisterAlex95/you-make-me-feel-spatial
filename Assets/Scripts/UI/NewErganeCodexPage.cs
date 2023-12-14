using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class NewErganeCodexPage : MonoBehaviour
    {
        [SerializeField] private GameObject lexiquePage;
        [SerializeField] private GameObject letterPage;
        [SerializeField] private GameObject lexiqueLetterPrefab;

        private void Start()
        {
            NewErganeDictionary.Instance.OnUnlockLetter += AddLetterToDictionary;
        }

        private void AddLetterToDictionary(NewErganeLetterObj letter)
        {
            var letterGo = Instantiate(lexiqueLetterPrefab, this.lexiquePage.transform);
            if (letterGo.TryGetComponent<RawImage>(out var letterGoImage))
            {
                letterGoImage.texture = letter.texture;
                if (letterGo.TryGetComponent<NewEraganeLetter>(out var letterGoScript))
                {
                    letterGoScript.SetNewErganeDictionaryPage(this);
                    letterGoScript.SetLetter(letter);
                }
            }
        }

        public void GoToLetter(NewErganeLetterObj letter)
        {
            this.lexiquePage.SetActive(false);
            if (this.letterPage.TryGetComponent<NewErganeLetterPage>(out var letterPageComponent))
            {
                letterPageComponent.SetLetter(letter);
                this.letterPage.SetActive(true);
            }
        }

        public void OnClickHome()
        {
            this.SwitchToLexique();
        }

        private void SwitchToLexique()
        {
            this.letterPage.SetActive(false);
            this.lexiquePage.SetActive(true);
        }
    }
}