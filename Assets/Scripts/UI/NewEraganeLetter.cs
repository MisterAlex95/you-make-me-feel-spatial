using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class NewEraganeLetter : MonoBehaviour, IPointerClickHandler
    {
        private NewErganeDictionaryPage _newErganeDictionaryPage;
        private NewErganeLetterObj letter;

        public void SetNewErganeDictionaryPage(NewErganeDictionaryPage dico)
        {
            this._newErganeDictionaryPage = dico;
        }

        public void SetLetter(NewErganeLetterObj letter)
        {
            this.letter = letter;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _newErganeDictionaryPage.GoToLetter(this.letter);
        }
    }
}