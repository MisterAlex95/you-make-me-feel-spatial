using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class NewEraganeLetter : MonoBehaviour, IPointerClickHandler
    {
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
            _newErganeCodexPage.GoToLetter(this.letter);
        }
    }
}