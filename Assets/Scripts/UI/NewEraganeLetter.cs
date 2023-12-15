using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class NewEraganeLetter : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private GameObject HoverAsset;

        private NewErganeCodexPage _newErganeCodexPage;
        private NewErganeLetterObj letter;

        public void SetNewErganeDictionaryPage(NewErganeCodexPage dico)
        {
            this._newErganeCodexPage = dico;
        }

        public void SetLetter(NewErganeLetterObj letter)
        {
            this.letter = letter;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            HoverAsset.SetActive(false);
            _newErganeCodexPage.GoToLetter(this.letter);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            HoverAsset.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            HoverAsset.SetActive(false);
        }
    }
}